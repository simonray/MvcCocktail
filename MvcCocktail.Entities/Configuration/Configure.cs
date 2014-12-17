using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.SqlServer;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcCocktail.Entities.Configuration
{
    class Configure : DbConfiguration
    {
        public Configure()
        {
            this.SetProviderServices(SqlProviderServices.ProviderInvariantName, SqlProviderServices.Instance);
            this.SetDatabaseInitializer(new Seeder());
        }
    }
}
