namespace KulujenSeuranta.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using KulujenSeuranta.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using KulujenSeuranta.Settings;

    internal sealed class Configuration : DbMigrationsConfiguration<KulujenSeuranta.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(KulujenSeuranta.Models.ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            AddUserAndRole(context);

        }

        bool AddUserAndRole(KulujenSeuranta.Models.ApplicationDbContext context) 
        {
            IdentityResult ir;
            var rm = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            ir = rm.Create(new IdentityRole("canEdit"));
            var um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            ConfigurationHandler configHandler = new ConfigurationHandler();
            string username = configHandler.GetAppSettingValue("Username");
            string password = configHandler.GetAppSettingValue("Password");

            var user = new ApplicationUser()
            {
                UserName = username
            };

            ir = um.Create(user, password);

            if (ir.Succeeded == false)
            {
                return ir.Succeeded;
            }

            ir = um.AddToRole(user.Id, "canEdit");

            return ir.Succeeded;
        }
    }
}
