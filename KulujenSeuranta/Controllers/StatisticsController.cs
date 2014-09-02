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

            ViewBag.Total = statisticsViewModel.SumOfAllPayments;
            Highcharts chart = statisticsViewModel.CreateChart;

            return View(chart);
        }
    }
}