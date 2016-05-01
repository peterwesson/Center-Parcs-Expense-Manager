using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using CenterParcs.Models.Users;

namespace CenterParcs.Models.Transactions
{
    public class Transaction
    {
        public Transaction()
        {
            this.SubTransactions = new HashSet<SubTransaction>();
        }

        public int TransactionId { get; set; }

        public TransactionType TransactionType { get; set; }

        public string TransactionDescription { get; set; }

        public DateTime CreationTime { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }

        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }

        public virtual User User { get; set; }

        public virtual ICollection<SubTransaction> SubTransactions { get; set; }
    }
}