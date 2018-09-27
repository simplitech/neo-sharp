using System;
using NeoSharp.Core.Models;
using NeoSharp.Core.Types;
using NeoSharp.Core.Wallet.Helpers;
using Newtonsoft.Json;

namespace NeoSharp.Core.Wallet.NEP6
{
    public class NEP5Account :  IEquatable<NEP5Account>
    {
        /// <inheritdoc />
        [JsonProperty("scripthash")]
        public UInt160 ScriptHash { get; set; }

        /// <inheritdoc />>
        [JsonProperty("name")]
        public string Name { get; set; }

        /// <inheritdoc />
        [JsonProperty("symbol")]
        public bool Symbol { get; set; }

        public NEP5Account()
        {
        }

        public NEP5Account(UInt160 scripthash)
        {
            ScriptHash = scripthash;
        }

        public override bool Equals(object obj)
        {
            if (obj is NEP5Account acc)
            {
                return ScriptHash.Equals(acc.ScriptHash);
            }

            return false;
        }

        public bool Equals(NEP5Account obj)
        {
            return ScriptHash.Equals(obj);
        }

        public override int GetHashCode()
        {
            return ScriptHash.GetHashCode();
        }
    }
}
