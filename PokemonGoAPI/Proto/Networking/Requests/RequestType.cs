// Generated by the protocol buffer compiler.  DO NOT EDIT!
// source: RequestType.proto
#pragma warning disable 1591, 0612, 3021
#region Designer generated code

using pb = global::Google.Protobuf;
using pbc = global::Google.Protobuf.Collections;
using pbr = global::Google.Protobuf.Reflection;
using scg = global::System.Collections.Generic;
namespace POGOProtos.Networking.Requests {

  /// <summary>Holder for reflection information generated from RequestType.proto</summary>
  public static partial class RequestTypeReflection {

    #region Descriptor
    /// <summary>File descriptor for RequestType.proto</summary>
    public static pbr::FileDescriptor Descriptor {
      get { return descriptor; }
    }
    private static pbr::FileDescriptor descriptor;

    static RequestTypeReflection() {
      byte[] descriptorData = global::System.Convert.FromBase64String(
          string.Concat(
            "ChFSZXF1ZXN0VHlwZS5wcm90bxIeUE9HT1Byb3Rvcy5OZXR3b3JraW5nLlJl",
            "cXVlc3RzKsENCgtSZXF1ZXN0VHlwZRIQCgxNRVRIT0RfVU5TRVQQABIRCg1Q",
            "TEFZRVJfVVBEQVRFEAESDgoKR0VUX1BMQVlFUhACEhEKDUdFVF9JTlZFTlRP",
            "UlkQBBIVChFET1dOTE9BRF9TRVRUSU5HUxAFEhsKF0RPV05MT0FEX0lURU1f",
            "VEVNUExBVEVTEAYSIgoeRE9XTkxPQURfUkVNT1RFX0NPTkZJR19WRVJTSU9O",
            "EAcSHgoaUkVHSVNURVJfQkFDS0dST1VORF9ERVZJQ0UQCBIPCgtGT1JUX1NF",
            "QVJDSBBlEg0KCUVOQ09VTlRFUhBmEhEKDUNBVENIX1BPS0VNT04QZxIQCgxG",
            "T1JUX0RFVEFJTFMQaBIMCghJVEVNX1VTRRBpEhMKD0dFVF9NQVBfT0JKRUNU",
            "UxBqEhcKE0ZPUlRfREVQTE9ZX1BPS0VNT04QbhIXChNGT1JUX1JFQ0FMTF9Q",
            "T0tFTU9OEG8SEwoPUkVMRUFTRV9QT0tFTU9OEHASEwoPVVNFX0lURU1fUE9U",
            "SU9OEHESFAoQVVNFX0lURU1fQ0FQVFVSRRByEhEKDVVTRV9JVEVNX0ZMRUUQ",
            "cxITCg9VU0VfSVRFTV9SRVZJVkUQdBIQCgxUUkFERV9TRUFSQ0gQdRIPCgtU",
            "UkFERV9PRkZFUhB2EhIKDlRSQURFX1JFU1BPTlNFEHcSEAoMVFJBREVfUkVT",
            "VUxUEHgSFgoSR0VUX1BMQVlFUl9QUk9GSUxFEHkSEQoNR0VUX0lURU1fUEFD",
            "SxB6EhEKDUJVWV9JVEVNX1BBQ0sQexIQCgxCVVlfR0VNX1BBQ0sQfBISCg5F",
            "Vk9MVkVfUE9LRU1PThB9EhQKEEdFVF9IQVRDSEVEX0VHR1MQfhIfChtFTkNP",
            "VU5URVJfVFVUT1JJQUxfQ09NUExFVEUQfxIVChBMRVZFTF9VUF9SRVdBUkRT",
            "EIABEhkKFENIRUNLX0FXQVJERURfQkFER0VTEIEBEhEKDFVTRV9JVEVNX0dZ",
            "TRCFARIUCg9HRVRfR1lNX0RFVEFJTFMQhgESFQoQU1RBUlRfR1lNX0JBVFRM",
            "RRCHARIPCgpBVFRBQ0tfR1lNEIgBEhsKFlJFQ1lDTEVfSU5WRU5UT1JZX0lU",
            "RU0QiQESGAoTQ09MTEVDVF9EQUlMWV9CT05VUxCKARIWChFVU0VfSVRFTV9Y",
            "UF9CT09TVBCLARIbChZVU0VfSVRFTV9FR0dfSU5DVUJBVE9SEIwBEhAKC1VT",
            "RV9JTkNFTlNFEI0BEhgKE0dFVF9JTkNFTlNFX1BPS0VNT04QjgESFgoRSU5D",
            "RU5TRV9FTkNPVU5URVIQjwESFgoRQUREX0ZPUlRfTU9ESUZJRVIQkAESEwoO",
            "RElTS19FTkNPVU5URVIQkQESIQocQ09MTEVDVF9EQUlMWV9ERUZFTkRFUl9C",
            "T05VUxCSARIUCg9VUEdSQURFX1BPS0VNT04QkwESGQoUU0VUX0ZBVk9SSVRF",
            "X1BPS0VNT04QlAESFQoQTklDS05BTUVfUE9LRU1PThCVARIQCgtFUVVJUF9C",
            "QURHRRCWARIZChRTRVRfQ09OVEFDVF9TRVRUSU5HUxCXARIWChFTRVRfQlVE",
            "RFlfUE9LRU1PThCYARIVChBHRVRfQlVERFlfV0FMS0VEEJkBEhUKEEdFVF9B",
            "U1NFVF9ESUdFU1QQrAISFgoRR0VUX0RPV05MT0FEX1VSTFMQrQISEwoOQ0xB",
            "SU1fQ09ERU5BTUUQkwMSDwoKU0VUX0FWQVRBUhCUAxIUCg9TRVRfUExBWUVS",
            "X1RFQU0QlQMSGwoWTUFSS19UVVRPUklBTF9DT01QTEVURRCWAxIWChFMT0FE",
            "X1NQQVdOX1BPSU5UUxD0AxIUCg9DSEVDS19DSEFMTEVOR0UQ2AQSFQoQVkVS",
            "SUZZX0NIQUxMRU5HRRDZBBIJCgRFQ0hPEJoFEhsKFkRFQlVHX1VQREFURV9J",
            "TlZFTlRPUlkQvAUSGAoTREVCVUdfREVMRVRFX1BMQVlFUhC9BRIXChJTRklE",
            "QV9SRUdJU1RSQVRJT04QoAYSFQoQU0ZJREFfQUNUSU9OX0xPRxChBhIYChNT",
            "RklEQV9DRVJUSUZJQ0FUSU9OEKIGEhEKDFNGSURBX1VQREFURRCjBhIRCgxT",
            "RklEQV9BQ1RJT04QpAYSEQoMU0ZJREFfRE9XU0VSEKUGEhIKDVNGSURBX0NB",
            "UFRVUkUQpgYSHwoaTElTVF9BVkFUQVJfQ1VTVE9NSVpBVElPTlMQpwYSHgoZ",
            "U0VUX0FWQVRBUl9JVEVNX0FTX1ZJRVdFRBCoBmIGcHJvdG8z"));
      descriptor = pbr::FileDescriptor.FromGeneratedCode(descriptorData,
          new pbr::FileDescriptor[] { },
          new pbr::GeneratedClrTypeInfo(new[] {typeof(global::POGOProtos.Networking.Requests.RequestType), }, null));
    }
    #endregion

  }
  #region Enums
  public enum RequestType {
    /// <summary>
    ///  No implementation required
    /// </summary>
    [pbr::OriginalName("METHOD_UNSET")] MethodUnset = 0,
    /// <summary>
    ///  Implemented [R &amp; M]
    /// </summary>
    [pbr::OriginalName("PLAYER_UPDATE")] PlayerUpdate = 1,
    /// <summary>
    ///  Implemented [R &amp; M]
    /// </summary>
    [pbr::OriginalName("GET_PLAYER")] GetPlayer = 2,
    /// <summary>
    ///  Implemented [R &amp; M]
    /// </summary>
    [pbr::OriginalName("GET_INVENTORY")] GetInventory = 4,
    /// <summary>
    ///  Implemented [R &amp; M]
    /// </summary>
    [pbr::OriginalName("DOWNLOAD_SETTINGS")] DownloadSettings = 5,
    /// <summary>
    ///  Implemented [R &amp; M]
    /// </summary>
    [pbr::OriginalName("DOWNLOAD_ITEM_TEMPLATES")] DownloadItemTemplates = 6,
    /// <summary>
    ///  Implemented [R &amp; M]
    /// </summary>
    [pbr::OriginalName("DOWNLOAD_REMOTE_CONFIG_VERSION")] DownloadRemoteConfigVersion = 7,
    /// <summary>
    ///  Implemented [R &amp; M]
    /// </summary>
    [pbr::OriginalName("REGISTER_BACKGROUND_DEVICE")] RegisterBackgroundDevice = 8,
    /// <summary>
    ///  Implemented [R &amp; M]
    /// </summary>
    [pbr::OriginalName("FORT_SEARCH")] FortSearch = 101,
    /// <summary>
    ///  Implemented [R &amp; M]
    /// </summary>
    [pbr::OriginalName("ENCOUNTER")] Encounter = 102,
    /// <summary>
    ///  Implemented [R &amp; M]
    /// </summary>
    [pbr::OriginalName("CATCH_POKEMON")] CatchPokemon = 103,
    /// <summary>
    ///  Implemented [R &amp; M]
    /// </summary>
    [pbr::OriginalName("FORT_DETAILS")] FortDetails = 104,
    /// <summary>
    ///  Can't find this one
    /// </summary>
    [pbr::OriginalName("ITEM_USE")] ItemUse = 105,
    /// <summary>
    ///  Implemented [R &amp; M]
    /// </summary>
    [pbr::OriginalName("GET_MAP_OBJECTS")] GetMapObjects = 106,
    /// <summary>
    ///  Implemented [R &amp; M]
    /// </summary>
    [pbr::OriginalName("FORT_DEPLOY_POKEMON")] FortDeployPokemon = 110,
    /// <summary>
    ///  Implemented [R &amp; M]
    /// </summary>
    [pbr::OriginalName("FORT_RECALL_POKEMON")] FortRecallPokemon = 111,
    /// <summary>
    ///  Implemented [R &amp; M]
    /// </summary>
    [pbr::OriginalName("RELEASE_POKEMON")] ReleasePokemon = 112,
    /// <summary>
    ///  Implemented [R &amp; M]
    /// </summary>
    [pbr::OriginalName("USE_ITEM_POTION")] UseItemPotion = 113,
    /// <summary>
    ///  Implemented [R &amp; M]
    /// </summary>
    [pbr::OriginalName("USE_ITEM_CAPTURE")] UseItemCapture = 114,
    /// <summary>
    ///  Can't find this one
    /// </summary>
    [pbr::OriginalName("USE_ITEM_FLEE")] UseItemFlee = 115,
    /// <summary>
    ///  Implemented [R &amp; M]
    /// </summary>
    [pbr::OriginalName("USE_ITEM_REVIVE")] UseItemRevive = 116,
    /// <summary>
    ///  Not yet implemented in the game
    /// </summary>
    [pbr::OriginalName("TRADE_SEARCH")] TradeSearch = 117,
    /// <summary>
    ///  Not yet implemented in the game
    /// </summary>
    [pbr::OriginalName("TRADE_OFFER")] TradeOffer = 118,
    /// <summary>
    ///  Not yet implemented in the game
    /// </summary>
    [pbr::OriginalName("TRADE_RESPONSE")] TradeResponse = 119,
    /// <summary>
    ///  Not yet implemented in the game
    /// </summary>
    [pbr::OriginalName("TRADE_RESULT")] TradeResult = 120,
    /// <summary>
    ///  Implemented [R &amp; M]
    /// </summary>
    [pbr::OriginalName("GET_PLAYER_PROFILE")] GetPlayerProfile = 121,
    /// <summary>
    ///  Can't find this one
    /// </summary>
    [pbr::OriginalName("GET_ITEM_PACK")] GetItemPack = 122,
    /// <summary>
    ///  Can't find this one
    /// </summary>
    [pbr::OriginalName("BUY_ITEM_PACK")] BuyItemPack = 123,
    /// <summary>
    ///  Can't find this one
    /// </summary>
    [pbr::OriginalName("BUY_GEM_PACK")] BuyGemPack = 124,
    /// <summary>
    ///  Implemented [R &amp; M]
    /// </summary>
    [pbr::OriginalName("EVOLVE_POKEMON")] EvolvePokemon = 125,
    /// <summary>
    ///  Implemented [R &amp; M]
    /// </summary>
    [pbr::OriginalName("GET_HATCHED_EGGS")] GetHatchedEggs = 126,
    /// <summary>
    ///  Implemented [R &amp; M]
    /// </summary>
    [pbr::OriginalName("ENCOUNTER_TUTORIAL_COMPLETE")] EncounterTutorialComplete = 127,
    /// <summary>
    ///  Implemented [R &amp; M]
    /// </summary>
    [pbr::OriginalName("LEVEL_UP_REWARDS")] LevelUpRewards = 128,
    /// <summary>
    ///  Implemented [R &amp; M]
    /// </summary>
    [pbr::OriginalName("CHECK_AWARDED_BADGES")] CheckAwardedBadges = 129,
    /// <summary>
    ///  Implemented [R &amp; M]
    /// </summary>
    [pbr::OriginalName("USE_ITEM_GYM")] UseItemGym = 133,
    /// <summary>
    ///  Implemented [R &amp; M]
    /// </summary>
    [pbr::OriginalName("GET_GYM_DETAILS")] GetGymDetails = 134,
    /// <summary>
    ///  Implemented [R &amp; M]
    /// </summary>
    [pbr::OriginalName("START_GYM_BATTLE")] StartGymBattle = 135,
    /// <summary>
    ///  Implemented [R &amp; M]
    /// </summary>
    [pbr::OriginalName("ATTACK_GYM")] AttackGym = 136,
    /// <summary>
    ///  Implemented [R &amp; M]
    /// </summary>
    [pbr::OriginalName("RECYCLE_INVENTORY_ITEM")] RecycleInventoryItem = 137,
    /// <summary>
    ///  Implemented [R &amp; M]
    /// </summary>
    [pbr::OriginalName("COLLECT_DAILY_BONUS")] CollectDailyBonus = 138,
    /// <summary>
    ///  Implemented [R &amp; M]
    /// </summary>
    [pbr::OriginalName("USE_ITEM_XP_BOOST")] UseItemXpBoost = 139,
    /// <summary>
    ///  Implemented [R &amp; M]
    /// </summary>
    [pbr::OriginalName("USE_ITEM_EGG_INCUBATOR")] UseItemEggIncubator = 140,
    /// <summary>
    ///  Implemented [R &amp; M]
    /// </summary>
    [pbr::OriginalName("USE_INCENSE")] UseIncense = 141,
    /// <summary>
    ///  Implemented [R &amp; M]
    /// </summary>
    [pbr::OriginalName("GET_INCENSE_POKEMON")] GetIncensePokemon = 142,
    /// <summary>
    ///  Implemented [R &amp; M]
    /// </summary>
    [pbr::OriginalName("INCENSE_ENCOUNTER")] IncenseEncounter = 143,
    /// <summary>
    ///  Implemented [R &amp; M]
    /// </summary>
    [pbr::OriginalName("ADD_FORT_MODIFIER")] AddFortModifier = 144,
    /// <summary>
    ///  Implemented [R &amp; M]
    /// </summary>
    [pbr::OriginalName("DISK_ENCOUNTER")] DiskEncounter = 145,
    /// <summary>
    ///  Implemented [R &amp; M]
    /// </summary>
    [pbr::OriginalName("COLLECT_DAILY_DEFENDER_BONUS")] CollectDailyDefenderBonus = 146,
    /// <summary>
    ///  Implemented [R &amp; M]
    /// </summary>
    [pbr::OriginalName("UPGRADE_POKEMON")] UpgradePokemon = 147,
    /// <summary>
    ///  Implemented [R &amp; M]
    /// </summary>
    [pbr::OriginalName("SET_FAVORITE_POKEMON")] SetFavoritePokemon = 148,
    /// <summary>
    ///  Implemented [R &amp; M]
    /// </summary>
    [pbr::OriginalName("NICKNAME_POKEMON")] NicknamePokemon = 149,
    /// <summary>
    ///  Implemented [R &amp; M]
    /// </summary>
    [pbr::OriginalName("EQUIP_BADGE")] EquipBadge = 150,
    /// <summary>
    ///  Implemented [R &amp; M]
    /// </summary>
    [pbr::OriginalName("SET_CONTACT_SETTINGS")] SetContactSettings = 151,
    /// <summary>
    ///  Implemented [R &amp; M]
    /// </summary>
    [pbr::OriginalName("SET_BUDDY_POKEMON")] SetBuddyPokemon = 152,
    /// <summary>
    ///  Implemented [R &amp; M]
    /// </summary>
    [pbr::OriginalName("GET_BUDDY_WALKED")] GetBuddyWalked = 153,
    /// <summary>
    ///  Implemented [R &amp; M]
    /// </summary>
    [pbr::OriginalName("GET_ASSET_DIGEST")] GetAssetDigest = 300,
    /// <summary>
    ///  Implemented [R &amp; M]
    /// </summary>
    [pbr::OriginalName("GET_DOWNLOAD_URLS")] GetDownloadUrls = 301,
    /// <summary>
    ///  Implemented [R &amp; M]
    /// </summary>
    [pbr::OriginalName("CLAIM_CODENAME")] ClaimCodename = 403,
    /// <summary>
    ///  Implemented [R &amp; M]
    /// </summary>
    [pbr::OriginalName("SET_AVATAR")] SetAvatar = 404,
    /// <summary>
    ///  Implemented [R &amp; M]
    /// </summary>
    [pbr::OriginalName("SET_PLAYER_TEAM")] SetPlayerTeam = 405,
    /// <summary>
    ///  Implemented [R &amp; M]
    /// </summary>
    [pbr::OriginalName("MARK_TUTORIAL_COMPLETE")] MarkTutorialComplete = 406,
    /// <summary>
    ///  Can't find this one
    /// </summary>
    [pbr::OriginalName("LOAD_SPAWN_POINTS")] LoadSpawnPoints = 500,
    /// <summary>
    ///  Implemented [R &amp; M]
    /// </summary>
    [pbr::OriginalName("CHECK_CHALLENGE")] CheckChallenge = 600,
    /// <summary>
    ///  Implemented [R &amp; M]
    /// </summary>
    [pbr::OriginalName("VERIFY_CHALLENGE")] VerifyChallenge = 601,
    /// <summary>
    ///  Implemented [R &amp; M]
    /// </summary>
    [pbr::OriginalName("ECHO")] Echo = 666,
    [pbr::OriginalName("DEBUG_UPDATE_INVENTORY")] DebugUpdateInventory = 700,
    [pbr::OriginalName("DEBUG_DELETE_PLAYER")] DebugDeletePlayer = 701,
    /// <summary>
    ///  Not yet released.
    /// </summary>
    [pbr::OriginalName("SFIDA_REGISTRATION")] SfidaRegistration = 800,
    /// <summary>
    ///  Implemented [R &amp; M]
    /// </summary>
    [pbr::OriginalName("SFIDA_ACTION_LOG")] SfidaActionLog = 801,
    /// <summary>
    ///  Not yet released.
    /// </summary>
    [pbr::OriginalName("SFIDA_CERTIFICATION")] SfidaCertification = 802,
    /// <summary>
    ///  Not yet released.
    /// </summary>
    [pbr::OriginalName("SFIDA_UPDATE")] SfidaUpdate = 803,
    /// <summary>
    ///  Not yet released.
    /// </summary>
    [pbr::OriginalName("SFIDA_ACTION")] SfidaAction = 804,
    /// <summary>
    ///  Not yet released.
    /// </summary>
    [pbr::OriginalName("SFIDA_DOWSER")] SfidaDowser = 805,
    /// <summary>
    ///  Not yet released.
    /// </summary>
    [pbr::OriginalName("SFIDA_CAPTURE")] SfidaCapture = 806,
    [pbr::OriginalName("LIST_AVATAR_CUSTOMIZATIONS")] ListAvatarCustomizations = 807,
    [pbr::OriginalName("SET_AVATAR_ITEM_AS_VIEWED")] SetAvatarItemAsViewed = 808,
  }

  #endregion

}

#endregion Designer generated code
