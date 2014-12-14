using MvcCocktail.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcCocktail.Services
{
    public interface IApplicationServices
    {
        //Settings
        Settings GetSettings();
        Settings GetSettingsCached();
        void UpdateSettings(Settings updateSettings);

        //User
        AppUser GetUser(string email);
        Task<AppUser> GetUserAsync(int id);
        Task<AppUser> GetUserAsync(string email);
        Task<AppUser> GetApiUserAsync(string token);
        Task<IPartialCollection<AppUser>> GetActiveUsersAsync(int page, int pageSize);
        Task<AppUser> CreateUserAsync(AppUser newUser);
        Task UpdateUserAsync(AppUser caller, AppUser updateUser);
    }
}
