namespace EnumGenie.Core.Filters
{
    /// <summary>
    /// Entry configurator for filters. Use extension methods for better readability
    /// </summary>
    public class Filter
    {
        private readonly EnumGenieGenerator _enumGenie;

        public Filter(EnumGenieGenerator enumGenie)
        {
            _enumGenie = enumGenie;
        }

        /// <summary>
        /// Define a custom filter.  It is recommended to use the <c>Custom</c> EXTENSION method
        /// instead, for a simple LINQ-based filter.
        /// </summary>
        public EnumGenieGenerator Custom(IEnumFilter filter)
        {
            _enumGenie.AddFilter(filter);
            return _enumGenie;
        }
    }
}