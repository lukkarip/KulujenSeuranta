using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Windows;

using KulujenSeuranta.Helpers;
using Resources.Models;

namespace KulujenSeuranta.Models
{
    [LocalizationEnum(typeof(CategoriesTexts))]
    public enum Categories
    {
        Books = 1,
        Children = 2,
        Clothes = 3,
        ElectricityBill = 4,
        Food = 5,
        Gas = 6,
        Other = 7,
        PhoneBill = 8,
        Presents = 9,
        Rent = 10,
        Restaurant = 11,
        TrainAndBusTickets = 12,
        Travelling = 13
    }

    public class Payment
    {
        public int PaymentId { get; set; }
        [Required]
        public decimal Sum { get; set; }
        [Required, Range(1, 13, ErrorMessageResourceType = typeof(Resources.Models.CustomErrors), 
                                ErrorMessageResourceName =  "errorCategoryIncorrect")]
        public Categories Category { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        [Required]
        public virtual ApplicationUser User { get; set; }
    }
}