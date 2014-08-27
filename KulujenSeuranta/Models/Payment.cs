using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace KulujenSeuranta.Models
{
    public class Payment
    {
        public int PaymentId { get; set; }

        [Required]
        public decimal Sum { get; set; }

        public string Category { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:dd.MM.yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }

        [Required]
        public virtual ApplicationUser User { get; set; }
    }
}