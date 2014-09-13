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
using KulujenSeuranta.Services;
using Resources.Models;

namespace KulujenSeuranta.ViewModels
{
    public class StatisticsViewModel
    {
        private IPaymentService _paymentService;

        public StatisticsViewModel()
        {
            _paymentService = new PaymentService();
            SearchDate = new SearchDate();
        }

        [Required]
        public SearchDate SearchDate { get; set; }

        public Dictionary<Categories, decimal> PaymentsByTypeInSelectedMonth 
        {
            get { return _paymentService.GetPaymentsByType(SearchDate); }
        }

        public decimal SumOfAllPaymentsInSelectedMonth 
        {
            get { return _paymentService.GetAllSearchDatePayments(SearchDate).Sum(payment => payment.Sum); }
        }

        public Highcharts ChartInSelectedMonth 
        {
            get { return CreateChart(); }
        }

        private Highcharts CreateChart() 
        {
            List<string> xAxisValuesList = new List<string>();
            List<decimal> yAxisValues = new List<decimal>();

            Dictionary<Categories, decimal> paymentsByType = _paymentService.GetPaymentsByType(SearchDate);

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