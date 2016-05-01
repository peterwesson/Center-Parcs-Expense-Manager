using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

using CenterParcs.Models.Users;
using CenterParcs.Models.ViewModels.Account;
using CenterParcs.Services.Users;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace CenterParcs.Controllers
{
    [Authorize]
    public class AccountController : BaseController
    {
        private readonly IUserService _userService;

        public AccountController()
        {
        }

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var result = await _userService.SignIn(model.Username, model.Password, model.RememberMe);
            if (result == SignInStatus.Success)
            {
                return RedirectToLocal(returnUrl);  
            }

            ModelState.AddModelError(string.Empty, "Invalid login attempt.");

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ChangePassword(ChangePasswordViewModel model)
        {
            var username = User.Identity.GetUserName();
            var user = _userService.GetUserByUserName(username);

            if (_userService.ChangePassword(user, model.Password, model.NewPassword))
            {
                return RedirectToAction("Index", "Home");
            }

            ModelState.AddModelError(string.Empty, "Unable to change password");

            return View(model);
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(model);
            }

            PaymentGroup paymentGroup;

            if (model.PaymentGroupId.HasValue)
            {
                paymentGroup = _userService.GetPaymentGroupById(model.PaymentGroupId.Value);
            }
            else
            {
                paymentGroup = new PaymentGroup();

                _userService.AddPaymentGroup(paymentGroup);
            }

            var user = new User
            {
                UserName = model.UserName, 
                FirstName = model.FirstName, 
                Surname = model.Surname,
                PaymentGroupId = paymentGroup.PaymentGroupId
            };

            var result = await this._userService.Register(user, model.Password);

            if (result.Succeeded)
            {
                await this._userService.SignIn(user);

                return this.RedirectToAction("Index", "Home");
            }

            this.AddErrors(result);

            return View(model);
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }
    }
}