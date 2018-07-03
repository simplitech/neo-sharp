using System;
using NeoSharp.Core.Cryptography;
using NeoSharp.Core.Models;
using NeoSharp.Core.Types;
using Newtonsoft.Json;

namespace NeoSharp.Core.Wallet.NEP6
{
    public class NEP6Account : IWalletAccount
    {
        
        public UInt160 ScriptHash => Contract.ScriptHash;
        public String Label { get; set; }
        public bool IsDefault { get; set; }
        public bool Lock { get; set; }
        public String Key { get; set; }
        public Contract Contract { get; set; }

        [JsonProperty("extra")]
        public Object Extra { get; set; }

        //TODO: Refactor
        public object Clone()
        {
            throw new NotImplementedException();
        }
    }
}
