using System;
using NeoSharp.VM;

namespace NeoSharp.Core.SmartContract.Invocation
{
    public interface IInvocationResult
    {
        byte[] ExecutedScript { get; }
        EVMState ResultingState { get; }
        ulong GasCost { get; }
        IStackItemsStack ResultStack { get; }
        object ResultingObject();

    }
}
