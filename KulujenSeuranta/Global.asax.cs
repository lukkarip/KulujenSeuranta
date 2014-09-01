using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Data.Entity;
using System.Globalization;

using KulujenSeuranta.Infrastructure;
using System.ComponentModel.DataAnnotations;


namespace KulujenSeuranta
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            // This is for overriding frameworks default error messages with localized messages in App_GlobalResources folder.
            ClientDataTypeModelValidatorProvider.ResourceClassKey = "Messages";
            DefaultModelBinder.ResourceClassKey = "Messages";

            Database.SetInitializer<KulujenSeuranta.Models.ApplicationDbContext>(null);
        }

        protected void Application_AcquireRequestState(object sender, EventArgs e)
        {
            // It's important to check wheter session object is ready
            if (HttpContext.Current.Session != null)
            {
                CultureInfo ci = (CultureInfo)this.Session["Culture"];

                //Checking first if there is no value in session and set default language 
                // this can happen for first user's request
                if (ci == null)
                {
                    // Sets default culture to english invariant
                    string langName = "en";

                    // Try to get values from Accept lang HTTP header
                    if (HttpContext.Current.Request.UserLanguages != null &&
                        HttpContext.Current.Request.UserLanguages.Length != 0)
                    {
                        // Gets accepted list
                        langName = HttpContext.Current.Request.UserLanguages[0].Substring(0, 2);
                        ci = new CultureInfo(langName);
                        this.Session["Culture"] = ci;
                    }
                }

                // Finally setting culture for each request
                System.Threading.Thread.CurrentThread.CurrentUICulture = ci;
                System.Threading.Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(new CultureInfo("fi").Name);
            }
        }
    }
}
