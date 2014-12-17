using MvcCocktail.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcCocktail.Services
{
    public partial class ApplicationServices : IApplicationServices
    {
        public static IApplicationServices Instance { get { return new ApplicationServices(); } }
        private Context NewContext { get { return Context.Instance; } }
    }
}
