using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcCocktail.Domain.Models
{
    /// <summary>
    /// Application settings.
    /// </summary>
    public class Settings : BaseEntity
    {
        /// <summary>
        /// Website name.
        /// </summary>
        public string SiteName { get; set; }
        /// <summary>
        /// Default response email address.
        /// </summary>
        public string SiteEmail { get; set; }
    }
}
