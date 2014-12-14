using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcCocktail.Services
{
    public interface IPartialCollection<T>
    {
        IReadOnlyCollection<T> Items { get; }
        int Page { get; }
        int PageSize { get; }
        int TotalCount { get; }
    }

    public class PartialCollection<T> : IPartialCollection<T>
    {
        public IReadOnlyCollection<T> Items { get; private set; }
        public int Page { get; private set; }
        public int PageSize { get; private set; }
        public int TotalCount { get; private set; }

        public static async Task<IPartialCollection<T>> ToPartial(IQueryable<T> source, int page, int pageSize)
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
