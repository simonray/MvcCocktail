using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcCocktail.Services.Security
{
    /// <summary>
    /// Login result.
    /// </summary>
    public enum LoginState
    {
        /// <summary>
        /// Successfull login.
        /// </summary>
        [Display(Name = "Success")]
        Success,
        /// <summary>
        /// Login failed, either username or password.
        /// </summary>
        [Display(Name = "Invalid")]
        Invalid,
        /// <summary>
        /// Login parameters correct however the user has not been activated.
        /// </summary>
        [Display(Name = "Not Activated")]
        NotActivated,
    }
}
