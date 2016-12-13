using System;

namespace EnumGenie.Core.Filters
{
    public interface IEnumFilter
    {
        bool ShouldEmit(Type type);
    }
}