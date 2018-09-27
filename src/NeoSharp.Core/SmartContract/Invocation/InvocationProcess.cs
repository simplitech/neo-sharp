using System.Collections.Generic;
using System.Text;
using NeoSharp.Core.Extensions;
using NeoSharp.Core.SmartContract.ContractParameters;
using NeoSharp.Core.SmartContract.Debugging;
using NeoSharp.Core.Types;
using NeoSharp.VM;

namespace NeoSharp.Core.SmartContract.Invocation
{
    public class InvocationProcess : IInvocationProcess
    {
        private IVMFactory _vmFactory;

        public InvocationProcess(IVMFactory vmFactory)
        {
            _vmFactory = vmFactory;
        }


        //public InvocationResult TestInvoke(UInt160 scriptHash, string operation, object[] parameters, IScriptTable scriptTable, ETriggerType trigger, StringBuilder logBuilder = null)
        //{
        //    var args = new ExecutionEngineArgs()
        //    {
        //        ScriptTable = scriptTable,
        //        Logger = new ExecutionEngineLogger(ELogVerbosity.StepInto),
        //        Trigger = trigger
        //    };

        //    args.Logger.OnStepInto += (context) =>
        //    {
        //        logBuilder?.AppendLine(context.NextInstruction.ToString());
        //    };

        //    using (var script = new ScriptBuilder())
        //    using (var vm = _vmFactory.Create(args))
        //    {
        //        script.EmitMainPush(operation, parameters);
        //        script.EmitAppCall(scriptHash.ToArray(), false);

        //        vm.LoadScript(script);

        //        var ret = vm.Execute();
        //        return new InvocationResult(script, vm.State, vm.ConsumedGas, vm.ResultStack);
        //    }
        //}

        public InvocationResult TestInvoke(UInt160 scriptHash, string operation, object[] parameters, IScriptTable scriptTable, ETriggerType trigger, StringBuilder logBuilder = null)
        {
            throw new System.NotImplementedException();
        }


        /// <summary>
        /// Builds the invocation script.
        /// </summary>
        /// <returns>The invocation script.</returns>
        /// <param name="scriptHash">Script hash.</param>
        /// <param name="parameters">Parameters.</param>
        private byte[] BuildInvocationScript(UInt160 scriptHash, ContractParameter[] parameters)
        {
            byte[] executionScript;

            using (var sb = new ScriptBuilder())
            {
                for (int i = parameters.Length - 1; i >= 0; i--)
                {
                    sb.PushContractParameter(parameters[i]);
                }
                sb.EmitAppCall(scriptHash.ToArray());
                executionScript = sb.ToArray();
            }

            return executionScript;
        }

        private byte[] BuildInvocationScript(byte[] script, ContractParameter[] parameters)
        {
            byte[] executionScript;

            using (var sb = new ScriptBuilder())
            {
                for (int i = parameters.Length - 1; i >= 0; i--)
                {
                    sb.PushContractParameter(parameters[i]);
                }

                sb.Emit(script);
                executionScript = sb.ToArray();
            }

            return executionScript;
        }
    }
}
