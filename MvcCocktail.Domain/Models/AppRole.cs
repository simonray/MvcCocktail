using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcCocktail.Domain.Models
{
    /// <summary>
    /// Available roles that can be assigned to a user.
    /// </summary>
    public enum AppRole
    {
        /// <summary>
        /// User role.
        /// </summary>
        [Display(Name = "User")]
        User,
        /// <summary>
        /// Administrator role.
        /// </summary>
        [Display(Name = "Administrator")]
        Administrator,
    }
}
