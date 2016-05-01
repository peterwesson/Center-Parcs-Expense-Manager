using System.Collections.Generic;

namespace CenterParcs.Models.ViewModels.Transactions
{
    public class TransactionViewModel
    {
        public int TransactionId { get; set; }

        public string TransactionDescription { get; set; }

        public decimal Amount { get; set; }

        public ICollection<SubTransactionViewModel> SubTransactions { get; set; }
    }
}
