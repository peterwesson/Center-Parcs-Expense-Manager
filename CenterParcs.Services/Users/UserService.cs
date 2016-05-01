using System.Collections.Generic;
using System.Threading.Tasks;

using CenterParcs.DAL.Identity;
using CenterParcs.DAL.PaymentGroups;
using CenterParcs.DAL.Users;
using CenterParcs.Models.Users;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace CenterParcs.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPaymentGroupRepository _paymentGroupRepository;
        private readonly SignInManager _signInManager;
        private readonly UserManager _userManager;

        public UserService(IUserRepository userRepository, IPaymentGroupRepository paymentGroupRepository, SignInManager signInManager, UserManager userManager)
        {
            this._userRepository = userRepository;
            this._paymentGroupRepository = paymentGroupRepository;
            this._signInManager = signInManager;
            this._userManager = userManager;
        }

        public IList<User> GetAllUsers()
        {
            return this._userRepository.GetAllUsers();
        }

        public User GetUserById(string userId)
        {
            return this._userRepository.GetUserById(userId);
        }

        public User GetUserByUserName(string username)
        {
            return this._userRepository.GetUserByUsername(username);
        }

        public void DeleteUser(User user)
        {
            this._userRepository.DeleteUser(user);
        }

        public void UpdateUser(User user)
        {
            this._userRepository.UpdateUser(user);
        }

        public async Task<SignInStatus> SignIn(string username, string password, bool rememberMe)
        {
            return await this._signInManager.PasswordSignInAsync(username, password, rememberMe, false);
        }

        public async Task SignIn(User user, bool isPersistent = false, bool rememberBrowser = false)
        {
            await this._signInManager.SignInAsync(user, isPersistent, rememberBrowser);
        }

        public async Task<IdentityResult> Register(User user, string password)
        {
            return await this._userManager.CreateAsync(user, password);
        }

        public IList<PaymentGroup> GetAllPaymentGroups()
        {
            return this._paymentGroupRepository.GetAllPaymentGroups();
        }

        public PaymentGroup GetPaymentGroupById(int paymentGroupId)
        {
            return this._paymentGroupRepository.GetPaymentGroupById(paymentGroupId);
        }

        public void AddPaymentGroup(PaymentGroup paymentGroup)
        {
            this._paymentGroupRepository.AddPaymentGroup(paymentGroup);
        }

        public void DeletePaymentGroup(PaymentGroup paymentGroup)
        {
            this._paymentGroupRepository.DeletePaymentGroup(paymentGroup);
        }

        public void UpdatePaymentGroup(PaymentGroup paymentGroup)
        {
            this._paymentGroupRepository.UpdatePaymentGroup(paymentGroup);
        }

        public bool ChangePassword(User user, string password, string newPassword)
        {
            if (_userManager.CheckPassword(user, password))
            {
                _userManager.ChangePassword(user.Id, password, newPassword);

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}