using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using KulujenSeuranta.Models;

namespace KulujenSeuranta.Interfaces
{
    public interface IPaymentService
    {
        Payment FindPayment(int? id);
        void AddPayment(Payment payment);
        void ModifyPayment(Payment payment);
        void RemovePayment(Payment payment);
        void DisposeDb();
        void AddCurrentUserToPayment(Payment payment, System.Security.Principal.IPrincipal user);
        Dictionary<Categories, decimal> GetPaymentsByType(SearchDate searchDate);
        List<Payment> GetAllSearchDatePayments(SearchDate searchDate);
    }
}