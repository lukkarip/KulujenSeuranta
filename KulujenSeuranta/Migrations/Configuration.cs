namespace KulujenSeuranta.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using KulujenSeuranta.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System.Collections.Generic;
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

            CreateAdminUser(context);
            CreateTestAndPreviewUser(context);
        }

        private bool CreateAdminUser(ApplicationDbContext context)
        {
            IdentityResult ir;
            var rm = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            ir = rm.Create(new IdentityRole("canEdit"));
            ir = rm.Create(new IdentityRole("canRead"));
            var um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            var configHandler = new ConfigurationHandler();
            string username = configHandler.GetAppSettingValue("AdminUserName");
            string password = configHandler.GetAppSettingValue("AdminPassword");

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
            ir = um.AddToRole(user.Id, "canRead");

            return ir.Succeeded;
        }

        private bool CreateTestAndPreviewUser(ApplicationDbContext context)
        {
            IdentityResult ir;
            var rm = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            ir = rm.Create(new IdentityRole("canRead"));
            var um = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            ConfigurationHandler configHandler = new ConfigurationHandler();
            string username = configHandler.GetAppSettingValue("TestUserName");
            string password = configHandler.GetAppSettingValue("TestUserPassword");

            var user = new ApplicationUser()
            {
                UserName = username
            };

            ir = um.Create(user, password);

            if (ir.Succeeded == false)
            {
                return ir.Succeeded;
            }

            ir = um.AddToRole(user.Id, "canRead");

            return ir.Succeeded;
        }
    }
}