namespace EnumGenie.Core.Sources
{
    /// <summary>
    /// Entry configurator for sources. Use extension methods for better readability
    /// </summary>
    public class Source
    {
        private readonly EnumGenieGenerator _enumGenie;

        public Source(EnumGenieGenerator enumGenie)
        {
            _enumGenie = enumGenie;
        }

        public EnumGenieGenerator Custom(IEnumSource source)
        {
            _enumGenie.AddSource(source);
            return _enumGenie;
        }
    }
}