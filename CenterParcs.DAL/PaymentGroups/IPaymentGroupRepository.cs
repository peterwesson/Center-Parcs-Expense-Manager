using System.Collections.Generic;

using CenterParcs.Models.Users;

namespace CenterParcs.DAL.PaymentGroups
{
    public interface IPaymentGroupRepository
    {
        IList<PaymentGroup> GetAllPaymentGroups();

        PaymentGroup GetPaymentGroupById(int paymentGroupId);

        void AddPaymentGroup(PaymentGroup paymentGroup);

        void DeletePaymentGroup(PaymentGroup paymentGroup);

        void UpdatePaymentGroup(PaymentGroup paymentGroup);
    }
}