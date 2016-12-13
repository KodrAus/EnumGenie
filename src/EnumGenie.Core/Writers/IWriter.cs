using System.Collections.Generic;

namespace EnumGenie.Core.Writers
{
    public interface IWriter
    {
        void Write(IReadOnlyCollection<EnumDefinition> enumDefinitions);
    }
}