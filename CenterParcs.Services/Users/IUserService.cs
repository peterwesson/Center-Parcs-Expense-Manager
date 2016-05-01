using System.Collections.Generic;
using System.Threading.Tasks;

using CenterParcs.Models.Users;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace CenterParcs.Services.Users
{
    public interface IUserService
    {
        IList<User> GetAllUsers();

        User GetUserById(string userId);

        User GetUserByUserName(string username);

        void DeleteUser(User user);

        void UpdateUser(User user);

        Task<SignInStatus> SignIn(string username, string password, bool rememberMe);

        Task SignIn(User user, bool isPersistent = false, bool rememberBrowser = false);

        Task<IdentityResult> Register(User user, string password);

        IList<PaymentGroup> GetAllPaymentGroups();

        PaymentGroup GetPaymentGroupById(int paymentGroupId);

        void AddPaymentGroup(PaymentGroup paymentGroup);

        void DeletePaymentGroup(PaymentGroup paymentGroup);

        void UpdatePaymentGroup(PaymentGroup paymentGroup);

        bool ChangePassword(User user, string password, string newPassword);
    }
}
