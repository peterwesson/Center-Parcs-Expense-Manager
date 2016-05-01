using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CenterParcs.Models.Users
{
    public class PaymentGroup
    {
        public PaymentGroup()
        {
            this.Users = new HashSet<User>();
        }

        public int PaymentGroupId { get; set; }

        [NotMapped]
        public decimal Balance { get; set; }

        public virtual ICollection<User> Users { get; set; } 
    }
}
