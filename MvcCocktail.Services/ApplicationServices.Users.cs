using MvcCocktail.Domain.Models;
using MvcCocktail.Entities.Extensions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcCocktail.Services
{
    public partial class ApplicationServices
    {
        public AppUser GetUser(string email)
        {
            using (var context = NewContext)
            {
                return context.Users.FindByEmail(email);
            }
        }
        
        public async Task<AppUser> GetUserAsync(int id)
        {
            using (var context = NewContext)
            {
                return await context.Users.FindByIdAsync(id);
            }
        }

        public async Task<AppUser> GetUserAsync(string email)
        {
            using (var context = NewContext)
            {
                return await context.Users.FindByEmailAsync(email);
            }
        }

        public async Task<AppUser> GetApiUserAsync(string token)
        {
            using (var context = NewContext)
            {
                return await context.Users.All().Where(o => o.ApiToken == token).FirstOrDefaultAsync();
            }
        }

        public async Task<IPartialCollection<AppUser>> GetActiveUsersAsync(int page, int pageSize)
        {
            using (var context = NewContext)
            {
                return await PartialCollection<AppUser>
                    .ToPartial(context.Users.All().Where(o => o.Activated == true), page, pageSize);
            }
        }

        public async Task<AppUser> CreateUserAsync(AppUser newUser)
        {
            using (var context = NewContext)
            {
                if (await context.Users.FindByEmailAsync(newUser.Email) != null)
                    throw new InvalidOperationException("Unable to create user as there is already a user with this email address.");
                context.Users.Add(newUser);
                await context.SaveChangesAsync();
                return newUser;
            }
        }

        public async Task UpdateUserAsync(AppUser caller, AppUser updateUser)
        {
            using (var context = NewContext)
            {
                AppUser findUser = await context.Users.FindByIdAsync(updateUser.Id);
                if (findUser == null)
                    throw new InvalidOperationException("Unable to update user, not found");
                if (caller.Id != findUser.Id && caller.Role != AppRole.Administrator)
                    throw new InvalidOperationException("You are not authorized to delete this user.");
                context.Entry(findUser).CurrentValues.SetValues(updateUser);
                await context.SaveChangesAsync();
            }
        }
    }
}
