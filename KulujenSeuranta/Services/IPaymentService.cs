using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using KulujenSeuranta.Models;
using KulujenSeuranta.ViewModels;

namespace KulujenSeuranta.Services
{
    public interface IPaymentService
    {
        Dictionary<Categories, decimal> GetPaymentsByType(SearchDate searchDate);
        List<Payment> GetAllSearchDatePayments(SearchDate searchDate);
    }
}