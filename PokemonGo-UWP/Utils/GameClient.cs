using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.Devices.Geolocation;
using Windows.Security.Credentials;
using Windows.UI.Xaml;
using PokemonGo.RocketAPI;
using PokemonGo.RocketAPI.Console;
using PokemonGo.RocketAPI.Enums;
using PokemonGo.RocketAPI.Extensions;
using PokemonGo_UWP.Entities;
using PokemonGo_UWP.ViewModels;
using POGOProtos.Data;
using POGOProtos.Data.Player;
using POGOProtos.Enums;
using POGOProtos.Inventory;
using POGOProtos.Inventory.Item;
using POGOProtos.Map.Fort;
using POGOProtos.Map.Pokemon;
using POGOProtos.Networking.Envelopes;
using POGOProtos.Networking.Responses;
using POGOProtos.Settings;
using POGOProtos.Settings.Master;
using Q42.WinRT.Data;
using Template10.Common;
using Template10.Utils;
using Windows.Devices.Sensors;
using Newtonsoft.Json;
using PokemonGo.RocketAPI.Rpc;
using PokemonGoAPI.Session;
using PokemonGo_UWP.Utils.Helpers;
using System.Collections.Specialized;
using Windows.UI.Popups;
using System.ComponentModel;
using PokemonGo_UWP.Views;
using POGOLib.Official.Util.Hash;
using PokemonGoAPI.Helpers.Hash.PokeHash;
using PokemonGoAPI.Exceptions;
using Google.Protobuf.Collections;
using POGOProtos.Data.Battle;

namespace PokemonGo_UWP.Utils
{
    /// <summary>
    ///     Static class containing game's state and wrapped client methods to update data
    /// </summary>
    public static class GameClient
    {
        #region Client Vars

        private static ISettings _clientSettings;
        private static Client _client;

        public static bool IsInitialized { get; private set; }
        public static bool LoggedIn { get; set; }

        /// <summary>
        /// Handles map updates using Niantic's logic.
        /// Ported from <seealso cref="https://github.com/AeonLucid/POGOLib"/>
        /// </summary>
        private class Heartbeat
        {
            /// <summary>
            ///     Determines whether we can keep heartbeating.
            /// </summary>
            private bool _keepHeartbeating = true;

            /// <summary>
            /// Timer used to update map
            /// </summary>
            private DispatcherTimer _mapUpdateTimer;

            /// <summary>
            /// Timer used to update applied item
            /// </summary>
            private DispatcherTimer _appliedItemUpdateTimer;

            /// <summary>
            /// True if another update operation is in progress.
            /// </summary>
            private bool _isHeartbeating;

            /// <summary>
            /// Checks if we need to update data
            /// </summary>
            /// <param name="sender"></param>
            /// <param name="o"></param>
            private async void HeartbeatTick(object sender, object o)
            {
                // We need to skip this iteration
                if (!_keepHeartbeating || _isHeartbeating) return;
                _isHeartbeating = true;
                // Heartbeat is alive so we check if we need to update data, based on GameSettings
                var canRefresh = false;


                //Collect location data for signature
                DeviceInfos.Current.CollectLocationData();

                // We have no settings yet so we just update without further checks
                if (GameSetting == null)
                {
                    canRefresh = true;
                }
                else
                {
                    // Check if we need to update
                    var minSeconds = GameSetting.MapSettings.GetMapObjectsMinRefreshSeconds;
                    var maxSeconds = GameSetting.MapSettings.GetMapObjectsMaxRefreshSeconds;
                    var minDistance = GameSetting.MapSettings.GetMapObjectsMinDistanceMeters;
                    var lastGeoCoordinate = _lastGeopositionMapObjectsRequest;
                    var secondsSinceLast = DateTime.Now.Subtract(BaseRpc.LastRpcRequest).Seconds;
                    if (lastGeoCoordinate == null)
                    {
                        Logger.Write("Refreshing MapObjects, reason: 'lastGeoCoordinate == null'.");
                        canRefresh = true;
                    }
                    else if (secondsSinceLast >= minSeconds)
                    {
                        var metersMoved = GeoHelper.Distance(LocationServiceHelper.Instance.Geoposition.Coordinate.Point, lastGeoCoordinate.Coordinate.Point);
                        if (secondsSinceLast >= maxSeconds)
                        {
                            Logger.Write($"Refreshing MapObjects, reason: 'secondsSinceLast({secondsSinceLast}) >= maxSeconds({maxSeconds})'.");
                            canRefresh = true;
                        }
                        else if (metersMoved >= minDistance)
                        {
                            Logger.Write($"Refreshing MapObjects, reason: 'metersMoved({metersMoved}) >= minDistance({minDistance})'.");
                            canRefresh = true;
                        }
                    }
                }
                // Update!
                if (!canRefresh)
                {
                    _isHeartbeating = false;
                    return;
                }
                try
                {
                    await UpdateMapObjects();
                }
                catch (Exception ex)
                {
                    await ExceptionHandler.HandleException(ex);
                }
                finally
                {
                    _isHeartbeating = false;
                }
            }

            /// <summary>
            /// Inits heartbeat
            /// </summary>
            internal async Task StartDispatcher()
            {
                _keepHeartbeating = true;
                if (_mapUpdateTimer == null)
                {
                    await DispatcherHelper.RunInDispatcherAndAwait(() =>
                    {
                        _mapUpdateTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
                        _mapUpdateTimer.Tick += HeartbeatTick;
                        _mapUpdateTimer.Start();
                    });
                }
                if (_appliedItemUpdateTimer == null)
                {
                    await DispatcherHelper.RunInDispatcherAndAwait((Action)(() =>
                    {
                        _appliedItemUpdateTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
                        _appliedItemUpdateTimer.Tick += this._appliedItemUpdateTimer_Tick;
                        _appliedItemUpdateTimer.Start();
                    }));
                }
            }

            private void _appliedItemUpdateTimer_Tick(object sender, object e)
            {
                foreach (AppliedItemWrapper appliedItem in AppliedItems)
                {
                    if (appliedItem.IsExpired)
                    {
                        AppliedItems.Remove(appliedItem);
                        break;
                    }
                    appliedItem.Update(appliedItem.WrappedData);
                }
            }

            /// <summary>
            /// Stops heartbeat
            /// </summary>
            internal void StopDispatcher()
            {
                _keepHeartbeating = false;
            }
        }
        #endregion

        #region Game Vars

        /// <summary>
        ///     App's current version
        /// </summary>
        public static string CurrentVersion
        {
            get
            {
                var currentVersion = Package.Current.Id.Version;
                return $"v{currentVersion.Major}.{currentVersion.Minor}.{currentVersion.Build}";
            }
        }

        /// <summary>
        ///     Settings downloaded from server
        /// </summary>
        public static GlobalSettings GameSetting { get; private set; }

        /// <summary>
        ///     Player's data, we use it just for the username
        /// </summary>
        public static PlayerData PlayerData { get; private set; }

        /// <summary>
        ///     Stats for the current player, including current level and experience related stuff
        /// </summary>
        public static PlayerStats PlayerStats { get; private set; }

        /// <summary>
        ///     Contains infos about level up rewards
        /// </summary>
        public static InventoryDelta InventoryDelta { get; private set; }

        public static bool IsIncenseActive
        {
            get { return AppliedItems.Count(x => x.ItemType == ItemType.Incense && !x.IsExpired) > 0; }
        }

        public static bool IsXpBoostActive
        {
            get { return AppliedItems.Count(x => x.ItemType == ItemType.XpBoost && !x.IsExpired) > 0; }
        }

        public static ObservableCollection<int> AdwardedXP { get; set; } =
            new ObservableCollection<int>();

        public static void AddGameXP(int AwardedXP)
        {
            AdwardedXP.Add(AwardedXP);
        }

        #region Collections

        /// <summary>
        ///		Collection of applied items
        /// </summary>
        public static ObservableCollection<AppliedItemWrapper> AppliedItems { get; set; } =
            new ObservableCollection<AppliedItemWrapper>();

        /// <summary>
        ///     Collection of Pokemon in 1 step from current position
        /// </summary>
        public static ObservableCollection<MapPokemonWrapper> CatchablePokemons { get; set; } =
            new ObservableCollection<MapPokemonWrapper>();

        /// <summary>
        ///     Collection of Pokemon in 2 steps from current position
        /// </summary>
        public static ObservableCollection<NearbyPokemonWrapper> NearbyPokemons { get; set; } =
            new ObservableCollection<NearbyPokemonWrapper>();

        /// <summary>
        ///     Collection of lured Pokemon
        /// </summary>
        public static ObservableCollection<LuredPokemon> LuredPokemons { get; set; } = new ObservableCollection<LuredPokemon>();

        /// <summary>
        ///     Collection of incense Pokemon
        /// </summary>
        public static ObservableCollection<IncensePokemon> IncensePokemons { get; set; } = new ObservableCollection<IncensePokemon>();

        /// <summary>
        ///     Collection of Pokestops in the current area
        /// </summary>
        public static ObservableCollection<FortDataWrapper> NearbyPokestops { get; set; } =
            new ObservableCollection<FortDataWrapper>();

        /// <summary>
        ///     Collection of Gyms in the current area
        /// </summary>
        public static ObservableCollection<FortDataWrapper> NearbyGyms { get; set; } =
            new ObservableCollection<FortDataWrapper>();

        /// <summary>
        ///     Stores Items in the current inventory
        /// </summary>
        public static ObservableCollection<ItemData> ItemsInventory { get; set; } = new ObservableCollection<ItemData>()
            ;

        /// <summary>
        ///     Stores Items that can be used to catch a Pokemon
        /// </summary>
        public static ObservableCollection<ItemData> CatchItemsInventory { get; set; } =
            new ObservableCollection<ItemData>();

        /// <summary>
        ///     Stores Incubators in the current inventory
        /// </summary>
        public static ObservableCollection<EggIncubator> IncubatorsInventory { get; set; } =
            new ObservableCollection<EggIncubator>();

        /// <summary>
        ///     Stores Pokemons in the current inventory
        /// </summary>
        public static ObservableCollectionPlus<PokemonData> PokemonsInventory { get; set; } =
            new ObservableCollectionPlus<PokemonData>();

        /// <summary>
        ///     Stores Eggs in the current inventory
        /// </summary>
        public static ObservableCollection<PokemonData> EggsInventory { get; set; } =
            new ObservableCollection<PokemonData>();

        /// <summary>
        ///     Stores player's current Pokedex
        /// </summary>
        public static ObservableCollectionPlus<PokedexEntry> PokedexInventory { get; set; } =
            new ObservableCollectionPlus<PokedexEntry>();

        /// <summary>
        ///     Stores player's current candies
        /// </summary>
        public static ObservableCollection<Candy> CandyInventory { get; set; } = new ObservableCollection<Candy>();

        #endregion

        #region Templates from server

        /// <summary>
        ///     Stores extra useful data for the Pokedex, like Pokemon type and other stuff that is missing from PokemonData
        /// </summary>
        public static IEnumerable<PokemonSettings> PokemonSettings { get; private set; } = new List<PokemonSettings>();

        /// <summary>
        ///     Stores upgrade costs (candy, stardust) per each level
        /// </summary>
        //public static Dictionary<int, object[]> PokemonUpgradeCosts { get; private set; } = new Dictionary<int, object[]>();

        /// <summary>
        /// Upgrade settings per each level
        /// </summary>
        public static PokemonUpgradeSettings PokemonUpgradeSettings { get; private set; }

        /// <summary>
        ///     Stores data about Pokemon Go settings
        /// </summary>
        public static IEnumerable<MoveSettings> MoveSettings { get; private set; } = new List<MoveSettings>();
        public static IEnumerable<BadgeSettings> BadgeSettings { get; private set; } = new List<BadgeSettings>();
        public static IEnumerable<GymBattleSettings> BattleSettings { get; private set; } = new List<GymBattleSettings>();
        public static IEnumerable<EncounterSettings> EncounterSettings { get; private set; } = new List<EncounterSettings>();
        public static IEnumerable<GymLevelSettings> GymLevelSettings { get; private set; } = new List<GymLevelSettings>();
        public static IEnumerable<IapSettings> IapSettings { get; private set; } = new List<IapSettings>();
        public static IEnumerable<ItemSettings> ItemSettings { get; private set; } = new List<ItemSettings>();
        public static IEnumerable<PlayerLevelSettings> PlayerLevelSettings { get; private set; } = new List<PlayerLevelSettings>();
        public static IEnumerable<QuestSettings> QuestSettings { get; private set; } = new List<QuestSettings>();
        public static IEnumerable<IapItemDisplay> IapItemDisplay { get; private set; } = new List<IapItemDisplay>();
        public static IEnumerable<MoveSequenceSettings> MoveSequenceSettings { get; private set; } = new List<MoveSequenceSettings>();
        public static IEnumerable<CameraSettings> CameraSettings { get; private set; } = new List<CameraSettings>();
        #endregion

        #endregion

        #region Constructor

        static GameClient()
        {
            PokedexInventory.CollectionChanged += PokedexInventory_CollectionChanged;
            AppliedItems.CollectionChanged += AppliedItems_CollectionChanged;
            // TODO: Investigate whether or not this needs to be unsubscribed when the app closes.
        }

        public static void SetCredentialsFromSettings()
        {
            var credentials = SettingsService.Instance.UserCredentials;
            if (credentials != null)
            {
                credentials.RetrievePassword();
                _clientSettings = new Settings()
                {
                    AuthType = SettingsService.Instance.LastLoginService,
                    PtcUsername = SettingsService.Instance.LastLoginService == AuthType.Ptc ? credentials.UserName : null,
                    PtcPassword = SettingsService.Instance.LastLoginService == AuthType.Ptc ? credentials.Password : null,
                    GoogleUsername = SettingsService.Instance.LastLoginService == AuthType.Google ? credentials.UserName : null,
                    GooglePassword = SettingsService.Instance.LastLoginService == AuthType.Google ? credentials.Password : null,
                };
            }
        }

        /// <summary>
        /// When new items are added to the Pokedex, reset the Nearby Pokemon so their state can be re-run.
        /// </summary>
        /// <remarks>
        /// This exists because the Nearby Pokemon are Map objects, and are loaded before Inventory. If you don't do this,
        /// the first Nearby items are always shown as "new to the Pokedex" until they disappear, regardless of if they are
        /// ACTUALLY new.
        /// </remarks>
        private static void PokedexInventory_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                // advancedrei: This is a total order-of-operations hack.
                var nearby = NearbyPokemons.ToList();
                NearbyPokemons.Clear();
                NearbyPokemons.AddRange(nearby);
            }
        }

        private static void AppliedItems_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                OnAppliedItemStarted?.Invoke(null, (AppliedItemWrapper)e.NewItems[0]);
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                OnAppliedItemExpired?.Invoke(null, (AppliedItemWrapper)e.OldItems[0]);
            }
        }


        #endregion

        #region Game Logic

        #region Login/Logout

        /// <summary>
        /// Saves the new AccessToken to settings.
        /// </summary>
        private static void SaveAccessToken()
        {
            SettingsService.Instance.AccessTokenString = JsonConvert.SerializeObject(_client.AccessToken);
        }

        /// <summary>
        /// Loads current AccessToken
        /// </summary>
        /// <returns></returns>
        public static AccessToken LoadAccessToken()
        {
            var tokenString = SettingsService.Instance.AccessTokenString;
            return tokenString == null ? null : JsonConvert.DeserializeObject<AccessToken>(SettingsService.Instance.AccessTokenString);
        }


        /// <summary>
        /// Creates and initializes API client
        /// </summary>
        private static void CreateClient(Geoposition pos)
        {
            //Unregister old handlers
            if (_client != null)
            {
                _client.CheckChallengeReceived -= _client_CheckChallengeReceived;
            }

            _client = new Client(SettingsService.Instance.PokehashAuthKey, _clientSettings, null, DeviceInfos.Current);
            _client.SetInitialLocation(pos);
            _client.StartTime = PokemonGo.RocketAPI.Helpers.Utils.GetTime(true);

            //Register EventHandlers
            _client.CheckChallengeReceived += _client_CheckChallengeReceived;

            var apiFailureStrategy = new ApiFailureStrategy(_client);
            _client.ApiFailure = apiFailureStrategy;
            // Register to AccessTokenChanged
            apiFailureStrategy.OnAccessTokenUpdated += (s, e) => SaveAccessToken();
            apiFailureStrategy.OnFailureToggleUpdateTimer += ToggleUpdateTimer;
        }


        /// <summary>
        ///     Sets things up if we didn't come from the login page
        /// </summary>
        /// <returns></returns>
        public static async Task InitializeClient()
        {
            DataCache.Init();

            var credentials = SettingsService.Instance.UserCredentials;
            credentials.RetrievePassword();
            _clientSettings = new Settings
            {
                AuthType = SettingsService.Instance.LastLoginService,
                PtcUsername = SettingsService.Instance.LastLoginService == AuthType.Ptc ? credentials.UserName : null,
                PtcPassword = SettingsService.Instance.LastLoginService == AuthType.Ptc ? credentials.Password : null,
                GoogleUsername = SettingsService.Instance.LastLoginService == AuthType.Google ? credentials.UserName : null,
                GooglePassword = SettingsService.Instance.LastLoginService == AuthType.Google ? credentials.Password : null,
            };

            Geoposition pos = await GetInitialLocation();

            CreateClient(pos);

            try
            {
                await _client.Login.DoLogin();
            }
            catch (Exception e)
            {
                if (e is PokemonGo.RocketAPI.Exceptions.AccessTokenExpiredException)
                {
                    Debug.WriteLine("AccessTokenExpired Exception caught");
                    _client.AccessToken.Expire();
                    await _client.Login.DoLogin();
                }
                else if (e is HasherException)
                {
                    Debug.WriteLine("A PokeHash Exception occurred");
                    throw e;
                }
                else
                {
                    ConfirmationDialog dialog = new ConfirmationDialog(e.Message);
                    dialog.Show();
                    //await new MessageDialog(e.Message).ShowAsyncQueue();
                }
            }

            IsInitialized = true;
        }

        /// <summary>
        ///     Starts a PTC session for the given user
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <returns>true if login worked</returns>
        public static async Task<bool> DoPtcLogin(string username, string password)
        {
            _clientSettings = new Settings
            {
                PtcUsername = username,
                PtcPassword = password,
                AuthType = AuthType.Ptc
            };

            Geoposition pos = await GetInitialLocation();

            CreateClient(pos);

            // Get PTC token
            await _client.Login.DoLogin();
            // Update current token even if it's null and clear the token for the other identity provide
            SaveAccessToken();
            // Update other data if login worked
            if (_client.AccessToken == null) return false;
            SettingsService.Instance.LastLoginService = AuthType.Ptc;
            SettingsService.Instance.UserCredentials = new PasswordCredential(nameof(SettingsService.Instance.UserCredentials), username, password);

            // Get the game settings, so we can start with the tutorial, which needs info on the starter pokemons
            await LoadGameSettings();

            // Return true if login worked, meaning that we have a token
            return true;
        }


        /// <summary>
        ///     Starts a Google session for the given user
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns>true if login worked</returns>
        public static async Task<bool> DoGoogleLogin(string email, string password)
        {
            _clientSettings = new Settings
            {
                GoogleUsername = email,
                GooglePassword = password,
                AuthType = AuthType.Google
            };

            Geoposition pos = await GetInitialLocation();

            CreateClient(pos);

            // Get Google token
            await _client.Login.DoLogin();
            // Update current token even if it's null and clear the token for the other identity provide
            SaveAccessToken();
            // Update other data if login worked
            if (_client.AccessToken == null) return false;
            SettingsService.Instance.LastLoginService = AuthType.Google;
            SettingsService.Instance.UserCredentials = new PasswordCredential(nameof(SettingsService.Instance.UserCredentials), email, password);

            // Get the game settings, so we can start with the tutorial, which needs info on the starter pokemons
            await LoadGameSettings();

            // Return true if login worked, meaning that we have a token
            return true;
        }

        /// <summary>
        ///     Logs the user out by clearing data and timers
        /// </summary>
        public static void DoLogout()
        {
            // Clear stored token
            SettingsService.Instance.AccessTokenString = null;
            if (!SettingsService.Instance.RememberLoginData)
                SettingsService.Instance.UserCredentials = null;
            _heartbeat?.StopDispatcher();
            LocationServiceHelper.Instance.PropertyChanged -= LocationHelperPropertyChanged;
            _lastGeopositionMapObjectsRequest = null;
        }

        #endregion

        #region Data Updating
        private static Compass _compass;

        private static Heartbeat _heartbeat;

        public static event EventHandler<GetHatchedEggsResponse> OnEggHatched;
        public static event EventHandler<AppliedItemWrapper> OnAppliedItemExpired;
        public static event EventHandler<AppliedItemWrapper> OnAppliedItemStarted;

        #region GameSettings
        private static Task LoadGameSettingsAsync()
        {
            return Task.Run(() => LoadGameSettings());
        }

        public static async Task LoadGameSettings(bool ForceRefresh = false)
        {
            // Compare the cacheExpiryDateTime to the last updated settings (if new settings are needed, override the caches ones)
            DateTime cacheExpiryDateTime = await DataCache.GetExpiryDate<GlobalSettings>(nameof(GameSetting));
            if (VersionInfo.Instance.settings_updated.AddMonths(1) > cacheExpiryDateTime)
            {
                ForceRefresh = true;
                Busy.SetBusy(true, Resources.CodeResources.GetString("RefreshingGameSettings"));
            }

            // Before starting we need game settings and templates
            GameSetting = await DataCache.GetAsync(nameof(GameSetting), async () => (await _client.Download.GetSettings()).Settings, DateTime.Now.AddMonths(1), ForceRefresh);

            // The itemtemplates can be upated since a new release, how can we detect this to enable a force refresh here?
            await UpdateItemTemplates(ForceRefresh);
        }
        #endregion

        #region Compass Stuff
        /// <summary>
        /// We fire this event when the current compass position changes
        /// </summary>
        public static event EventHandler<CompassReading> HeadingUpdated;
        private static void compass_ReadingChanged(Compass sender, CompassReadingChangedEventArgs args)
        {
            HeadingUpdated?.Invoke(sender, args.Reading);
        }
        #endregion

        /// <summary>
        ///     Starts the timer to update map objects and the handler to update position
        /// </summary>
        public static async Task InitializeDataUpdate()
        {
            // Get the game settings, they contain information about pokemons, moves, and a lot more...
            await LoadGameSettings();

            #region Compass management
            SettingsService.Instance.PropertyChanged += (object sender, PropertyChangedEventArgs e) =>
            {
                if (e.PropertyName == nameof(SettingsService.Instance.MapAutomaticOrientationMode))
                {
                    switch (SettingsService.Instance.MapAutomaticOrientationMode)
                    {
                        case MapAutomaticOrientationModes.Compass:
                            _compass = Compass.GetDefault();
                            _compass.ReportInterval = Math.Max(_compass.MinimumReportInterval, 50);
                            _compass.ReadingChanged += compass_ReadingChanged;
                            break;
                        case MapAutomaticOrientationModes.None:
                        case MapAutomaticOrientationModes.GPS:
                        default:
                            if (_compass != null)
                            {
                                _compass.ReadingChanged -= compass_ReadingChanged;
                                _compass = null;
                            }
                            break;
                    }
                }
            };
            //Trick to trigger the PropertyChanged for MapAutomaticOrientationMode ;)
            SettingsService.Instance.MapAutomaticOrientationMode = SettingsService.Instance.MapAutomaticOrientationMode;
            #endregion
            Busy.SetBusy(true, Resources.CodeResources.GetString("GettingGpsSignalText"));
            await LocationServiceHelper.Instance.InitializeAsync();

            // Update geolocator settings based on server
            LocationServiceHelper.Instance.UpdateMovementThreshold(GameSetting.MapSettings.GetMapObjectsMinDistanceMeters);
            LocationServiceHelper.Instance.PropertyChanged += LocationHelperPropertyChanged;
            if (_heartbeat == null)
                _heartbeat = new Heartbeat();
            await _heartbeat.StartDispatcher();
            // Update before starting timer
            Busy.SetBusy(true, Resources.CodeResources.GetString("GettingUserDataText"));
            //await UpdateMapObjects();
            await UpdateInventory();
            if (PlayerData != null && PlayerStats != null)
                Busy.SetBusy(false);
        }

        private static async void _client_CheckChallengeReceived(object sender, CheckChallengeResponse e)
        {

            if (e.ShowChallenge && !String.IsNullOrWhiteSpace(e.ChallengeUrl) && e.ChallengeUrl.Length > 5)
            {
                // Captcha is shown in checkChallengeResponse.ChallengeUrl
                Logger.Write($"ChallengeURL: {e.ChallengeUrl}");
                // breakpoint here to manually resolve Captcha in a browser
                // after that set token to str variable from browser (see screenshot)
                Logger.Write("Pause");

                //GOTO THE REQUIRED PAGE
                if (BootStrapper.Current.NavigationService.CurrentPageType != typeof(ChallengePage))
                {
                     await DispatcherHelper.RunInDispatcherAndAwait(() =>
                     {
                         // We are not in UI thread probably, so run this via dispatcher
                         BootStrapper.Current.NavigationService.Navigate(typeof(ChallengePage), e.ChallengeUrl);
                     });
                }
            }

        }

        private static void LocationHelperPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if(e.PropertyName==nameof(LocationServiceHelper.Instance.Geoposition))
			{
                if (_lastPlayerLocationUpdate == null || _lastPlayerLocationUpdate.AddSeconds((int)GameClient.GameSetting.MapSettings.GetMapObjectsMinRefreshSeconds) < DateTime.Now)
                {
                    // Updating player's position
                    var position = LocationServiceHelper.Instance.Geoposition.Coordinate.Point.Position;
                    if (_client != null)
                    {
                        _lastPlayerLocationUpdate = DateTime.Now;
                        _client.Player.UpdatePlayerLocation(position.Latitude, position.Longitude, LocationServiceHelper.Instance.Geoposition.Coordinate.Accuracy);
                    }
                }
			}
		}

        private static DateTime _lastPlayerLocationUpdate;

        /// <summary>
        ///     DateTime for the last map update
        /// </summary>
        private static DateTime _lastUpdate;

        /// <summary>
        ///     Toggles the update timer based on the isEnabled value
        /// </summary>
        /// <param name="isEnabled"></param>
        public static async void ToggleUpdateTimer(bool isEnabled = true)
        {
            if (_heartbeat == null) return;

            Logger.Write($"Called ToggleUpdateTimer({isEnabled})");
            if (isEnabled)
                await _heartbeat.StartDispatcher();
            else
            {
                _heartbeat.StopDispatcher();
            }
        }

        /// <summary>
        ///     Updates catcheable and nearby Pokemons + Pokestops.
        ///     We're using a single method so that we don't need two separate calls to the server, making things faster.
        /// </summary>
        /// <returns></returns>
        private static async Task UpdateMapObjects()
        {
            // Get all map objects from server
            var mapObjects = await GetMapObjects(LocationServiceHelper.Instance.Geoposition);
            _lastUpdate = DateTime.Now;

            // update catchable pokemons
            var newCatchablePokemons = mapObjects.Item1.MapCells.SelectMany(x => x.CatchablePokemons).Select(item => new MapPokemonWrapper(item)).ToArray();
            Logger.Write($"Found {newCatchablePokemons.Length} catchable pokemons");
            CatchablePokemons.UpdateWith(newCatchablePokemons, x => x,
                (x, y) => x.EncounterId == y.EncounterId);

            // update nearby pokemons
            var newNearByPokemons = mapObjects.Item1.MapCells.SelectMany(x => x.NearbyPokemons).ToArray();
            Logger.Write($"Found {newNearByPokemons.Length} nearby pokemons");
            // for this collection the ordering is important, so we follow a slightly different update mechanism
            NearbyPokemons.UpdateByIndexWith(newNearByPokemons, x => new NearbyPokemonWrapper(x));

            // update poke stops on map
            var newPokeStops = mapObjects.Item1.MapCells
                .SelectMany(x => x.Forts)
                .Where(x => x.Type == FortType.Checkpoint)
                .ToArray();
            Logger.Write($"Found {newPokeStops.Length} nearby PokeStops");
            NearbyPokestops.UpdateWith(newPokeStops, x => new FortDataWrapper(x), (x, y) => x.Id == y.Id);

            // update gyms on map
            var newGyms = mapObjects.Item1.MapCells
                .SelectMany(x => x.Forts)
                .Where(x => x.Type == FortType.Gym)
                .ToArray();
            Logger.Write($"Found {newGyms.Length} nearby Gyms");
            // For now, we do not show the gyms on the map, as they are not complete yet. Code remains, so we can still work on it.
            NearbyGyms.UpdateWith(newGyms, x => new FortDataWrapper(x), (x, y) => x.Id == y.Id);

            // Update LuredPokemon
            var newLuredPokemon = newPokeStops.Where(item => item.LureInfo != null).Select(item => new LuredPokemon(item.LureInfo, item.Latitude, item.Longitude)).ToArray();
            Logger.Write($"Found {newLuredPokemon.Length} lured Pokemon");
            LuredPokemons.UpdateByIndexWith(newLuredPokemon, x => x);

            // Update IncensePokemon
            if (IsIncenseActive)
            {
                var incensePokemonResponse = await GetIncensePokemons(LocationServiceHelper.Instance.Geoposition);
                if (incensePokemonResponse.Result == GetIncensePokemonResponse.Types.Result.IncenseEncounterAvailable)
                {
                    IncensePokemon[] newIncensePokemon;
                    newIncensePokemon = new IncensePokemon[1];
                    newIncensePokemon[0] = new IncensePokemon(incensePokemonResponse, incensePokemonResponse.Latitude, incensePokemonResponse.Longitude);
                    Logger.Write($"Found incense Pokemon {incensePokemonResponse.PokemonId}");
                    IncensePokemons.UpdateByIndexWith(newIncensePokemon, x => x);
                }
            }
            Logger.Write("Finished updating map objects");

            // Update BuddyPokemon Stats
            //if (GameClient.PlayerData.BuddyPokemon.Id != 0)
            //if (true)
            //{
                var buddyWalkedResponse = await GetBuddyWalked();
                if (buddyWalkedResponse.Success)
                {
                    Logger.Write($"BuddyWalked CandyID: {buddyWalkedResponse.FamilyCandyId}, CandyCount: {buddyWalkedResponse.CandyEarnedCount}");
                };
            //}

            // Update TimeOfDay
            //var ToD = mapObjects.Item1.Types.TimeOfDay.

            // Update Hatched Eggs
            var hatchedEggResponse = mapObjects.Item3;
            if (hatchedEggResponse.Success)
            {
                //OnEggHatched?.Invoke(null, hatchedEggResponse);

                for (var i = 0; i < hatchedEggResponse.PokemonId.Count; i++)
                {
                    Logger.Write("Egg Hatched");
                    await UpdateInventory();

                    //TODO: Fix hatching of more than one pokemon at a time
                    var currentPokemon = PokemonsInventory
                        .FirstOrDefault(item => item.Id == hatchedEggResponse.PokemonId[i]);

                    if (currentPokemon == null)
                        continue;

                    ConfirmationDialog dialog = new Views.ConfirmationDialog(string.Format(
                        Resources.CodeResources.GetString("EggHatchMessage"),
                            currentPokemon.PokemonId, hatchedEggResponse.StardustAwarded[i], hatchedEggResponse.CandyAwarded[i],
                            hatchedEggResponse.ExperienceAwarded[i]));
                    dialog.Show();
                    //await
                    //    new MessageDialog(string.Format(
                    //        Resources.CodeResources.GetString("EggHatchMessage"),
                    //        currentPokemon.PokemonId, hatchedEggResponse.StardustAwarded[i], hatchedEggResponse.CandyAwarded[i],
                    //        hatchedEggResponse.ExperienceAwarded[i])).ShowAsyncQueue();

                    BootStrapper.Current.NavigationService.Navigate(typeof(PokemonDetailPage), new SelectedPokemonNavModel()
                    {
                        SelectedPokemonId = currentPokemon.Id.ToString(),
                        ViewMode = PokemonDetailPageViewMode.ReceivedPokemon
                    });
                }
            }
        }

        private static async Task<Geoposition> GetInitialLocation()
        {
            var accessStatus = await Geolocator.RequestAccessAsync();
            Geolocator _geolocator;

            switch (accessStatus)
            {
                case GeolocationAccessStatus.Allowed:
                    _geolocator = new Geolocator { DesiredAccuracy = PositionAccuracy.Default };
                    return await _geolocator.GetGeopositionAsync();
                default:
                    return null;
            }
        }

        #endregion

        #region Map & Position

        private static Geoposition _lastGeopositionMapObjectsRequest;

        /// <summary>
        ///     Gets updated map data based on provided position
        /// </summary>
        /// <param name="geoposition"></param>
        /// <returns></returns>
        private static async
            Task
                <
                    Tuple
                        <GetMapObjectsResponse, CheckChallengeResponse, GetHatchedEggsResponse, GetInventoryResponse, CheckAwardedBadgesResponse,
                            DownloadSettingsResponse>> GetMapObjects(Geoposition geoposition)
        {
            _lastGeopositionMapObjectsRequest = geoposition;
            return await _client.Map.GetMapObjects();
        }

        /// <summary>
        ///		Gets updated incense Pokemon data based on provided position
        /// </summary>
        /// <param name="geoposition"></param>
        /// <returns></returns>
        private static async
            Task
                <GetIncensePokemonResponse> GetIncensePokemons(Geoposition geoposition)
        {
            return await _client.Map.GetIncensePokemons();
        }

        #endregion

        #region Player Data & Inventory

        /// <summary>
        ///     List of items that can be used when trying to catch a Pokemon
        /// </summary>
        public static readonly List<ItemId> CatchItemIds = new List<ItemId>
        {
            ItemId.ItemPokeBall,
            ItemId.ItemGreatBall,
            ItemId.ItemBlukBerry,
            ItemId.ItemMasterBall,
            ItemId.ItemNanabBerry,
            ItemId.ItemPinapBerry,
            ItemId.ItemRazzBerry,
            ItemId.ItemUltraBall,
            ItemId.ItemWeparBerry
        };

        /// <summary>
        /// List of items, that can be used from the normal ItemsInventoryPage
        /// </summary>
        public static readonly List<ItemId> NormalUseItemIds = new List<ItemId>
        {
            ItemId.ItemPotion,
            ItemId.ItemSuperPotion,
            ItemId.ItemHyperPotion,
            ItemId.ItemMaxPotion,
            ItemId.ItemRevive,
            ItemId.ItemMaxRevive,
            ItemId.ItemLuckyEgg,
            ItemId.ItemIncenseOrdinary,
            ItemId.ItemIncenseSpicy,
            ItemId.ItemIncenseCool,
            ItemId.ItemIncenseFloral,
        };

        /// <summary>
        ///     Gets user's profile
        /// </summary>
        /// <returns></returns>
        public static async Task UpdateProfile()
        {
            PlayerData = (await _client.Player.GetPlayer()).PlayerData;
        }

        public static async Task<GetPlayerProfileResponse> GetPlayerProfile(string playerName)
        {
            return await _client.Player.GetPlayerProfile(playerName);
        }

        /// <summary>
        ///     Gets player's inventoryDelta
        /// </summary>
        /// <returns></returns>
        public static async Task<LevelUpRewardsResponse> UpdatePlayerStats(bool checkForLevelUp = false)
        {
            InventoryDelta = (await _client.Inventory.GetInventory()).InventoryDelta;

            var tmpStats =
                InventoryDelta.InventoryItems.First(item => item.InventoryItemData.PlayerStats != null)
                    .InventoryItemData.PlayerStats;

            if (checkForLevelUp && (PlayerStats == null || PlayerStats.Level != tmpStats.Level))
            {
                PlayerStats = tmpStats;
                var levelUpResponse = await GetLevelUpRewards(tmpStats.Level);
                await UpdateInventory();
                // Set busy to false because initial loading may have left it going until we had PlayerStats
                Busy.SetBusy(false);
                return levelUpResponse;
            }
            PlayerStats = tmpStats;
            // Set busy to false because initial loading may have left it going until we had PlayerStats
            Busy.SetBusy(false);
            return null;
        }

        /// <summary>
        ///     Gets player's inventoryDelta
        /// </summary>
        /// <returns></returns>
        public static async Task<GetInventoryResponse> GetInventory()
        {
            return await _client.Inventory.GetInventory();
        }

        /// <summary>
        ///     Gets the rewards after leveling up
        /// </summary>
        /// <returns></returns>
        public static async Task<LevelUpRewardsResponse> GetLevelUpRewards(int newLevel)
        {
            return await _client.Player.GetLevelUpRewards(newLevel);
        }

        /// <summary>
        ///     Pokedex extra data doesn't change so we can just call this method once.
        /// </summary>
        /// <returns></returns>
        private static async Task UpdateItemTemplates(bool ForceRefresh)
        {
            // Get all the templates
            var itemTemplates = await DataCache.GetAsync("itemTemplates", async () => (await _client.Download.GetItemTemplates()).ItemTemplates, DateTime.Now.AddMonths(1), ForceRefresh);

            // Update Pokedex data
            PokemonSettings = await DataCache.GetAsync(nameof(PokemonSettings), async () =>
            {
                await Task.CompletedTask;
                return itemTemplates.Where(
                    item => item.PokemonSettings != null && item.PokemonSettings.FamilyId != PokemonFamilyId.FamilyUnset)
                    .Select(item => item.PokemonSettings);
            }, DateTime.Now.AddMonths(1), ForceRefresh);

            PokemonUpgradeSettings = await DataCache.GetAsync(nameof(PokemonUpgradeSettings), async () =>
            {
                await Task.CompletedTask;
                return itemTemplates.First(item => item.PokemonUpgrades != null).PokemonUpgrades;
            }, DateTime.Now.AddMonths(1), ForceRefresh);


            // Update Moves data
            MoveSettings = await DataCache.GetAsync(nameof(MoveSettings), async () =>
            {
                await Task.CompletedTask;
                return itemTemplates.Where(item => item.MoveSettings != null)
                                    .Select(item => item.MoveSettings);
            }, DateTime.Now.AddMonths(1), ForceRefresh);

            BadgeSettings = await DataCache.GetAsync(nameof(BadgeSettings), async () =>
            {
                await Task.CompletedTask;
                return itemTemplates.Where(item => item.BadgeSettings != null)
                                    .Select(item => item.BadgeSettings);
            }, DateTime.Now.AddMonths(1), ForceRefresh);

            BattleSettings = await DataCache.GetAsync(nameof(BattleSettings), async () =>
            {
                await Task.CompletedTask;
                return itemTemplates.Where(item => item.BattleSettings != null)
                                    .Select(item => item.BattleSettings);
            }, DateTime.Now.AddMonths(1), ForceRefresh);

            EncounterSettings = await DataCache.GetAsync(nameof(EncounterSettings), async () =>
            {
                await Task.CompletedTask;
                return itemTemplates.Where(item => item.EncounterSettings != null)
                                    .Select(item => item.EncounterSettings);
            }, DateTime.Now.AddMonths(1), ForceRefresh);

            GymLevelSettings = await DataCache.GetAsync(nameof(GymLevelSettings), async () =>
            {
                await Task.CompletedTask;
                return itemTemplates.Where(item => item.GymLevel != null)
                                    .Select(item => item.GymLevel);
            }, DateTime.Now.AddMonths(1), ForceRefresh);

            IapSettings = await DataCache.GetAsync(nameof(IapSettings), async () =>
            {
                await Task.CompletedTask;
                return itemTemplates.Where(item => item.IapSettings != null)
                                    .Select(item => item.IapSettings);
            }, DateTime.Now.AddMonths(1), ForceRefresh);

            ItemSettings = await DataCache.GetAsync(nameof(ItemSettings), async () =>
            {
                await Task.CompletedTask;
                return itemTemplates.Where(item => item.ItemSettings != null)
                                    .Select(item => item.ItemSettings);
            }, DateTime.Now.AddMonths(1), ForceRefresh);

            PlayerLevelSettings = await DataCache.GetAsync(nameof(PlayerLevelSettings), async () =>
            {
                await Task.CompletedTask;
                return itemTemplates.Where(item => item.PlayerLevel != null)
                                    .Select(item => item.PlayerLevel);
            }, DateTime.Now.AddMonths(1), ForceRefresh);

            QuestSettings = await DataCache.GetAsync(nameof(QuestSettings), async () =>
            {
                await Task.CompletedTask;
                return itemTemplates.Where(item => item.QuestSettings != null)
                                    .Select(item => item.QuestSettings);
            }, DateTime.Now.AddMonths(1), ForceRefresh);

            CameraSettings = await DataCache.GetAsync(nameof(CameraSettings), async () =>
            {
                await Task.CompletedTask;
                return itemTemplates.Where(item => item.Camera != null)
                                    .Select(item => item.Camera);
            }, DateTime.Now.AddMonths(1), ForceRefresh);

            IapItemDisplay = await DataCache.GetAsync(nameof(IapItemDisplay), async () =>
            {
                await Task.CompletedTask;
                return itemTemplates.Where(item => item.IapItemDisplay != null)
                                    .Select(item => item.IapItemDisplay);
            }, DateTime.Now.AddMonths(1), ForceRefresh);

            MoveSequenceSettings = await DataCache.GetAsync(nameof(MoveSequenceSettings), async () =>
            {
                await Task.CompletedTask;
                return itemTemplates.Where(item => item.MoveSequenceSettings != null)
                                    .Select(item => item.MoveSequenceSettings);
            }, DateTime.Now.AddMonths(1), ForceRefresh);
        }

        /// <summary>
        ///     Updates inventory data
        /// </summary>
        public static async Task UpdateInventory()
        {
            // Get ALL the items
            var response = await GetInventory();
            var fullInventory = response.InventoryDelta?.InventoryItems;
            // Update items
            ItemsInventory.AddRange(fullInventory.Where(item => item.InventoryItemData.Item != null)
                .GroupBy(item => item.InventoryItemData.Item)
                .Select(item => item.First().InventoryItemData.Item), true);
            CatchItemsInventory.AddRange(
                fullInventory.Where(
                    item =>
                        item.InventoryItemData.Item != null && CatchItemIds.Contains(item.InventoryItemData.Item.ItemId))
                    .GroupBy(item => item.InventoryItemData.Item)
                    .Select(item => item.First().InventoryItemData.Item), true);
            AppliedItems.AddRange(
                fullInventory
                .Where(item => item.InventoryItemData.AppliedItems != null)
                    .SelectMany(item => item.InventoryItemData.AppliedItems.Item)
                    .Select(item => new AppliedItemWrapper(item)), true);

            // Update incbuators
            IncubatorsInventory.AddRange(fullInventory.Where(item => item.InventoryItemData.EggIncubators != null)
                .SelectMany(item => item.InventoryItemData.EggIncubators.EggIncubator)
                .Where(item => item != null), true);

            // Update Pokedex
            PokedexInventory.AddRange(fullInventory.Where(item => item.InventoryItemData.PokedexEntry != null)
                .Select(item => item.InventoryItemData.PokedexEntry), true);

            // Update Pokemons
            PokemonsInventory.AddRange(fullInventory.Select(item => item.InventoryItemData.PokemonData)
                .Where(item => item != null && item.PokemonId > 0), true);
            EggsInventory.AddRange(fullInventory.Select(item => item.InventoryItemData.PokemonData)
                .Where(item => item != null && item.IsEgg), true);

            // Update candies
            CandyInventory.AddRange(from item in fullInventory
                                    where item.InventoryItemData?.Candy != null
                                    where item.InventoryItemData?.Candy.FamilyId != PokemonFamilyId.FamilyUnset
                                    group item by item.InventoryItemData?.Candy.FamilyId into family
                                    select new Candy
                                    {
                                        FamilyId = family.FirstOrDefault().InventoryItemData.Candy.FamilyId,
                                        Candy_ = family.FirstOrDefault().InventoryItemData.Candy.Candy_
                                    }, true);

        }

        public static async Task<GetBuddyWalkedResponse> GetBuddyWalked()
        {
            return await _client.Player.GetBuddyWalked();
        }

        public static async Task<CheckAwardedBadgesResponse> GetNewlyAwardedBadges()
        {
            return await _client.Player.GetNewlyAwardedBadges();
        }

        public static async Task<CollectDailyBonusResponse> CollectDailyBonus()
        {
            return await _client.Player.CollectDailyBonus();
        }

        public static async Task<CollectDailyDefenderBonusResponse> CollectDailyDefenderBonus()
        {
            return await _client.Player.CollectDailyDefenderBonus();
        }

        public static async Task<EquipBadgeResponse> EquipBadge(BadgeType type)
        {
            return await _client.Player.EquipBadge(type);
        }

        public static async Task<SetAvatarResponse> SetAvatar(PlayerAvatar playerAvatar)
        {
            return await _client.Player.SetAvatar(playerAvatar);
        }

        public static async Task<SetContactSettingsResponse> SetContactSetting(ContactSettings contactSettings)
        {
            return await _client.Player.SetContactSetting(contactSettings);
        }

        public static async Task<SetPlayerTeamResponse> SetPlayerTeam(TeamColor teamColor)
        {
            return await _client.Player.SetPlayerTeam(teamColor);
        }

        public static async Task<EncounterTutorialCompleteResponse> EncounterTutorialComplete(PokemonId pokemonId)
        {
            return await _client.Encounter.EncounterTutorialComplete(pokemonId);
        }

        public static async Task<MarkTutorialCompleteResponse> MarkTutorialComplete(TutorialState[] completed_tutorials, bool send_marketing_emails, bool send_push_notifications)
        {
            return await _client.Player.MarkTutorialComplete(completed_tutorials, send_marketing_emails, send_push_notifications);
        }
        #endregion

        #region Pokemon Handling

        #region Pokedex

        /// <summary>
        ///     Gets extra data for the current pokemon
        /// </summary>
        /// <param name="pokemonId"></param>
        /// <returns></returns>
        public static PokemonSettings GetExtraDataForPokemon(PokemonId pokemonId)
        {
            // In case we have not retrieved the game settings yet, do it now.
            if (PokemonSettings.Count() == 0)
            {
                Busy.SetBusy(true, Resources.CodeResources.GetString("RefreshingGameSettings"));
                LoadGameSettingsAsync().GetAwaiter().GetResult();
                Busy.SetBusy(false);
            }
            return PokemonSettings.First(pokemon => pokemon.PokemonId == pokemonId);
        }

        public static IEnumerable<PokemonData> GetFavoritePokemons()
        {
            return PokemonsInventory.Where(i => i.Favorite == 1);
        }

        public static IEnumerable<PokemonData> GetDeployedPokemons()
        {
            return PokemonsInventory.Where(i => !string.IsNullOrEmpty(i.DeployedFortId));
        }

        #endregion

        #region Catching

        /// <summary>
        ///     Encounters the selected Pokemon
        /// </summary>
        /// <param name="encounterId"></param>
        /// <param name="spawnpointId"></param>
        /// <returns></returns>
        public static async Task<EncounterResponse> EncounterPokemon(ulong encounterId, string spawnpointId)
        {
            return await _client.Encounter.EncounterPokemon(encounterId, spawnpointId);
        }

        /// <summary>
        ///     Encounters the selected lured Pokemon
        /// </summary>
        /// <param name="encounterId"></param>
        /// <param name="spawnpointId"></param>
        /// <returns></returns>
        public static async Task<DiskEncounterResponse> EncounterLurePokemon(ulong encounterId, string spawnpointId)
        {
            return await _client.Encounter.EncounterLurePokemon(encounterId, spawnpointId);
        }

        /// <summary>
        ///		Encounters the selected incense pokemon
        /// </summary>
        /// <param name="encounterId"></param>
        /// <param name="spawnpointId"></param>
        /// <returns></returns>
        public static async Task<IncenseEncounterResponse> EncounterIncensePokemon(ulong encounterId, string spawnpointId)
        {
            return await _client.Encounter.EncounterIncensePokemon(encounterId, spawnpointId);
        }

        /// <summary>
        ///     Executes Pokemon catching
        /// </summary>
        /// <param name="encounterId"></param>
        /// <param name="spawnpointId"></param>
        /// <param name="captureItem"></param>
        /// <param name="hitPokemon"></param>
        /// <returns></returns>
        public static async Task<CatchPokemonResponse> CatchPokemon(ulong encounterId, string spawnpointId,
            ItemId captureItem, bool hitPokemon = true)
        {
            var random = new Random();
            return
                await
                    _client.Encounter.CatchPokemon(encounterId, spawnpointId, captureItem, random.NextDouble() * 1.95D,
                        random.NextDouble(), 1, hitPokemon);
        }

        /// <summary>
        ///     Throws a capture item to the Pokemon
        /// </summary>
        /// <param name="encounterId"></param>
        /// <param name="spawnpointId"></param>
        /// <param name="captureItem"></param>
        /// <returns></returns>
        public static async Task<UseItemCaptureResponse> UseCaptureItem(ulong encounterId, string spawnpointId, ItemId captureItem)
        {
            return await _client.Encounter.UseCaptureItem(encounterId, captureItem, spawnpointId);
        }

        /// <summary>
        ///     New method to throws an item to the Pokemon
        /// </summary>
        /// <param name="encounterId"></param>
        /// <param name="spawnpointId"></param>
        /// <param name="captureItem"></param>
        /// <returns></returns>
        public static async Task<UseItemEncounterResponse> UseItemEncounter(ulong encounterId, string spawnpointId, ItemId captureItem)
        {
            return await _client.Encounter.UseItemEncounter(encounterId, captureItem, spawnpointId);
        }

        #endregion

        #region Power Up & Evolving & Transfer & Favorite

        /// <summary>
        ///
        /// </summary>
        /// <param name="pokemon"></param>
        /// <returns></returns>
        public static async Task<UpgradePokemonResponse> PowerUpPokemon(PokemonData pokemon)
        {
            return await _client.Inventory.UpgradePokemon(pokemon.Id);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="pokemon"></param>
        /// <returns></returns>
        public static async Task<EvolvePokemonResponse> EvolvePokemon(PokemonData pokemon)
        {
            return await _client.Inventory.EvolvePokemon(pokemon.Id);
        }

        /// <summary>
        /// Transfers the Pokemon
        /// </summary>
        /// <param name="pokemonId"></param>
        /// <returns></returns>
        public static async Task<ReleasePokemonResponse> TransferPokemon(ulong pokemonId)
        {
            return await _client.Inventory.TransferPokemon(pokemonId);
        }

        /// <summary>
        /// Transfers multiple Pokemons at once
        /// </summary>
        /// <param name="pokemonIds"></param>
        /// <returns></returns>
        public static async Task<ReleasePokemonResponse> TransferPokemons(ulong[] pokemonIds)
        {
            return await _client.Inventory.TransferPokemons(pokemonIds);
        }

        /// <summary>
        /// Favourites/Unfavourites the Pokemon
        /// </summary>
        /// <param name="pokemonId"></param>
        /// <param name="isFavorite"></param>
        /// <returns></returns>
        public static async Task<SetFavoritePokemonResponse> SetFavoritePokemon(ulong pokemonId, bool isFavorite)
        {
            // Cast ulong to long... because Niantic is a bunch of retarded idiots...
            long pokeId = (long)pokemonId;
            return await _client.Inventory.SetFavoritePokemon(pokeId, isFavorite);
        }

        public static async Task<SetBuddyPokemonResponse> SetBuddyPokemon(ulong id)
        {
            return await _client.Player.SetBuddyPokemon(id);
        }

        public static async Task<NicknamePokemonResponse> SetPokemonNickName(ulong pokemonId, string nickName)
        {
            return await _client.Inventory.NicknamePokemon(pokemonId, nickName);
        }

        #endregion

        #endregion

        #region Pokestop Handling

        /// <summary>
        ///     Gets fort data for the given Id
        /// </summary>
        /// <param name="pokestopId"></param>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <returns></returns>
        public static async Task<FortDetailsResponse> GetFort(string pokestopId, double latitude, double longitude)
        {
            return await _client.Fort.GetFort(pokestopId, latitude, longitude);
        }

        /// <summary>
        ///     Searches the given fort
        /// </summary>
        /// <param name="pokestopId"></param>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <returns></returns>
        public static async Task<FortSearchResponse> SearchFort(string pokestopId, double latitude, double longitude)
        {
            return await _client.Fort.SearchFort(pokestopId, latitude, longitude);
        }

        public static async Task<AddFortModifierResponse> AddFortModifier(string pokestopId, ItemId modifierType)
        {
            return await _client.Fort.AddFortModifier(pokestopId, modifierType);
        }
        #endregion

        #region Gym Handling

        /// <summary>
        ///     Gets the details for the given Gym
        /// </summary>
        /// <param name="gymid"></param>
        /// <param name="latitude"></param>
        /// <param name="longitude"></param>
        /// <returns></returns>
        public static async Task<GetGymDetailsResponse> GetGymDetails(string gymid, double latitude, double longitude)
        {
            return await _client.Fort.GetGymDetails(gymid, latitude, longitude);
        }

        /// <summary>
        ///     Deploys a pokemong to the given Gym
        /// </summary>
        /// <param name="fortId"></param>
        /// <param name="pokemonId"></param>
        /// <returns></returns>
        public static async Task<FortDeployPokemonResponse> FortDeployPokemon(string fortId, ulong pokemonId)
        {
            return await _client.Fort.FortDeployPokemon(fortId, pokemonId);
        }

        /// The following _client.Fort methods need implementation:
        /// FortRecallPokemon -> Do we? Pokemons can't be recalled by the player

        public static async Task<StartGymBattleResponse> StartGymBattle(string gymId, ulong defendingPokemonId, IEnumerable<ulong>attackingPokemonIds)
        {
            return await _client.Fort.StartGymBattle(gymId, defendingPokemonId, attackingPokemonIds);
        }

        public static async Task<AttackGymResponse> AttackGym(string fortId, string battleId, List<BattleAction> battleActions, BattleAction lastRetrievedAction)
        {
            return await _client.Fort.AttackGym(fortId, battleId, battleActions, lastRetrievedAction);
        }
        #endregion

        #region Items Handling

        /// <summary>
        ///     Uses the given incense item
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static async Task<UseIncenseResponse> UseIncense(ItemId item)
        {
            return await _client.Inventory.UseIncense(item);
        }

        /// <summary>
        ///     Uses the given XpBoost item
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public static async Task<UseItemXpBoostResponse> UseXpBoost(ItemId item)
        {
            return await _client.Inventory.UseItemXpBoost();
        }

        public static async Task<UseItemReviveResponse> UseItemRevive(ItemId item, ulong pokemonId)
        {
            return await _client.Inventory.UseItemRevive(item, pokemonId);
        }

        public static async Task<UseItemPotionResponse> UseItemPotion(ItemId item, ulong pokemonId)
        {
            return await _client.Inventory.UseItemPotion(item, pokemonId);
        }

        /// <summary>
        ///     Recycles the given amount of the selected item
        /// </summary>
        /// <param name="item"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public static async Task<RecycleInventoryItemResponse> RecycleItem(ItemId item, int amount)
        {
            return await _client.Inventory.RecycleItem(item, amount);
        }

        #endregion

        #region Eggs Handling

        /// <summary>
        ///     Uses the selected incubator on the given egg
        /// </summary>
        /// <param name="incubator"></param>
        /// <param name="egg"></param>
        /// <returns></returns>
        public static async Task<UseItemEggIncubatorResponse> UseEggIncubator(EggIncubator incubator, PokemonData egg)
        {
            return await _client.Inventory.UseItemEggIncubator(incubator.Id, egg.Id);
        }

        /// <summary>
        ///     Gets the incubator used by the given egg
        /// </summary>
        /// <param name="egg"></param>
        /// <returns></returns>
        public static EggIncubator GetIncubatorFromEgg(PokemonData egg)
        {
            return IncubatorsInventory.FirstOrDefault(item => item.Id == null ? false : item.Id.Equals(egg.EggIncubatorId));
        }

        #endregion

        #region Misc


        /// <summary>
        ///     Verifies challenge
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static async Task<VerifyChallengeResponse> VerifyChallenge(string token)
        {
            return await _client.Misc.VerifyChallenge(token);
        }

        /// <summary>
        ///     Claims codename
        /// </summary>
        /// <param name="codename"></param>
        /// <returns></returns>
        public static async Task<ClaimCodenameResponse> ClaimCodename(string codename)
        {
            return await _client.Misc.ClaimCodename(codename);
        }

        /// <summary>
        ///     Sends an echo
        /// </summary>
        /// <returns></returns>
        public static async Task<EchoResponse> SendEcho()
        {
            return await _client.Misc.SendEcho();
        }

        /// <summary>
        ///     Gets Action log
        /// </summary>
        /// <returns></returns>
        public static async Task<SfidaActionLogResponse> GetSfidaActionLog()
        {
            return await _client.Misc.GetSfidaActionLog();
        }
        #endregion

        #endregion
    }
}
