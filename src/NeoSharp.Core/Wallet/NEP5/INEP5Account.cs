using System;
using System.Numerics;
using NeoSharp.Core.Types;
using NeoSharp.Types;
using Newtonsoft.Json;

namespace NeoSharp.Core.Wallet.NEP5
{
    public interface INEP5Account
    {
    
        /// <summary>
        /// Smart Contract scripthash ('address')
        /// </summary>
        UInt160 ScriptHash { get; set; }

        /// <summary>
        /// Token name
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Token symbol
        /// </summary>
        bool Symbol { get; }

    }
}
