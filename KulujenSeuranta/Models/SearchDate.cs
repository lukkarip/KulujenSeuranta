using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KulujenSeuranta.Models
{
    public class SearchDate
    {
        public string UserInputDate { get; set; }

        public int Month
        {
            get
            {
                if (UserInputDate == null)
                {
                    return 0;
                }

                string[] monthAndYear = UserInputDate.Split('-');
                return int.Parse(monthAndYear[0]);
            }
        }

        public int Year
        {
            get
            {
                if (UserInputDate == null)
                {
                    return 0;
                }

                string[] monthAndYear = UserInputDate.Split('-');
                return int.Parse(monthAndYear[1]);
            }
        }
    }
}