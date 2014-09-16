using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity;
using System.Data.Entity;

using KulujenSeuranta.Models;
using KulujenSeuranta.Interfaces;

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

        public Payment FindPayment(int? id)
        {
            return _db.Payments.Find(id);
        }


        public void AddPayment(Payment payment)
        {
            _db.Payments.Add(payment);
            _db.SaveChanges();
        }


        public void ModifyPayment(Payment payment)
        {
            _db.Entry(payment).State = EntityState.Modified;
            _db.SaveChanges();
        }


        public void RemovePayment(Payment payment)
        {
            _db.Payments.Remove(payment);
            _db.SaveChanges();
        }


        public void DisposeDb()
        {
            _db.Dispose();       
        }

        public void AddCurrentUserToPayment(ref Payment payment, System.Security.Principal.IPrincipal user)
        {
            string currentUserId = user.Identity.GetUserId();
            ApplicationUser currentUser = _db.Users.FirstOrDefault(x => x.Id == currentUserId);
            payment.User = currentUser;
        }
    }
}