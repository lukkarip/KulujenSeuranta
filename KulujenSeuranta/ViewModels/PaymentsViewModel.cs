using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using System.ComponentModel.DataAnnotations;

using KulujenSeuranta.Models;
using KulujenSeuranta.Interfaces;

namespace KulujenSeuranta.ViewModels
{
    public class PaymentsViewModel
    {
        private IPaymentService _paymentService;

        public PaymentsViewModel(IPaymentService paymentService)
        {
            _paymentService = paymentService;
            SearchDate = new SearchDate();
        }

        public SearchDate SearchDate { get; set; }

        public List<Payment> PaymentsByTypeInSelectedMonth
        {
            get { return _paymentService.GetAllSearchDatePayments(SearchDate); }
        }
    }
}