using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

using KulujenSeuranta.Models;
using KulujenSeuranta.ViewModels;
using KulujenSeuranta.Interfaces;
using KulujenSeuranta.Services;

namespace KulujenSeuranta.Controllers
{
    public class PaymentController : Controller
    {
        private ApplicationDbContext _db = new ApplicationDbContext();
        IPaymentService _paymentService = new PaymentService();

        // GET: Payment
        [Authorize(Roles = "canRead")]
        public ActionResult Index()
        {
            var paymentsViewModel = new PaymentsViewModel(_paymentService);
            // Default search date is always current month & year
            paymentsViewModel.SearchDate = new SearchDate { UserInputDate = DateTime.Now.Month + "-" + DateTime.Now.Year };
            
            return View(paymentsViewModel);
        }

        [Authorize(Roles = "canRead")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "SearchDate")] PaymentsViewModel paymentsViewModel)
        {
            if (paymentsViewModel.SearchDate.UserInputDate == null)
            {
                return View(paymentsViewModel);
            }

            return View(paymentsViewModel);
        }

        // GET: Payment/Details/5
        [Authorize(Roles = "canRead")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Payment payment = _paymentService.FindPayment(id);

            if (payment == null)
            {
                return HttpNotFound();
            }

            return View(payment);
        }

        // GET: Payment/Create
        [Authorize(Roles = "canRead")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Payment/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "canEdit")]
        public ActionResult Create([Bind(Include = "PaymentId,Sum,Category,Date,User_Id")] Payment payment)
        {
            _paymentService.AddCurrentUserToPayment(ref payment, User);
            ModelState.Clear();
            TryValidateModel(payment);

            if (ModelState.IsValid)
            {
                _paymentService.AddPayment(payment);
                return RedirectToAction("Index");
            }

            return View(payment);
        }

        // GET: Payment/Edit/5
        [Authorize(Roles = "canRead")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Payment payments = _paymentService.FindPayment(id);

            if (payments == null)
            {
                return HttpNotFound();
            }

            return View(payments);
        }

        // POST: Payment/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "canEdit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PaymentId,Sum,Category,Date,User,Created,CreatedUser,Modified,ModifiedUser")] Payment payment)
       {
           string currentUserId = User.Identity.GetUserId();
           ApplicationUser currentUser = _db.Users.FirstOrDefault(x => x.Id == currentUserId);
           payment.User = currentUser;
           //_paymentService.AddCurrentUserToPayment(ref payment, User);

            ModelState.Clear();
            TryValidateModel(payment);

            if (ModelState.IsValid)
            {
                _db.Entry(payment).State = EntityState.Modified;
                _db.SaveChanges();
                //_paymentService.ModifyPayment(payment);
                return RedirectToAction("Index");
            }

            return View(payment);
        }

        // GET: Payment/Delete/5
        [Authorize(Roles = "canRead")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Payment payments = _paymentService.FindPayment(id);

            if (payments == null)
            {
                return HttpNotFound();
            }

            return View(payments);
        }

        // POST: Payment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "canEdit")]
        public ActionResult DeleteConfirmed(int id)
        {
            Payment payment = _paymentService.FindPayment(id);
            _paymentService.RemovePayment(payment);

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _paymentService.DisposeDb();
            }

            base.Dispose(disposing);
        }

    }
}
