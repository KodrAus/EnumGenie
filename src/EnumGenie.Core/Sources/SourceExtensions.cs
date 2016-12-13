using System;
using System.Collections.Generic;
using System.Reflection;

namespace EnumGenie.Core.Sources
{
    public static class SourceExtensions
    {
        /// <summary>
        /// Manually specify the enums
        /// </summary>
        public static EnumGenieGenerator List(this Source source, IEnumerable<Type> enums)
        {
            return source.Custom(new StaticEnumSource(enums));
        }

        /// <summary>
        /// Reads all enums in an assembly
        /// </summary>
        public static EnumGenieGenerator Assembly(this Source source, Assembly assembly)
        {
            return source.Custom(new AssemblyEnumSource(assembly));
        }
    }
}