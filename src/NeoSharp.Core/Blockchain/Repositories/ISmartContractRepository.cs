using System;
using System.Threading.Tasks;
using NeoSharp.Core.Models;
using NeoSharp.Core.Types;
using NeoSharp.Types;

namespace NeoSharp.Core.Blockchain.Repositories
{
    public interface ISmartContractRepository
    {
        /// <summary>
        /// Return the corresponding smart-contract information according to the specified script-hash
        /// </summary>
        /// <param name="hash"></param>
        /// <returns>Contract</returns>
        Task<Contract> GetSmartContract(UInt160 hash);
    }
}
