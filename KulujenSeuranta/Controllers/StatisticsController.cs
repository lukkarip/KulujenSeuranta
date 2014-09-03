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

using DotNet.Highcharts;


namespace KulujenSeuranta.Controllers
{
    public class StatisticsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Statistics
        [Authorize(Roles = "canEdit")]
        public ActionResult Index()
        {
            var statisticsViewModel = new StatisticsViewModel();

            return View(statisticsViewModel);
        }

        [Authorize(Roles = "canEdit")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind(Include = "SearchDate")] StatisticsViewModel statisticsVM)
        {
            if (statisticsVM.SearchDate.UserInputDate == null)
            {
                return View(statisticsVM);
            }

            return View("MonthlyView", statisticsVM);
        }
    }
}