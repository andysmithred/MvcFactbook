using System;
using System.Collections.Generic;
using System.Linq;

namespace MvcFactbook.Code.Classes
{
    public static class MyExtensions
    {
        public static IEnumerable<TSource> Distinct<TSource, TCompare>(this IEnumerable<TSource> source, Func<TSource, TCompare> selector)
        {
            return source.Distinct(new ViewComparer<TSource, TCompare>(selector));
        }
    }
}
