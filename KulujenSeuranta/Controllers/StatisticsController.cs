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

namespace KulujenSeuranta.Controllers
{
    public class StatisticsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Statistics
        [Authorize(Roles = "canEdit")]
        public ActionResult Index()
        {
            IEnumerable<Payment> payments = db.Payments.ToList().Where(p => p.User.Id == User.Identity.GetUserId());
            decimal allPayments = payments.Sum(payment => payment.Sum);
            ViewBag.Total = allPayments;

            return View();
        }
    }
}