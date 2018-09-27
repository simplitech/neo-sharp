using System;
using System.IO;
using NeoSharp.Application.Attributes;
using NeoSharp.Application.Client;
using NeoSharp.Core.Models;
using NeoSharp.Core.SmartContract.Invocation;
using NeoSharp.Core.Types;
using NeoSharp.Core.Wallet.Wrappers;

namespace NeoSharp.Application.Controllers
{
    public class PromptInvokesController : IPromptController
    {
        #region Private fields

        private readonly IConsoleHandler _consoleHandler;
        private readonly IInvocationProcess _invocationProcess;
        private readonly IFileWrapper _fileWrapper;

        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="consoleHandler">Console handler</param>
        public PromptInvokesController(IConsoleHandler consoleHandler, IInvocationProcess invocationProcess, IFileWrapper fileWrapper)
        {
            _consoleHandler = consoleHandler;
            _invocationProcess = invocationProcess;
            _fileWrapper = fileWrapper;
        }

        // testinvoke {contract hash} {params} (--attach-neo={amount}, --attach-gas={amount}) (--from-addr={addr})

        /// <summary>
        /// Invoke contract
        /// </summary>
        /// <param name="contractHash">Contract</param>
        /// <param name="body">Body</param>
        [PromptCommand("invoke", Help = "Invoke a contract", Category = "Invokes")]
        public void Invoke(UInt160 contractHash, [PromptCommandParameterBody] object[] args)
        {
            //Contract contract = Contract.GetContract(contractHash);
            //if (contract == null) throw (new ArgumentNullException("Contract not found"));

            //var tx = contract.CreateInvokeTransaction(args);
        }

        [PromptCommand("invoke test", Help = "Test-invoke a contract", Category = "Invokes")]
        public void TestInvoke(string filepath, [PromptCommandParameterBody] object[] args)
        {
            if(_fileWrapper.Exists(filepath))
            {
                _fileWrapper.Load(filepath);
            }
            else
            {
                _consoleHandler.WriteLine("Contract file not found");
            }
              
            //Contract contract = Contract.GetContract(contractHash);
            //if (contract == null) throw (new ArgumentNullException("Contract not found"));

            //var tx = contract.CreateInvokeTransaction(args);
        }
    }
}