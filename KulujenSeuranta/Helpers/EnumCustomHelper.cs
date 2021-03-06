﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using KulujenSeuranta.Infrastructure;

namespace KulujenSeuranta.Helpers
{
    public class EnumCustomHelper
    {
        public static string GetCategory(KulujenSeuranta.Models.Categories category)
        {
            string sCategory = KulujenSeuranta.Infrastructure.EnumHelper
                                  .GetLocalizedString<KulujenSeuranta
                                  .Models.Categories>(typeof(Resources.Models.CategoriesTexts), category);

            return sCategory;
        }
    }
}