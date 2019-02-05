using System;
using System.Collections.Generic;

namespace MvcFactbook.Code.Classes
{
    public class ViewComparer<TSource, TResult> : IEqualityComparer<TSource>
    {
        private Func<TSource, TResult> selector = null;

        public Func<TSource, TResult> Selector
        {
            get => selector;
            set => selector = value;
        }

        public ViewComparer(Func<TSource, TResult> selector)
        {
            Selector = selector;
        }

        public bool Equals(TSource x, TSource y)
        {
            return Selector(x).Equals(Selector(y));
        }

        public int GetHashCode(TSource obj)
        {
            return Selector(obj).GetHashCode();
        }
    }
}
