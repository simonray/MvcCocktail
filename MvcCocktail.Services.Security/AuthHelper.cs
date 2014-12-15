using MvcCocktail.Core.Utilities;
using MvcCocktail.Domain.Models;
using MvcCocktail.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace System.Web.Mvc
{
    /// <summary>
    /// Authentication helper methods.
    /// </summary>
    public class AuthHelper
    {
        private const string CACHE_USER_KEY = "MVCCOCKTAIL_AUTHUSER";

        /// <summary>
        /// Get the authenticated user (cached) or null if not logged in.
        /// </summary>
        public static AppUser User
        {
            get
            {
                if (!HttpContext.Current.User.Identity.IsAuthenticated)
                    return null;
                
                if (!CacheObject.Exists(CACHE_USER_KEY))
                    CacheObject.Add(ApplicationServices.Instance.GetUser(HttpContext.Current.User.Identity.Name), CACHE_USER_KEY);
                if (CacheObject.Exists(CACHE_USER_KEY))
                    return CacheObject.Get<AppUser>(CACHE_USER_KEY);
                
                return null;
            }
        }

        /// <summary>
        /// Is the active user authenticated with the system.
        /// </summary>
        public static bool IsAuthenticated
        {
            get { return (User != null); }
        }

        /// <summary>
        /// Is the authenticated user an administrator.
        /// </summary>
        public static bool IsAdministrator
        {
            get { return (IsAuthenticated && User.Role == AppRole.Administrator) ? true : false; }
        }

        /// <summary>
        /// Is the authenticated user an administrator or do they match a given user id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool IsAdminOrMatch(int id)
        {
            return (IsAuthenticated ? (IsAdministrator || User.Id == id) : false);
        }

        /// <summary>
        /// Get the full name of the authenticated user. An empty string is returned if the
        /// user is not logged in.
        /// </summary>
        public static string FullName
        {
            get { return (IsAuthenticated ? User.FullName : string.Empty); }
        }
    }
}
