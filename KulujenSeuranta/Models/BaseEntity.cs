using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KulujenSeuranta.Models
{
    public class BaseEntity
    {
        public DateTime Created { get; set; }
        public string CreatedUser { get; set; }
        public DateTime? Modified { get; set; }
        public string ModifiedUser { get; set; }
    }
}