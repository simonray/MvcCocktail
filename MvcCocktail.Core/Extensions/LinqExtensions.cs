using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System.Linq
{
    public static class LinqExtensions
    {
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (var t in enumerable)
            {
                action(t);
            }
        }

        public static void Swap<T>(ref T lhs, ref T rhs)
        {
            T tmp = lhs; lhs = rhs; rhs = tmp;
        }
    }
}
