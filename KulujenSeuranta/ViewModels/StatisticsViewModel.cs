using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using KulujenSeuranta.Models;

using DotNet.Highcharts;
using DotNet.Highcharts.Options;
using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Helpers;

namespace KulujenSeuranta.ViewModels
{
    public class StatisticsViewModel
    {
        private ApplicationDbContext db;
        private IEnumerable<Payment> AllPayments;

        public Dictionary<Categories, decimal> PaymentsByType { get; private set; }
        public decimal SumOfAllPayments { get; private set; }
        public Highcharts Chart { get; set; }

        public StatisticsViewModel()
        {
            db = new ApplicationDbContext();
            GetAllPayments();
            GetPaymentsByType();
            CalculateSumOfAllPayments();
            CreateChart();
        }

        private void GetAllPayments()
        {
            AllPayments = db.Payments.ToList().Where(p => p.User.Id == HttpContext.Current.User.Identity.GetUserId());
        }

        private void GetPaymentsByType() 
        {
            IEnumerable<Payment> payments = AllPayments;
            PaymentsByType = CreatePaymentsByTypeDictionary();

            foreach (Payment payment in payments)
            {
                PaymentsByType[payment.Category] += payment.Sum;
            }
        }

        private void CalculateSumOfAllPayments()
        {
            SumOfAllPayments = AllPayments.Sum(payment => payment.Sum);
        }

        private void CreateChart() 
        {
            var transactionCounts = new List<TransactionCount> 
            {
                new TransactionCount() { MonthName = "January", Count = 30 },
                new TransactionCount() { MonthName = "February", Count = 40 },
                new TransactionCount() { MonthName = "March", Count = 4 },
                new TransactionCount() { MonthName = "April", Count = 35 },
            };

            // modify dta type to make it of Array type
            var xDataMonths = transactionCounts.Select(i => i.MonthName).ToArray();
            var yDataCounts = transactionCounts.Select(i => new object[] { i.Count }).ToArray();

            Chart = new Highcharts("chart")
                // define the type of chart
                .InitChart(new Chart { DefaultSeriesType = ChartTypes.Line })
                // overall Title of the chart
                .SetTitle(new Title { Text = "Incoming Transactions per hour" })
                // small label below the main Title
                .SetSubtitle(new Subtitle { Text = "Accounting" })
                // load the X values
                .SetXAxis(new XAxis { Categories = xDataMonths })
                // set the Y title
                .SetYAxis(new YAxis { Title = new YAxisTitle { Text = "Number of Transactions" } })
                .SetTooltip(new Tooltip
                {
                    Enabled = true,
                    Formatter = @"function() { return '<b>' + this.series.name + '</b><br/>' + this.x + ': ' + this.y; }"
                })
                .SetPlotOptions(new PlotOptions
                {
                    Line = new PlotOptionsLine
                    {
                        DataLabels = new PlotOptionsLineDataLabels
                        {
                            Enabled = true
                        },
                        EnableMouseTracking = false
                    }
                })
                // Load the Y values
                .SetSeries(new[] {
                new Series { Name = "Hour", Data = new Data(yDataCounts)}

            });
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