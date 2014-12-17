using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvcCocktail.Domain.Models
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
        public IReadOnlyCollection<T> Items { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
    }
}
