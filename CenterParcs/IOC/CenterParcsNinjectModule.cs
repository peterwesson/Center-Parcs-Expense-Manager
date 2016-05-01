using System.Web;

using CenterParcs.DAL.DbContexts;
using CenterParcs.DAL.PaymentGroups;
using CenterParcs.DAL.Transactions;
using CenterParcs.DAL.Users;
using CenterParcs.Models.Users;
using CenterParcs.Services.Transactions;
using CenterParcs.Services.Users;

using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;

using Ninject;
using Ninject.Modules;
using Ninject.Web.Common;

namespace CenterParcs.IOC
{
    public class CenterParcsNinjectModule : NinjectModule
    {
        public override void Load()
        {
            this.Bind<IUserService>().To<UserService>().InRequestScope();
            this.Bind<IUserRepository>().To<UserRepository>().InRequestScope();
            this.Bind<IPaymentGroupRepository>().To<PaymentGroupRepository>().InRequestScope();

            this.Bind<ITransactionService>().To<TransactionService>().InRequestScope();
            this.Bind<ITransactionRepository>().To<TransactionRepository>().InRequestScope();

            this.Bind<CenterParcsDbContext>().ToSelf().InSingletonScope();
            this.Bind<IUserStore<User>>().To<UserStore<User>>().InRequestScope()
                .WithConstructorArgument("context", this.Kernel.Get<CenterParcsDbContext>());

            this.Bind<UserManager<User>>().ToSelf().InRequestScope();
            this.Bind<IAuthenticationManager>()
                .ToMethod(c => HttpContext.Current.GetOwinContext().Authentication)
                .InRequestScope();
        }
    }
}