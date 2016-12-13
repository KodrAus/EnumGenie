using System;
using System.Collections.Generic;

namespace EnumGenie.Core.Sources
{
    public interface IEnumSource
    {
        IEnumerable<Type> EnumTypes { get; }
    }
}