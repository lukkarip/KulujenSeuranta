using System;
using System.ComponentModel.DataAnnotations;
using KulujenSeuranta.Helpers;
using Resources.Models;

using System.Collections.Generic;
using System.Web.Mvc;

using System.Linq.Expressions;

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

    public class Payment : BaseEntity
    {
        public int PaymentId { get; set; }
        [RequiredLocalized]
        [RegularExpression(@"^[0-9]{1,6}(\,[0-9]{0,2})?$", 
                           ErrorMessageResourceType = typeof(CustomErrors), ErrorMessageResourceName = "errorSumInvalid")]
        public decimal Sum { get; set; }
        [RequiredLocalized]
        [Range(1, 13, ErrorMessageResourceType = typeof(CustomErrors), ErrorMessageResourceName =  "errorCategoryIncorrect")]
        public Categories Category { get; set; }
        [RequiredLocalized]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        [RequiredLocalized]
        public virtual ApplicationUser User { get; set; }
    }

    public class RequiredLocalized : RequiredAttribute, System.Web.Mvc.IClientValidatable
    {
        public RequiredLocalized()
        {
            ErrorMessageResourceType = typeof(CustomErrors);
            ErrorMessageResourceName = "errorRequired";
        }
    
        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            return new[] { new ModelClientValidationRule 
                               {                          
                                   ErrorMessage = CustomErrors.errorRequired, 
                                   ValidationType = "required" 
                               } 
                         };
        }
    }
     
    // public static class koe
    // {
    //     public static string GetNameOf<T>(Expression<Func<T>> property)
    //     {
    //         return (property.Body as MemberExpression).Member.Name;
    //     }
    // }
}