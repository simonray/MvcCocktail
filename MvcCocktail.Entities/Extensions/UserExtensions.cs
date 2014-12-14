using MvcCocktail.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;

namespace MvcCocktail.Entities.Extensions
{
    public static class UserExtensions
    {
        public static IQueryable<AppUser> Any(this System.Data.Entity.DbSet<AppUser> repos)
        {
            return repos
                .OrderByDescending(o => o.Created).OrderBy(o => o.Id);
        }
        
        public static IQueryable<AppUser> All(this System.Data.Entity.DbSet<AppUser> repos)
        {
            return repos.Any()
                .Where(o => o.Deleted == false);
        }

        public static async Task<AppUser> FindByIdAsync(this System.Data.Entity.DbSet<AppUser> repos, int id)
        {
            return await repos.All()
                .Where(o => o.Id == id)
                .FirstOrDefaultAsync();
        }

        public static AppUser FindByEmail(this System.Data.Entity.DbSet<AppUser> repos, string email)
        {
            return repos.All()
                .Where(o => o.Email.Equals(email, StringComparison.InvariantCultureIgnoreCase))
                .FirstOrDefault();
        }

        public static async Task<AppUser> FindByEmailAsync(this System.Data.Entity.DbSet<AppUser> repos, string email)
        {
            return await repos.All()
                .Where(o => o.Email.Equals(email, StringComparison.InvariantCultureIgnoreCase))
                .FirstOrDefaultAsync();
        }
    }
}
