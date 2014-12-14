using App.Core.Utilities;
using MvcCocktail.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcCocktail.Services
{
    public partial class ApplicationServices
    {
        private const string CACHE_SETTINGS = "MVCCOCKTAIL_SETTINGS";

        public Settings GetSettings()
        {
            using (var context = NewContext)
            {
                return context.Settings.First();
            }
        }

        public Settings GetSettingsCached()
        {
            if (!CacheObject.Exists(CACHE_SETTINGS))
                CacheObject.Add(GetSettings(), CACHE_SETTINGS);
            if (CacheObject.Exists(CACHE_SETTINGS))
                return CacheObject.Get<Settings>(CACHE_SETTINGS);
            return new Settings();
        }

        public void UpdateSettings(Settings updateSettings)
        {
            using (var context = NewContext)
            {
                Settings findSettings = context.Settings.First();
                context.Entry(findSettings).CurrentValues.SetValues(updateSettings);
                CacheObject.Clear(CACHE_SETTINGS);
                context.SaveChanges();
            }
        }
    }
}
