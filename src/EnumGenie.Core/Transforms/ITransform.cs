namespace EnumGenie.Core.Transforms
{
    public interface ITransform
    {
        EnumDefinition Transform(EnumDefinition other);
    }
}