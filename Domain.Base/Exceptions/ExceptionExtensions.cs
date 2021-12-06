using System;
using System.Collections.Generic;

namespace Domain.Base.Exceptions
{
    public static class ExceptionExtensions
    {
        private static IEnumerable<Exception> FromHierarchy(this Exception source, Func<Exception, Exception> nextItem, Func<Exception, bool> canContinue)
        {
            for (var current = source; canContinue(current); current = nextItem(current))
            {
                yield return current;
            }
        }

        public static IEnumerable<Exception> FromHierarchy(this Exception source, Func<Exception, Exception> nextItem)
        {
            return FromHierarchy(source, nextItem, s => s != null);
        }
    }
}
