using System;
using System.Collections.Generic;
using System.Text;
using NeoSharp.Core.Models;
using NeoSharp.Core.Types;
using NeoSharp.Wallet;
using System.Linq;
using NeoSharp.VM;
using NeoSharp.Core.Wallet;
using NeoSharp.Core.Blockchain;
using NeoSharp.Core.Persistence;

namespace NeoSharp.Wallet.TransactionManager
{

    // assetId, unspent list
    using UnspentCoinsDictionary = Dictionary<UInt256, List<CoinReference>>;

    public class TransactionManager : ITransactionManager
    {

        IRepository _repository;

        public TransactionManager(IRepository repository){
            _repository = repository;
        }

        /// <summary>
        /// Builds the ClaimTransaction.
        /// </summary>
        /// <returns>The claim transaction.</returns>
        /// <param name="from">From.</param>
        /// <param name="attributes">Attributes.</param>
        public Transaction BuildClaimTransaction(IWalletAccount from, TransactionAttribute[] attributes){

            Transaction transaction = new Transaction(TransactionType.ClaimTransaction);
            transaction.Attributes = attributes ?? new TransactionAttribute[0];
            //TODO: Complete transaction


            return transaction;
        }


        public Transaction BuildClaimTransaction(CoinReference[] inputs, TransactionAttribute[] attributes)
        {
            if(inputs == null || inputs.Length == 0){
                throw new ArgumentException();
            }

            Transaction transaction = new Transaction(TransactionType.ClaimTransaction);
            transaction.Inputs = inputs;
            transaction.Attributes = attributes ?? new TransactionAttribute[0];
            //TODO: Complete transaction

            return transaction;
        }

        public Transaction BuildClaimTransaction(TransactionAttribute[] attributes, CoinReference[] inputs)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Builds the contract transaction.
        /// This is a very common kind of transaction as it allows one wallet to send NEO to another. 
        /// The inputs and outputs transaction fields will usually be important for this transaction 
        /// (for example, to govern how much NEO will be sent, and to what address).
        /// In this method the input will be gathered from the account unspent balance.
        /// </summary>
        /// <returns>The contract transaction.</returns>
        /// <param name="from">From.</param>
        /// <param name="attributes">Attributes.</param>
        /// <param name="outputs">Outputs.</param>
        public Transaction BuildContractTransaction(
            IWalletAccount from,
            TransactionAttribute[] attributes,
            TransactionOutput[] outputs){

            //TODO: Complete transaction
            return new Transaction(TransactionType.ContractTransaction);
        }


        /// <summary>
        /// Builds the contract transaction.
        /// This is a very common kind of transaction as it allows one wallet to send NEO to another. 
        /// The inputs and outputs transaction fields will usually be important for this transaction 
        /// (for example, to govern how much NEO will be sent, and to what address).
        /// </summary>
        /// <returns>The contract transaction.</returns>
        /// <param name="attributes">Attributes.</param>
        /// <param name="inputs">Inputs.</param>
        /// <param name="outputs">Outputs.</param>
        public Transaction BuildContractTransaction(
            TransactionAttribute[] attributes,
            CoinReference[] inputs,
            TransactionOutput[] outputs){

            //TODO: Complete transaction
            return new Transaction(TransactionType.ContractTransaction);
        }


        /// <summary>
        /// Builds the invocation transaction.
        /// </summary>
        /// <returns>The invocation transaction.</returns>
        /// <param name="from">From.</param>
        /// <param name="attributes">Attributes.</param>
        /// <param name="outputs">Outputs.</param>
        /// <param name="script">Script.</param>
        /// <param name="fee">Fee.</param>
        public Transaction BuildInvocationTransaction(
            IWalletAccount from,
            TransactionAttribute[] attributes,
            TransactionOutput[] outputs,
            String script,
            Fixed8 fee = default(Fixed8)){

            //TODO: Complete transaction
            return new Transaction(TransactionType.InvocationTransaction);
        }

        /// <summary>
        /// Builds the invocation transaction.
        /// </summary>
        /// <returns>The invocation transaction.</returns>
        /// <param name="attributes">Attributes.</param>
        /// <param name="inputs">Inputs.</param>
        /// <param name="outputs">Outputs.</param>
        /// <param name="script">Script.</param>
        /// <param name="fee">Fee.</param>
        public Transaction BuildInvocationTransaction(
            TransactionAttribute[] attributes,
            CoinReference[] inputs,
            TransactionOutput[] outputs,
            String script,
            Fixed8 fee = default(Fixed8)){

            //TODO: Complete transaction
            return new Transaction(TransactionType.InvocationTransaction);

        }

        public UnspentCoinsDictionary GetBalance(IWalletAccount from)
        {
            throw new NotImplementedException();
        }

        public UInt256 BroadcastTransaction(IWalletAccount account, Transaction transaction)
        {
            throw new NotImplementedException();
        }

    }
}