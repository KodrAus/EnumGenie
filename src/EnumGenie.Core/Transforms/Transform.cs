namespace EnumGenie.Core.Transforms
{
    /// <summary>
    /// Entry configurator for transforms. Use extension methods for better readability
    /// </summary>
    public class Transform
    {
        private readonly EnumGenieGenerator _enumGenie;

        public Transform(EnumGenieGenerator enumGenie)
        {
            _enumGenie = enumGenie;
        }

        public EnumGenieGenerator Custom(ITransform transform)
        {
            _enumGenie.AddTransform(transform);
            return _enumGenie;
        }
    }
}