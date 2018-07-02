﻿using System;
using NeoSharp.Core.Models;
using NeoSharp.Core.Types;

namespace NeoSharp.Core.Wallet
{
    public interface IWalletAccount 
    {
        /// <summary>
        /// label is a label that the user has made to the account.
        /// </summary>
        String Label { get; set; }

        /// <summary>
        /// isDefault indicates whether the account is the default change
        /// account.
        /// </summary>
        bool IsDefault { get; set; }

        /// <summary>
        /// lock indicates whether the account is locked by user. The client
        /// shouldn't spend the funds in a locked account.
        /// </summary>
        bool Lock { get; set; }

        /// <summary>
        /// key is the private key of the account in the NEP-2 format. This
        /// field can be null (for watch-only address or non-standard address).
        /// </summary>
        String Key { get; }

        /// <summary>
        /// contract is a Contract object which describes the details of the
        /// contract. This field can be null (for watch-only address).
        /// </summary>
        Contract Contract { get; set; }

        /// <summary>
        /// extra is an object that is defined by the implementor of the client
        /// for storing extra data. This field can be null
        /// </summary>
        Object Extra { get; set; }
    }
}
