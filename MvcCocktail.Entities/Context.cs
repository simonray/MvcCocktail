using MvcCocktail.Domain.Models;
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
        }

        public override int SaveChanges()
        {
            try
            {
                Commit();
                return base.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var validationErrors in e.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                        System.Diagnostics.Debug.WriteLine("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                }
                throw e;
            }
        }

        public override Task<int> SaveChangesAsync()
        {
            try
            {
                Commit();
                return base.SaveChangesAsync();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var validationErrors in e.EntityValidationErrors)
                {
                    foreach (var validationError in validationErrors.ValidationErrors)
                        System.Diagnostics.Debug.WriteLine("Property: {0} Error: {1}", validationError.PropertyName, validationError.ErrorMessage);
                }
                throw e;
            }
        }

        private void Commit()
        {
            var modifiedEntries = this.ChangeTracker.Entries()
                .Where(o => o.Entity is IBaseEntity &&
                    (o.State == EntityState.Added || o.State == EntityState.Modified));

            foreach (var entry in modifiedEntries)
            {
                IBaseEntity entity = entry.Entity as IBaseEntity;
                if (entity != null)
                {
                    if (entry.State == EntityState.Added)
                        entity.Created = DateTime.Now;
                    else
                        entity.Modified = DateTime.Now;
                }
            }
        }
    }
}
