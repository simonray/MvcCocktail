using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using MvcCocktail.Core.Utilities;
using MvcCocktail.Domain.Models;
using MvcCocktail.Entities;
using MvcCocktail.Entities.Extensions;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MvcCocktail.Services.Security
{
    public partial class SecurityServices : ISecurityServices
    {
        public static ISecurityServices Instance { get { return new SecurityServices(); } }
        private Context NewContext { get { return Context.Instance; } }

        public async Task<LoginState> LoginAsync(System.Web.HttpRequestBase request, string email, string password, bool remember)
        {
            using (var context = NewContext)
            {
                var findUser = await context.Users.FindByEmailAsync(email);
                if (findUser == null || findUser.Deleted == true)
                    return FakeHashDelay(LoginState.Invalid);
                if (findUser.Activated == false)
                    return FakeHashDelay(LoginState.NotActivated);
                if (findUser.CheckPassword(password) == false)
                    return LoginState.Invalid;
                
                findUser.LastVisited = DateTime.Now;
                await context.SaveChangesAsync();

                var identity = new ClaimsIdentity(
                    new[] { new Claim(ClaimTypes.Email, email), },
                    DefaultAuthenticationTypes.ApplicationCookie,
                    ClaimTypes.Email, ClaimTypes.Role);
                identity.AddClaim(new Claim(ClaimTypes.Role, Enum.GetName(typeof(AppRole), findUser.Role)));

                request.GetOwinContext().Authentication
                    .SignIn(new AuthenticationProperties { IsPersistent = remember, },identity);
            }

            return LoginState.Success;
        }

        public void Logout(HttpRequestBase request)
        {
            request.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        }

        public async Task<string> ResetPasswordAsync(string email)
        {
            using (var context = NewContext)
            {
                var findUser = await context.Users.Any()
                    .Where(o => o.Email.Equals(email, StringComparison.InvariantCultureIgnoreCase))
                    .SingleOrDefaultAsync();
                if (findUser == null || findUser.Deleted == true || findUser.Activated == false)
                    throw new InvalidOperationException("Unable to reset password for the email specified.");
                var password = Generator.RandomPassword(6);
                findUser.SetPassword(password);
                await context.SaveChangesAsync();
                return password;
            }
        }

        public async Task<bool> UpdateUserStateAsync(AppUser updateUser, bool activeState)
        {
            using (var context = NewContext)
            {
                var findUser = await context.Users.Any()
                    .Where(o => o.Email.Equals(updateUser.Email, StringComparison.InvariantCultureIgnoreCase))
                    .SingleOrDefaultAsync();
                if (findUser == null || findUser.Activated == activeState)
                    return false;
                findUser.Activated = activeState;
                await context.SaveChangesAsync();
                return true;
            }
        }

        private static LoginState FakeHashDelay(LoginState state)
        {
            if (state != LoginState.Success)
            {
                System.Threading.Thread.Sleep(3000);
            }
            return state;
        }
    }
}
