using System;

namespace EnumGenie.Core.Filters
{
    public static class FilterExtensions
    {
        /// <summary>
        /// Filters based on a predicate.  Return <c>true</c> to keep the result
        /// </summary>
        public static EnumGenieGenerator Predicate(this Filter filter, Func<Type, bool> predicate)
        {
            return filter.Custom(new PredicateFilter(predicate));
        }
    }
}