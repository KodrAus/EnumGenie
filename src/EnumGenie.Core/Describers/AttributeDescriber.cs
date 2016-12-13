using System;
using System.Reflection;

namespace EnumGenie.Core.Describers
{
    public class AttributeDescriber<TAttribute> : IDescriber where TAttribute : Attribute
    {
        private readonly Func<TAttribute, string> _descriptionFromAttribute;

        public AttributeDescriber(Func<TAttribute, string> descriptionFromAttribute)
        {
            _descriptionFromAttribute = descriptionFromAttribute;
        }

        public string GetDescription(Type enumDefinition, MemberInfo member)
        {
            var attribute = member.GetCustomAttribute<TAttribute>();
            return attribute == null ? null : _descriptionFromAttribute(attribute);
        }
    }
}