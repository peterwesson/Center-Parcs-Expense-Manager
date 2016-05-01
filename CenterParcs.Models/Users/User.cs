using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Threading.Tasks;

using CenterParcs.Models.Transactions;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace CenterParcs.Models.Users
{
    public class User : IdentityUser
    {
        public User()
        {
            this.SubTransactions = new HashSet<SubTransaction>();
            this.Transactions = new HashSet<Transaction>();
        }

        public string FirstName { get; set; }
        public string Surname { get; set; }

        [NotMapped]
        public string FullName
        {
            get
            {
                return string.Format("{0} {1}", this.FirstName, this.Surname);
            }
        }

        public virtual ICollection<SubTransaction> SubTransactions { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }

        public int PaymentGroupId { get; set; }

        public virtual PaymentGroup PaymentGroup { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            return userIdentity;
        }
    }
}