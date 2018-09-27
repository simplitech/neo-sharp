using System;
using System.Text;
using NeoSharp.Core.Types;
using NeoSharp.VM;

namespace NeoSharp.Core.SmartContract.Invocation
{
    public interface IInvocationProcess
    {
        /// <summary>
        ///Invoke the specified script with the provided parameters
        /// </summary>
        /// <returns>The invoke.</returns>
        /// <param name="scriptHash">Script hash.</param>
        /// <param name="operation">Operation.</param>
        /// <param name="parameters">Parameters.</param>
        /// <param name="scriptTable">Script table.</param>
        /// <param name="trigger">Trigger.</param>
        /// <param name="logBuilder">Log builder.</param>
        InvocationResult TestInvoke(UInt160 scriptHash, string operation, object[] parameters, IScriptTable scriptTable, ETriggerType trigger, StringBuilder logBuilder = null);

    }
}
