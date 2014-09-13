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

using DotNet.Highcharts;


namespace KulujenSeuranta.Controllers
{
    public class StatisticsController : Controller
    {
        // GET: Statistics
        [Authorize(Roles = "canRead")]
        public ActionResult Index()
        {
            IPaymentService paymentService = new PaymentService();
            var statisticsViewModel = new StatisticsViewModel(paymentService);
            statisticsViewModel.SearchDate = new SearchDate { UserInputDate = DateTime.Now.Month + "-" + DateTime.Now.Year };

            return View("MonthlyView", statisticsViewModel);
        }

        [Authorize(Roles = "canRead")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "SearchDate")] StatisticsViewModel statisticsViewModel)
        {
            if (statisticsViewModel.SearchDate.UserInputDate == null)
            {
                return View(statisticsViewModel);
            }

            return View("MonthlyView", statisticsViewModel);
        }
    }
}