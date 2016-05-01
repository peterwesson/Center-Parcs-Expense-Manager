using System.Data.Entity;

using CenterParcs.Models.Transactions;
using CenterParcs.Models.Users;

using Microsoft.AspNet.Identity.EntityFramework;

namespace CenterParcs.DAL.DbContexts
{
    public class CenterParcsDbContext : IdentityDbContext<User>
    {
        public DbSet<Transaction> Transactions { get; set; }

        public DbSet<SubTransaction> SubTransactions { get; set; }

        public DbSet<PaymentGroup> PaymentGroups { get; set; }

        public static CenterParcsDbContext Create()
        {
            return new CenterParcsDbContext();
        }
    }
}