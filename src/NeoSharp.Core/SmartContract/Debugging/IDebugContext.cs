using System;
using System.Collections.Generic;
using NeoSharp.Core.Types;
using NeoSharp.VM;

namespace NeoSharp.Core.SmartContract.Debugging
{
    public interface IDebugContext : IScriptTable
    {
        bool TryAddVirtualContract(byte[] script, out UInt160 hash);
        bool ContainsScript(UInt160 scripthash);
        IEnumerable<UInt160> ListVirtualContracts(); 
        void ClearVirtualContracts();

    }
}
