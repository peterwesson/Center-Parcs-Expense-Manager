using System.Collections.Generic;

using CenterParcs.Models.Transactions;

namespace CenterParcs.Services.Transactions
{
    public interface ITransactionService
    {
        IList<Transaction> GetAllTransactions();

        Transaction GetTransactionsById(int transactionId);

        void AddTransaction(Transaction transaction);

        void DeleteTransaction(Transaction transaction);

        void UpdateTransaction(Transaction transaction);
    }
}