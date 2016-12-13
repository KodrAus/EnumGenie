namespace EnumGenie.Core.Describers
{
    public class Describer
    {
        private readonly EnumGenieGenerator _enumGenie;

        public Describer(EnumGenieGenerator enumGenie)
        {
            _enumGenie = enumGenie;
        }

        /// <summary>
        /// Define a custom enum value describer.  It is recommended to use the <c>Custom</c> EXTENSION method
        /// instead, for a simple LINQ-based describer.
        /// </summary>
        public EnumGenieGenerator Custom(IDescriber describer)
        {
            _enumGenie.SetDescriber(describer);
            return _enumGenie;
        }
    }
}