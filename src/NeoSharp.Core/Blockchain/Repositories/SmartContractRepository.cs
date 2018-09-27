using System;
using System.Threading.Tasks;
using NeoSharp.Core.Models;
using NeoSharp.Core.Persistence;
using NeoSharp.Core.Types;

namespace NeoSharp.Core.Blockchain.Repositories
{
    public class SmartContractRepository : ISmartContractRepository
    {
        #region Private Fields 
        private readonly IRepository _repository;
        #endregion

        public SmartContractRepository(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<Contract> GetSmartContract(UInt160 scriptHash)
        {
            return await _repository.GetContract(scriptHash);
        }
    }
}
