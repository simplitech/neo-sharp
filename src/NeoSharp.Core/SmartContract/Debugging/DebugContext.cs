using System.Collections.Generic;
using System.Linq;
using NeoSharp.Core.Cryptography;
using NeoSharp.Core.Types;


namespace NeoSharp.Core.SmartContract.Debugging
{
	public class DebugContext : IDebugContext
    {
        private readonly Dictionary<UInt160, byte[]> VirtualContracts = new Dictionary<UInt160, byte[]>();

        public IEnumerable<UInt160> ListVirtualContracts()
        {
            return VirtualContracts.Keys.ToArray();
        }

        public void ClearVirtualContracts()
        {
            VirtualContracts.Clear();
        }

        public byte[] GetScript(byte[] scriptHash, bool isDynamicInvoke)
        {
            var hash = new UInt160(scriptHash);

            if (VirtualContracts.TryGetValue(hash, out var script))
            {
                return script;
            }

            return null;
        }

        public bool TryAddVirtualContract(byte[] script, out UInt160 hash)
        {
            hash = new UInt160(Crypto.Default.Hash160(script));
            return VirtualContracts.TryAdd(hash, script);
        }

        public bool ContainsScript(UInt160 scripthash)
        {
            return ListVirtualContracts().Contains(scripthash);
        }
    }
}
