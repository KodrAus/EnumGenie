using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using EnumGenie.Core.Describers;
using EnumGenie.Core.Filters;
using EnumGenie.Core.Sources;
using EnumGenie.Core.Transforms;
using EnumGenie.Core.Writers;

namespace EnumGenie.Core
{
    /// <summary>
    /// Entry point to describe how to read and outputs enums.  Configure by using
    /// <c>SourceFrom</c>, <c>FilterBy</c>, <c>WriteTo</c> and <c>TransformBy</c> properties.
    /// 
    /// The configurations here mutate the genie, so don't expect to be able to stack them.
    /// </summary>
    public class EnumGenieGenerator
    {
        private readonly List<IEnumSource> _sources = new List<IEnumSource>();
        private readonly List<IEnumFilter> _filters = new List<IEnumFilter>();
        private readonly List<IWriter> _writers = new List<IWriter>();
        private readonly List<ITransform> _transforms = new List<ITransform>();

        private static readonly IDescriber DefaultDescriber = new DefaultDescriber();
        private IDescriber _describer = DefaultDescriber;

        /// <summary>
        /// Configures from where the enums can be read
        /// </summary>
        public Source SourceFrom => new Source(this);

        /// <summary>
        /// Allows removal of unwanted enums.
        /// </summary>
        public Filter FilterBy => new Filter(this);

        /// <summary>
        /// Describes where to write the generated enums.  Each writer has its 
        /// own configuration for the output type.
        /// </summary>
        public Writer WriteTo => new Writer(this);

        /// <summary>
        /// Transforms over enum definitions.  Allows hefty customisation
        /// of the enum descriptions
        /// </summary>
        public Transform TransformBy => new Transform(this);

        /// <summary>
        /// How to get the description for an enum member
        /// </summary>
        public Describer DescribeUsing => new Describer(this);

        internal void AddSource(IEnumSource enumSource)
        {
            _sources.Add(enumSource);
        }

        internal void AddFilter(IEnumFilter filter)
        {
            _filters.Add(filter);
        }

        internal void AddWriter(IWriter writer)
        {
            _writers.Add(writer);
        }

        internal void AddTransform(ITransform transform)
        {
            _transforms.Add(transform);
        }

        public void SetDescriber(IDescriber describer)
        {
            _describer = describer ?? DefaultDescriber;
        }

        /// <summary>
        /// Writes all the enums from the sources to the specified writers
        /// </summary>
        public void Write()
        {
            var enumTypes = _sources.SelectMany(s => s.EnumTypes)
                .Where(t => _filters.All(f => f.ShouldEmit(t)))
                .Distinct()
                .ToList();

            var nonEnumTypes = enumTypes.Where(e => !e.GetTypeInfo().IsEnum).ToList();

            if (nonEnumTypes.Any())
                throw new InvalidOperationException($"Can only write enum types. Fail on: {string.Join(",", nonEnumTypes.Select(e => e.Name))}");

            var definitions = enumTypes
                .Select(EnumDefinition)
                .Select(def => _transforms.Aggregate(def, (old, transform) => transform.Transform(old)))
                .ToList();

            foreach (var writer in _writers)
            {
                writer.Write(definitions.ToList());
            }
        }

        private  EnumDefinition EnumDefinition(Type enumType)
        {
            var memberDefinitions = Enum.GetValues(enumType)
                .Cast<int>()
                .Select(value => EnumMemberDefinition(enumType, value))
                .ToList();

            return new EnumDefinition(enumType, enumType.Name, memberDefinitions);
        }

        private  EnumMemberDefinition EnumMemberDefinition(Type enumType, int value)
        {
            var name = Enum.GetName(enumType, value);
            var member = enumType.GetTypeInfo().DeclaredMembers.Where(m => m.Name == name).First();
            var description = _describer.GetDescription(enumType, member) 
                ?? DefaultDescriber.GetDescription(enumType, member);

            return new EnumMemberDefinition(member, value, name, description);
        }
    }
}