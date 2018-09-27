using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using NeoSharp.Application.Attributes;
using NeoSharp.Application.Client;
using NeoSharp.Core.Cryptography;
using NeoSharp.Core.Extensions;
using NeoSharp.Core.Logging;
using NeoSharp.Core.Models;
using NeoSharp.Core.SmartContract.Debugging;
using NeoSharp.Core.SmartContract.Invocation;
using NeoSharp.Core.Types;
using NeoSharp.VM;
using NeoSharp.VM.Types;

namespace NeoSharp.Application.Controllers
{
    public class PromptDebugController : IPromptController
    {
        #region Private fields

        private readonly ILogBag _logs;
        private readonly ILogger<Prompt> _logger;
        private readonly ILoggerFactoryExtended _log;

        private readonly IVMFactory _vmFactory;
        private readonly IConsoleHandler _consoleHandler;
        private readonly IDebugContext _debugContext;
        private readonly IInvocationProcess _invocationProcess;
        private LogVerbose _logVerbose = LogVerbose.Off;

        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="logBag">Log bag</param>
        /// <param name="logger">Logger</param>
        /// <param name="log">Log</param>
        /// <param name="vmFactory">VM Factory</param>
        /// <param name="consoleHandler">Console handler</param>
        public PromptDebugController
            (
            ILogBag logBag,
            ILogger<Prompt> logger, ILoggerFactoryExtended log,
            IVMFactory vmFactory, IConsoleHandler consoleHandler,
            IDebugContext debugContext, IInvocationProcess invocationProcess
            )
        {
            _logs = logBag;
            _log = log;
            _logger = logger;
            _vmFactory = vmFactory;
            _consoleHandler = consoleHandler;
            _debugContext = debugContext;
            _invocationProcess = invocationProcess;
        }

        /// <summary>
        /// Enable / Disable logs
        /// </summary>
        /// <param name="mode">Mode</param>
        [PromptCommand("log", Help = "Enable/Disable log output", Category = "Usability")]
        public void LogCommand(LogVerbose mode)
        {
            _logVerbose = mode;

            if (mode != LogVerbose.Off)
            {
                _log.OnLog -= Log_OnLog;
                _log.OnLog += Log_OnLog;

                _logger.LogDebug("Log output is enabled");
            }
            else
            {
                _logs.Clear();
                _logger.LogDebug("Log output is disabled");

                _log.OnLog -= Log_OnLog;
            }
        }



        // TODO #401: Implement test invoke with asset attachment
        // testinvoke {contract hash} {params} (--attach-neo={amount}, --attach-gas={amount}) (--from-addr={addr})

        /// <summary>
        /// Add virtual contract
        /// </summary>
        /// <param name="script">Script</param>
        [PromptCommand("virtual contract add", Help = "Add virtual contract to the script table", Category = "Debug")]
        public void VirtualContractAddCommand(byte[] script)
        {
            _debugContext.TryAddVirtualContract(script, out UInt160 contractHash);

            foreach (var h in _debugContext.ListVirtualContracts())
            {
                _consoleHandler.WriteLine(h.ToString(true), h == contractHash ? ConsoleOutputStyle.Information : ConsoleOutputStyle.Output);
            }
        }

        /// <summary>
        /// Add virtual contract
        /// </summary>
        /// <param name="file">File</param>
        [PromptCommand("virtual contract add", Help = "Add virtual contract to the script table", Category = "Debug")]
        public void VirtualContractAddCommand(FileInfo file)
        {
            if (!file.Exists) throw new ArgumentException("File must exists");

            VirtualContractAddCommand(File.ReadAllBytes(file.FullName));
        }

        /// <summary>
        /// Clear virtual contract
        /// </summary>
        [PromptCommand("virtual contract clear", Help = "Clear all virtual smart contracts from the script table", Category = "Debug")]
        public void VirtualContractClearCommand()
        {
            _debugContext.ClearVirtualContracts();
        }

        /// <summary>
        /// List virtual contract
        /// </summary>
        [PromptCommand("virtual contract list", Help = "List all virtual smart contracts on the script table", Category = "Debug")]
        public void VirtualContractListCommand()
        {
            foreach (var h in _debugContext.ListVirtualContracts())
            {
                _consoleHandler.WriteLine(h.ToString(true));
            }
        }

        /// <summary>
        /// Decompile
        /// </summary>
        /// <param name="contractHash">Contract</param>
        [PromptCommand("decompile", Help = "Decompile contract", Category = "Debug")]
        public void DecompileCommand(UInt160 contractHash)
        {
            var script = _debugContext.GetScript(contractHash.ToArray(), false);

            if (script == null) throw (new ArgumentNullException("Contract not found"));

            var parser = new InstructionParser();
            foreach (var i in parser.Parse(script))
            {
                _consoleHandler.Write(i.Location.ToString() + " ", ConsoleOutputStyle.Information);

                if (i is InstructionWithPayload ip)
                {
                    _consoleHandler.Write(i.OpCode.ToString() + " ");
                    _consoleHandler.WriteLine("{" + ip.Payload.ToHexString(true) + "}", ConsoleOutputStyle.DarkGray);
                }
                else
                {
                    _consoleHandler.WriteLine(i.OpCode.ToString());
                }
            }
        }

        /// <summary>
        /// Invoke contract
        /// </summary>
        /// <param name="contractHash">Contract</param>
        /// <param name="trigger">Trigger</param>
        /// <param name="operation">Operation</param>
        /// <param name="parameters">Parameters</param>
        [PromptCommand("testinvoke", Help = "Test invoke contract", Category = "Debug")]
        public void TestInvoke(UInt160 contractHash, ETriggerType trigger, string operation, [PromptCommandParameterBody] object[] parameters = null)
        {

            if(_debugContext.ContainsScript(contractHash))
            {
                throw new ArgumentNullException("Contract not found");
            }


            //IInvocationResult invocationResult = _invocationProcess.TestInvoke();


            //_consoleHandler.WriteObject(invocationResult, PromptOutputStyle.json);

            //_logger.LogDebug("Execution opcodes:" + Environment.NewLine + log.ToString());
        }

        private void Log_OnLog(LogEntry log)
        {
            if (!_logVerbose.HasFlag(LogFlagProxy.Instance[log.Level]))
            {
                return;
            }

            _logs.Add(log);
        }
    }
}