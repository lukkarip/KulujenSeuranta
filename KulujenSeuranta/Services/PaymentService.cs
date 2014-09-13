using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;

using KulujenSeuranta.Models;
using KulujenSeuranta.ViewModels;

namespace KulujenSeuranta.Services
{
    public class PaymentService : IPaymentService
    {
        private ApplicationDbContext _db;
        private IEnumerable<Payment> _AllPayments;

        public PaymentService()
        {
            _db = new ApplicationDbContext();
            GetAllPayments();
        }

        private void GetAllPayments()
        {
            _AllPayments = _db.Payments.ToList().Where(p => p.User.Id == HttpContext.Current.User.Identity.GetUserId());
        }

        public Dictionary<Categories, decimal> GetPaymentsByType(SearchDate searchDate)
        {
            var paymentsByTypeInSelectedMonth = new Dictionary<Categories, decimal>();
            List<Payment> payments = GetAllSearchDatePayments(searchDate);
            paymentsByTypeInSelectedMonth = CreatePaymentsByTypeDictionary();

            foreach (Payment payment in payments)
            {
                paymentsByTypeInSelectedMonth[payment.Category] += payment.Sum;
            }

            return paymentsByTypeInSelectedMonth;
        }

        public List<Payment> GetAllSearchDatePayments(SearchDate searchDate)
        {
            var searchDatePayments = new List<Payment>();

            foreach (Payment payment in _AllPayments)
            {
                if (payment.Date.Year == searchDate.Year &&
                    payment.Date.Month == searchDate.Month)
                {
                    searchDatePayments.Add(payment);
                }
            }

            return searchDatePayments;
        }

        private Dictionary<Categories, decimal> CreatePaymentsByTypeDictionary()
        {
            var paymentDictionary = new Dictionary<Categories, decimal>();

            foreach (Categories category in (Categories[])Enum.GetValues(typeof(Categories)))
            {
                paymentDictionary.Add(category, 0);
            }

            return paymentDictionary;
        }
    }
}