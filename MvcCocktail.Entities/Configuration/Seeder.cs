using MvcCocktail.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Data.Entity.Validation;
using MvcCocktail.Domain.Models;

namespace MvcCocktail.Entities.Configuration
{
    public class Seeder : DropCreateDatabaseIfModelChanges<Context>
    {
        protected override void Seed(Context context)
        {
            context.Settings.Add(GetSettings());
            GetUsers().ForEach(u => context.Users.Add(u)); 
            context.SaveChanges();
        }

        private Settings GetSettings()
        {
            return new Settings()
            {
                SiteName = "MvcCocktail",
                SiteEmail = "noreply@mvccocktail.com",
            };
        }

        private List<AppUser> GetUsers()
        {
            return new List<AppUser> {
                new AppUser("Simon", "Smith", "simon@mvccocktail.com", "pass123", AppRole.Administrator, true),
                new AppUser("John", "Jones", "john@mvccocktail.com", "pass123", AppRole.User, true),
                new AppUser("Leah", "Williams", "leah@mvccocktail.com", "pass123", AppRole.User, true),
                new AppUser("Mike", "Brown", "mike@mvccocktail.com", "pass123", AppRole.User, false),
                new AppUser("Robin", "Taylor", "robin@mvccocktail.com", "pass123", AppRole.User, true),
            };
        }
    }
}

