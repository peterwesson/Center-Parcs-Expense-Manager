using System.ComponentModel.DataAnnotations;

using CenterParcs.Models.Users;

namespace CenterParcs.Models.Transactions
{
    public class SubTransaction
    {
        public int SubTransactionId { get; set; }

        public int TransactionId { get; set; }

        public virtual Transaction Transaction { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }

        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }

        [Timestamp]
        public byte[] RowVersion { get; set; }
    }
}
