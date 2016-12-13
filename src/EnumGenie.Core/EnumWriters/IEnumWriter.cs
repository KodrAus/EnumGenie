using System.IO;

namespace EnumGenie.Core.EnumWriters
{
    public interface IEnumWriter
    {
        void WriteTo(Stream stream, EnumDefinition enumDefinition);
    }
}
