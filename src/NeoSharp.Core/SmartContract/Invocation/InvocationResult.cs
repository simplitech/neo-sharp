using System;
using System.Collections.Generic;
using NeoSharp.Core.Types;
using NeoSharp.VM;

namespace NeoSharp.Core.SmartContract.Invocation
{
    public class InvocationResult : IInvocationResult
    {
        public byte[] ExecutedScript { get; }
        public EVMState ResultingState { get; }
        public ulong GasCost { get; }
        public IStackItemsStack ResultStack { get; }
        private object result;

        public InvocationResult(byte[] executedScript, EVMState resultingState, ulong gasCost, IStackItemsStack resultStack)
        {
            ExecutedScript = executedScript;
            ResultingState = resultingState;
            GasCost = gasCost;
            ResultStack = resultStack;
        }

        public object ResultingObject()
        {
            if(result == null)
            {
                var resultList = new List<object>();
                foreach (var stackItem in ResultStack)
                {
                    using (stackItem)
                    {
                        resultList.Add(stackItem.GetRawObject());
                    }
                }
                result = resultList;
            }

            return result;
        }
    }
}
