using System.Linq;
using System.Web.Mvc;

using CenterParcs.Models.Transactions;
using CenterParcs.Models.ViewModels.Transactions;
using CenterParcs.Services.Transactions;
using CenterParcs.Services.Users;

using Microsoft.AspNet.Identity;

namespace CenterParcs.Controllers
{
    public class TransactionController : BaseController
    {
        private readonly ITransactionService _transactionService;

        private readonly IUserService _userService;

        public TransactionController(ITransactionService transactionService, IUserService userService)
        {
            _transactionService = transactionService;
            _userService = userService;
        }

        [HttpGet]
        public ActionResult Summary()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetAllTransactions()
        {
            var transactions = _transactionService.GetAllTransactions();

            return JSONCircular(transactions);
        }

        [HttpGet]
        public ActionResult GetAllPaymentGroups()
        {
            var paymentGroups = _userService.GetAllPaymentGroups();

            foreach (var paymentGroup in paymentGroups)
            {
                var credit = paymentGroup.Users.Sum(u => u.Transactions.Sum(t => t.Amount));
                var debit = paymentGroup.Users.Sum(u => u.SubTransactions.Sum(t => t.Amount));

                paymentGroup.Balance = credit - debit;
            }

            return JSONCircular(paymentGroups);
        }

        [HttpPost]
        public ActionResult AddTransaction(TransactionViewModel transactionViewModel)
        {
            var username = User.Identity.GetUserName();
            
            var user = _userService.GetUserByUserName(username);

            var transaction = new Transaction
            {
                Amount = transactionViewModel.Amount,
                TransactionDescription = transactionViewModel.TransactionDescription,
                User = user,
                SubTransactions = transactionViewModel.SubTransactions.Select(s =>
                    new SubTransaction
                    {
                        Amount = s.Amount,
                        User = _userService.GetUserById(s.UserId)
                    }).ToList()
            };

            _transactionService.AddTransaction(transaction);

            return Json(true);
        }

        [HttpPost]
        public ActionResult UpdateTransaction(TransactionViewModel transactionViewModel)
        {
            var transaction = _transactionService.GetTransactionsById(transactionViewModel.TransactionId);

            transaction.Amount = transactionViewModel.Amount;
            transaction.TransactionDescription = transactionViewModel.TransactionDescription;

            foreach (var subTransactionViewModel in transactionViewModel.SubTransactions)
            {
                var subtransaction = transaction.SubTransactions.FirstOrDefault(s => s.UserId == subTransactionViewModel.UserId);

                if (subtransaction == null)
                {
                    transaction.SubTransactions.Add(
                        new SubTransaction
                            {
                                Amount = subTransactionViewModel.Amount,
                                User = _userService.GetUserById(subTransactionViewModel.UserId),
                                Transaction = transaction,
                                UserId = subTransactionViewModel.UserId,
                                TransactionId = transaction.TransactionId
                            });
                }
                else
                {
                    subtransaction.Amount = subTransactionViewModel.Amount;
                }
            }

            var removedSubtransactions = transaction.SubTransactions
                .Where(s => 
                    transactionViewModel.SubTransactions
                    .All(svm => svm.UserId != s.UserId))
                    .ToList();

            foreach (var subtransaction in removedSubtransactions)
            {
                transaction.SubTransactions.Remove(subtransaction);
            }

            _transactionService.UpdateTransaction(transaction);

            return Json(true);
        }

        [HttpPost]
        public ActionResult DeleteTransaction(int transactionId)
        {
            var transaction = _transactionService.GetTransactionsById(transactionId);

            _transactionService.DeleteTransaction(transaction);

            return Json(true);
        }

        [HttpGet]
        public ActionResult GetAllUsers()
        {
            var users = _userService.GetAllUsers();

            return JSONCircular(users);
        }

        [HttpGet]
        public ActionResult GetCurrentUser()
        {
            var username = User.Identity.GetUserName();
            
            var user = _userService.GetUserByUserName(username);

            return JSONCircular(user);
        }
    }
}