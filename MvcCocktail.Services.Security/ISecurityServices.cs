using MvcCocktail.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MvcCocktail.Services.Security
{
    public interface ISecurityServices
    {
        Task<LoginState> LoginAsync(System.Web.HttpRequestBase request, string email, string password, bool remember);
        void Logout(HttpRequestBase request);
        Task<string> ResetPasswordAsync(string email);
        Task<bool> UpdateUserStateAsync(AppUser updateUser, bool activeState);
    }
}
