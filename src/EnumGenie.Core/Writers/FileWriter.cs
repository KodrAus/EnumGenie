using System.Collections.Generic;
using System.IO;
using EnumGenie.Core.EnumWriters;

namespace EnumGenie.Core.Writers
{
    public class FileWriter : IWriter
    {
        private readonly string _file;
        private readonly IEnumWriter _enumWriter;

        public FileWriter(string file, IEnumWriter enumWriter)
        {
            _file = file;
            _enumWriter = enumWriter;
        }

        public void Write(IReadOnlyCollection<EnumDefinition> enumDefinitions)
        {
            using (var stream = File.Open(_file, FileMode.Truncate, FileAccess.Write))
            {
                foreach (var definition in enumDefinitions)
                {
                    _enumWriter.WriteTo(stream, definition);
                }

                stream.Flush(true);
            }
        }
    }
}