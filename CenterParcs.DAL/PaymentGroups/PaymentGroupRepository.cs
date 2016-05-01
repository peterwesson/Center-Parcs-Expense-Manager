using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

using CenterParcs.DAL.DbContexts;
using CenterParcs.Models.Users;

namespace CenterParcs.DAL.PaymentGroups
{
    public class PaymentGroupRepository : IPaymentGroupRepository
    {
        private readonly CenterParcsDbContext _centerParcsDbContext;

        public PaymentGroupRepository(CenterParcsDbContext centerParcsDbContext)
        {
            _centerParcsDbContext = centerParcsDbContext;
        }

        public IList<PaymentGroup> GetAllPaymentGroups()
        {
            var dbQuery = this._centerParcsDbContext.PaymentGroups.Where(p => p.Users.Any());


            return dbQuery.ToList();
        }

        public PaymentGroup GetPaymentGroupById(int paymentGroupId)
        {
            var dbQuery = _centerParcsDbContext.PaymentGroups.Where(pg => pg.PaymentGroupId == paymentGroupId);

            return dbQuery.FirstOrDefault();
        }

        public void AddPaymentGroup(PaymentGroup paymentGroup)
        {
            _centerParcsDbContext.Entry(paymentGroup).State = EntityState.Added;

            _centerParcsDbContext.SaveChanges();
        }

        public void DeletePaymentGroup(PaymentGroup paymentGroup)
        {
            _centerParcsDbContext.Entry(paymentGroup).State = EntityState.Deleted;

            _centerParcsDbContext.SaveChanges();
        }

        public void UpdatePaymentGroup(PaymentGroup paymentGroup)
        {
            _centerParcsDbContext.Entry(paymentGroup).State = EntityState.Modified;

            _centerParcsDbContext.SaveChanges();
        }
    }
}