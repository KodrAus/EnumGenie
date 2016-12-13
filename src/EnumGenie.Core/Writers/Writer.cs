namespace EnumGenie.Core.Writers
{
    /// <summary>
    /// Entry configurator for writers. Use extension methods for better readability
    /// </summary>
    public class Writer
    {
        private readonly EnumGenieGenerator _enumGenie;

        public Writer(EnumGenieGenerator enumGenie)
        {
            _enumGenie = enumGenie;
        }

        internal EnumGenieGenerator Custom(IWriter writer)
        {
            _enumGenie.AddWriter(writer);
            return _enumGenie;
        }
    }
}