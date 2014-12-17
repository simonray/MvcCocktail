using MvcCocktail.Domain.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcCocktail.Entities.Extensions
{
    public static class PartialCollectionExtensions
    {
        public static async Task<IPartialCollection<T>> ToPartialAsync<T>(this IQueryable<T> source, int page, int pageSize)
        {
            return new PartialCollection<T>()
            {
                Page = page,
                PageSize = pageSize,
                TotalCount = source.Count(),
                Items = await (source.Skip(page * pageSize).Take(pageSize).ToListAsync()),
            };
        }
    }
}
