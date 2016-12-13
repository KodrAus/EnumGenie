using System;
using System.IO;
using System.Linq;
using System.Reflection;
using EnumGenie.Core;
using EnumGenie.Core.EnumWriters;

namespace EnumGenie.TypeScript
{
    public class EnumDescriptorWriter : IEnumWriter
    {
        public void WriteTo(Stream stream, EnumDefinition enumDefinition)
        {
            var membersExceptFlagsDefault = enumDefinition.EnumType.GetTypeInfo().GetCustomAttribute<FlagsAttribute>() == null
                ? enumDefinition.Members
                : enumDefinition.Members.Where(m => m.Value != 0).ToList();

            var writer = new StreamWriter(stream);
            writer.WriteLine($"export interface I{enumDefinition.Name}Descriptor {{ value: {enumDefinition.Name}; name: string; description: string; }}");
            writer.WriteLine($"export var all{enumDefinition.Name}: I{enumDefinition.Name}Descriptor[] = [");

            writer.Write(string.Join($",{Environment.NewLine}", membersExceptFlagsDefault.Select(m => $"    {Descriptor(enumDefinition, m)}")));
            writer.WriteLine();

            writer.WriteLine("];");

            writer.WriteLine();

            writer.WriteLine($"export function get{enumDefinition.Name}Descriptor(value: {enumDefinition.Name}) {{");
            writer.WriteLine("    switch (value) {");

            foreach (var member in membersExceptFlagsDefault)
            {
                writer.WriteLine($"        case {enumDefinition.Name}.{member.Name}:");
                writer.WriteLine($"            return {Descriptor(enumDefinition, member)};");
            }

            writer.WriteLine("    }");
            writer.WriteLine("}");
            writer.Flush();
        }

        private static string Descriptor(EnumDefinition enumDefinition, EnumMemberDefinition m)
        {
            return $"{{ value: {enumDefinition.Name}.{m.Name}, name: `{m.Name}`, description: `{m.Description}` }}";
        }
    }
}
