using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

using CenterParcs.DAL.DbContexts;
using CenterParcs.Models.Transactions;

namespace CenterParcs.DAL.Transactions
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly CenterParcsDbContext _centerParcsDbContext;

        public TransactionRepository(CenterParcsDbContext centerParcsDbContext)
        {
            _centerParcsDbContext = centerParcsDbContext;
        }

        public IList<Transaction> GetAllTransactions()
        {
            var dbQuery = _centerParcsDbContext.Transactions.OrderByDescending(t => t.CreationTime);

            return dbQuery.ToList();
        }

        public Transaction GetTransactionsById(int transactionId)
        {
            var dbQuery = _centerParcsDbContext.Transactions.Where(t => t.TransactionId == transactionId);

            return dbQuery.FirstOrDefault();
        }
        
        public void AddTransaction(Transaction transaction)
        {
            _centerParcsDbContext.Entry(transaction).State = EntityState.Added;

            _centerParcsDbContext.SaveChanges();
        }

        public void DeleteTransaction(Transaction transaction)
        {
            _centerParcsDbContext.Entry(transaction).State = EntityState.Deleted;

            _centerParcsDbContext.SaveChanges();
        }

        public void UpdateTransaction(Transaction transaction)
        {
            _centerParcsDbContext.Entry(transaction).State = EntityState.Modified;

            foreach (var subTransaction in transaction.SubTransactions)
            {
                _centerParcsDbContext.Entry(subTransaction).State = subTransaction.SubTransactionId == 0 ? EntityState.Added : EntityState.Modified;
            }

            var originalSubtransactions =
                _centerParcsDbContext.SubTransactions.Where(s => s.TransactionId == transaction.TransactionId).ToList();

            var deletedSubTransactions = originalSubtransactions
                    .Where(st1 => transaction.SubTransactions.All(st2 => st1.SubTransactionId != st2.SubTransactionId));

            foreach (var subTransaction in deletedSubTransactions)
            {
                _centerParcsDbContext.Entry(subTransaction).State = EntityState.Deleted;
            }

            _centerParcsDbContext.SaveChanges();
        }
    }
}