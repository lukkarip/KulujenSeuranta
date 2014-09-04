using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using KulujenSeuranta.Models;
using Microsoft.AspNet.Identity;

using KulujenSeuranta.ViewModels;

namespace KulujenSeuranta.Controllers
{
    public class PaymentController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Payment
        [Authorize(Roles = "canEdit")]
        public ActionResult Index()
        {
            var paymentsViewModel = new PaymentsViewModel();
            // Default search date is always current month & year
            paymentsViewModel.SearchDate = new SearchDate { UserInputDate = DateTime.Now.Month + "-" + DateTime.Now.Year };

            return View(paymentsViewModel);
        }

        [Authorize(Roles = "canEdit")]
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
        [Authorize(Roles = "canEdit")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Payment payment = db.Payments.Find(id);

            if (payment == null)
            {
                return HttpNotFound();
            }

            return View(payment);
        }

        // GET: Payment/Create
        [Authorize(Roles = "canEdit")]
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
            AddCurrentUserToPayment(payment);
            ModelState.Clear();
            TryValidateModel(payment);

            if (ModelState.IsValid)
            {
                db.Payments.Add(payment);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(payment);
        }

        // GET: Payment/Edit/5
        [Authorize(Roles = "canEdit")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Payment payments = db.Payments.Find(id);

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
        public ActionResult Edit([Bind(Include = "PaymentId,Sum,Category,Date,Created,CreatedUser,Modified,ModifiedUser")] Payment payment)
        {
            AddCurrentUserToPayment(payment);
            ModelState.Clear();
            TryValidateModel(payment);

            if (ModelState.IsValid)
            {
                db.Entry(payment).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(payment);
        }

        // GET: Payment/Delete/5
        [Authorize(Roles = "canEdit")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Payment payments = db.Payments.Find(id);

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
            Payment payments = db.Payments.Find(id);
            db.Payments.Remove(payments);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }

            base.Dispose(disposing);
        }

        private void AddCurrentUserToPayment(Payment payment)
        {
            string currentUserId = User.Identity.GetUserId();
            ApplicationUser currentUser = db.Users.FirstOrDefault(x => x.Id == currentUserId);
            payment.User = currentUser;
        }
    }
}
