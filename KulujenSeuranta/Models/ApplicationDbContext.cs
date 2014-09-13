using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity.Validation;
using System.Linq;
using System.Collections.Generic;
using System;
using System.Web;

namespace KulujenSeuranta.Models
{
    /// <summary>
    /// http://benjii.me/2014/03/track-created-and-modified-fields-automatically-with-entity-framework-code-first/
    /// </summary>
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<KulujenSeuranta.Models.Payment> Payments { get; set; }

        public override int SaveChanges()
        {
            try
            {
                UpdateCreatedAndModifiedData();
                return base.SaveChanges();
            }
            catch (DbEntityValidationException exception)
            {
                string exceptionMessage = HandleDbEntityValidationException(exception);
                throw new DbEntityValidationException(exceptionMessage, exception.EntityValidationErrors);
            }

        }

        private void UpdateCreatedAndModifiedData()
        {
            var entities = ChangeTracker.Entries().Where(x => x.Entity is BaseEntity && (x.State == EntityState.Added ||
                                                                                         x.State == EntityState.Modified));

            var currentUsername = HttpContext.Current != null && HttpContext.Current.User != null
                    ? HttpContext.Current.User.Identity.Name
                    : "Anonymous";

            foreach (var entity in entities)
            {
                if (entity.State == EntityState.Added)
                {
                    ((BaseEntity)entity.Entity).Created = DateTime.Now;
                    ((BaseEntity)entity.Entity).CreatedUser = currentUsername;
                }

                if (entity.State == EntityState.Modified)
                {
                    ((BaseEntity)entity.Entity).Modified = DateTime.Now;
                    ((BaseEntity)entity.Entity).ModifiedUser = currentUsername;
                }
            }
        }

        /// <summary>
        /// Throw a new DbEntityValidationException with the improved exception message.
        /// </summary>
        private string HandleDbEntityValidationException(DbEntityValidationException exception)
        {
            // Retrieve the error messages as a list of strings.
            IEnumerable<string> errorMessages = exception.EntityValidationErrors
                                                         .SelectMany(x => x.ValidationErrors)
                                                         .Select(x => x.ErrorMessage);

            // Join the list to a single string.
            string fullErrorMessage = string.Join("; ", errorMessages);

            // Combine the original exception message with the new one.
            string newExceptionMessage = string.Concat(exception.Message, " The validation errors are: ", fullErrorMessage);

            return newExceptionMessage;
        }

    }
}