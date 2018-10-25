using System;
using System.Numerics;
using NeoSharp.Core.Blockchain.Repositories;
using NeoSharp.Core.Models;
using NeoSharp.Core.SmartContract.ContractParameters;
using NeoSharp.Core.SmartContract.Invocation;
using NeoSharp.Core.Types;
using NeoSharp.Types;

namespace NeoSharp.Core.SmartContract.NEP5
{
	public class NEP5Token : INEP5Token
    {
        private IInvocationProcess _invocationProcess;

        public Contract TokenContract { get; }

        public NEP5Token(IInvocationProcess invocationProcess, Contract tokenContract)
        {
            _invocationProcess = invocationProcess;
            TokenContract = tokenContract;
        }

        public BigInteger BalanceOf(UInt160 addressScriptHash)
        {
            //var methodName = new StringContractParameter("balanceOf");
            //var parameter = new Hash160ContractParameter(addressScriptHash);
            //var parameterArray = new ArrayContractParameter(new ContractParameter[] { parameter });
            //var contractParameters = new ContractParameter[] { methodName, parameterArray };
            //IInvocationResult invocationResult = _invocationProcess.TestInvoke(TokenContract.ScriptHash, contractParameters);
            //if(invocationResult.ResultingState == VM.EVMState.None)
            //{
            //    if(invocationResult.ExecutionResult is BigInteger)
            //    {
            //        return (BigInteger)invocationResult.ExecutionResult;
            //    }
            //}
            return 0;
        }

        public byte Decimals()
        {
            //var methodName = new StringContractParameter("decimals");
            //var parameterArray = new ArrayContractParameter(new ContractParameter[] { });
            //var contractParameters = new ContractParameter[] { methodName, parameterArray };
            //IInvocationResult invocationResult = _invocationProcess.TestInvoke(TokenContract.ScriptHash, contractParameters);
            //if (invocationResult.ResultingState == VM.EVMState.None)
            //{
            //    if (invocationResult.ExecutionResult is BigInteger)
            //    {
            //        return (byte)invocationResult.ExecutionResult;
            //    }
            //}
            return 0;
        }

        public string Name()
        {
            //var methodName = new StringContractParameter("name");
            //var parameterArray = new ArrayContractParameter(new ContractParameter[] { });
            //var contractParameters = new ContractParameter[] { methodName, parameterArray };
            //IInvocationResult invocationResult = _invocationProcess.TestInvoke(TokenContract.ScriptHash, contractParameters);
            //if (invocationResult.ResultingState == VM.EVMState.None)
            //{
            //    if (invocationResult.ExecutionResult is string)
            //    {
            //        return (string)invocationResult.ExecutionResult;
            //    }
            //}
            return "";
        }

        public string Symbol()
        {
            //var methodName = new StringContractParameter("symbol");
            //var parameterArray = new ArrayContractParameter(new ContractParameter[] { });
            //var contractParameters = new ContractParameter[] { methodName, parameterArray };
            //IInvocationResult invocationResult = _invocationProcess.TestInvoke(TokenContract.ScriptHash, contractParameters);
            //if (invocationResult.ResultingState == VM.EVMState.None)
            //{
            //    if (invocationResult.ExecutionResult is string)
            //    {
            //        return (string)invocationResult.ExecutionResult;
            //    }
            //}
            return "";
        }

        public BigInteger TotalSupply()
        {
            //var methodName = new StringContractParameter("totalSupply");
            //var parameterArray = new ArrayContractParameter(new ContractParameter[] { });
            //var contractParameters = new ContractParameter[] { methodName, parameterArray };
            //IInvocationResult invocationResult = _invocationProcess.TestInvoke(TokenContract.ScriptHash, contractParameters);
            //if (invocationResult.ResultingState == VM.EVMState.None)
            //{
            //    if (invocationResult.ExecutionResult is BigInteger)
            //    {
            //        return (BigInteger)invocationResult.ExecutionResult;
            //    }
            //}
            return 0;
        }
    }
}
