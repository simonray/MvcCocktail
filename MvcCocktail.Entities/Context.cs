using MvcCocktail.Domain.Models;
using MvcCocktail.Entities.Configuration;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcCocktail.Entities
{
    public class Context : DbContext
    {
        public static Context Instance { get { return new Context(); } }

        public DbSet<AppUser> Users { get; set; }
        public DbSet<Settings> Settings { get; set; }

        public Context()
            : base("DefaultConnection")
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Configurations
                .Add(new UserMapper()
                );
        }

        public override int SaveChanges()
        {
            try
            {
                ApplyEntityRules();
                return base.SaveChanges();
            }
#if DEBUG
            catch (DbEntityValidationException e)
            {
                OutputException(e); throw e;
            }
#endif //DEBUG
            finally { }
        }

        public override Task<int> SaveChangesAsync()
        {        
            try
            {
                ApplyEntityRules();
                return base.SaveChangesAsync();
            }
#if DEBUG
            catch (DbEntityValidationException e)
            {
                OutputException(e); throw e;
            }
#endif //DEBUG
            finally { }
        }

        private void ApplyEntityRules()
        {
            foreach (var entry in this.ChangeTracker.Entries()
                .Where(o => o.Entity is IBaseEntity &&
                    (o.State == EntityState.Added || o.State == EntityState.Modified || o.State == EntityState.Deleted)))
            {
                IBaseEntity entity = (IBaseEntity)entry.Entity;
                if (entry.State == EntityState.Added)
                {
                    entity.Created = DateTime.Now;
                }
                else
                {
                    if (entry.State == EntityState.Deleted)
                    {
                        entry.Reload();
                        entity.Deleted = true;
                    }
                    entity.Modified = DateTime.Now;
                }
            }
        }

        private void OutputException(DbEntityValidationException e)
        {
            foreach (var validationErrors in e.EntityValidationErrors)
                foreach (var validationError in validationErrors.ValidationErrors)
                    System.Diagnostics.Debug.WriteLine("Property: '{0}', Error: '{1}'", validationError.PropertyName, validationError.ErrorMessage);
        }
    }
}
