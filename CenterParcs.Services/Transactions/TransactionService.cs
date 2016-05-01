using System;
using System.Collections.Generic;

using CenterParcs.DAL.Transactions;
using CenterParcs.Models.Transactions;

namespace CenterParcs.Services.Transactions
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionService(ITransactionRepository transactionRepository)
        {
            this._transactionRepository = transactionRepository;
        }

        public IList<Transaction> GetAllTransactions()
        {
            return this._transactionRepository.GetAllTransactions();
        }

        public Transaction GetTransactionsById(int transactionId)
        {
            return this._transactionRepository.GetTransactionsById(transactionId);
        }

        public void AddTransaction(Transaction transaction)
        {
            transaction.CreationTime = DateTime.Now;

            this._transactionRepository.AddTransaction(transaction);
        }

        public void DeleteTransaction(Transaction transaction)
        {
            this._transactionRepository.DeleteTransaction(transaction);
        }

        public void UpdateTransaction(Transaction transaction)
        {
            this._transactionRepository.UpdateTransaction(transaction);
        }
    }
}
