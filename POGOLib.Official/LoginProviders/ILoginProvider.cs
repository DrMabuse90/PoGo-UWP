﻿using POGOLib.Official.Net.Authentication.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POGOLib.Official.LoginProviders
{
    public interface ILoginProvider
    {
        /// <summary>
        /// The unique identifier of this <see cref="ILoginProvider"/>.
        /// </summary>
        string ProviderId { get; }

        /// <summary>
        /// Only used for informational purposes. Could be an email, username, etc.
        /// </summary>
        string UserId { get; }

        /// <summary>
        /// The method to obtain an <see cref="AccessToken"/> using this <see cref="ILoginProvider"/>.
        /// </summary>
        /// <returns>Returns an <see cref="AccessToken"/>.</returns>
        Task<AccessToken> GetAccessToken();
    }
}