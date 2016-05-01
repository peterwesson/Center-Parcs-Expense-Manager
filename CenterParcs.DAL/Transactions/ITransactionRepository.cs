using System.Collections.Generic;

using CenterParcs.Models.Transactions;

namespace CenterParcs.DAL.Transactions
{
    public interface ITransactionRepository
    {
        IList<Transaction> GetAllTransactions();

        Transaction GetTransactionsById(int transactionId);

        void AddTransaction(Transaction transaction);

        void DeleteTransaction(Transaction transaction);

        void UpdateTransaction(Transaction transaction);
    }
}
