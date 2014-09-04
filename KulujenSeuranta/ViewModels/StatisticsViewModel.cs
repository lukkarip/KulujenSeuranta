using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using System.ComponentModel.DataAnnotations;

using DotNet.Highcharts;
using DotNet.Highcharts.Options;
using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Helpers;

using KulujenSeuranta.Models;
using KulujenSeuranta.Helpers;
using Resources.Models;

namespace KulujenSeuranta.ViewModels
{
    public class StatisticsViewModel
    {
        private ApplicationDbContext _db;
        private IEnumerable<Payment> _AllPayments;

        public Dictionary<Categories, decimal> PaymentsByTypeInSelectedMonth 
        {
            get { return GetPaymentsByType(); }
        }

        public decimal SumOfAllPaymentsInSelectedMonth 
        {
            get { return CalculateSumOfAllPaymentsInMonth(); }
        }

        public Highcharts ChartInSelectedMonth 
        {
            get { return CreateChart(); }
        }

        [Required]
        public SearchDate SearchDate { get; set; }

        public StatisticsViewModel()
        {
            _db = new ApplicationDbContext();
            GetAllPayments();
            SearchDate = new SearchDate();
        }

        private void GetAllPayments()
        {
            _AllPayments = _db.Payments.ToList().Where(p => p.User.Id == HttpContext.Current.User.Identity.GetUserId());
        }

        private Dictionary<Categories, decimal> GetPaymentsByType() 
        {
            Dictionary<Categories, decimal> paymentsByTypeInSelectedMonth = new Dictionary<Categories, decimal>();
            List<Payment> payments = GetAllSearchDatePayments();
            paymentsByTypeInSelectedMonth = CreatePaymentsByTypeDictionary();

            foreach (Payment payment in payments)
            {
                paymentsByTypeInSelectedMonth[payment.Category] += payment.Sum;
            }

            return paymentsByTypeInSelectedMonth;
        }

        private decimal CalculateSumOfAllPaymentsInMonth()
        {
            return GetAllSearchDatePayments().Sum(payment => payment.Sum);
        }

        private List<Payment> GetAllSearchDatePayments()
        {
            var searchDatePayments = new List<Payment>();

            foreach (Payment payment in _AllPayments)
            {
                if (payment.Date.Year == SearchDate.Year &&
                    payment.Date.Month == SearchDate.Month)
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

        private Highcharts CreateChart() 
        {
            List<string> xAxisValuesList = new List<string>();
            List<decimal> yAxisValues = new List<decimal>();

            Dictionary<Categories, decimal> paymentsByType = GetPaymentsByType();

            foreach (Categories category in (Categories[])Enum.GetValues(typeof(Categories)))
            {
                xAxisValuesList.Add(EnumCustomHelper.GetCategory(category));
                yAxisValues.Add(paymentsByType[category]);
            }

            Highcharts chart = new Highcharts("chart")
                .InitChart(new Chart { DefaultSeriesType = ChartTypes.Column })
                .SetTitle(new Title { Text = ChartTexts.txtMonthlyExpenses })
                //.SetSubtitle(new Subtitle { Text = "" })
                .SetXAxis(new XAxis { Categories = xAxisValuesList.ToArray() })
                .SetYAxis(new YAxis
                {
                    Min = 0,
                    Title = new YAxisTitle { Text = ChartTexts.txtSumOfExpenses }
                })
                .SetLegend(new Legend
                {
                    Layout = Layouts.Vertical,
                    Align = HorizontalAligns.Left,
                    VerticalAlign = VerticalAligns.Top,
                    X = 100,
                    Y = 70,
                    Floating = true,
                    BackgroundColor = new BackColorOrGradient(System.Drawing.ColorTranslator.FromHtml("#FFFFFF")),
                    Shadow = true
                })
                .SetTooltip(new Tooltip { Formatter = @"function() { return ''+ this.x +': '+ this.y +' €'; }" })
                .SetPlotOptions(new PlotOptions
                {
                    Column = new PlotOptionsColumn
                    {
                        PointPadding = 0.2,
                        BorderWidth = 0
                    }
                })
                .SetSeries(new[]
                {
                    new Series { Name = ChartTexts.txtEuros, Data = new Data( yAxisValues.Cast<object>().ToArray() ) },
                });

            return chart;
        }

    }
}