using System;
using System.Reflection;

namespace EnumGenie.Core.Describers
{
    public class CustomDescriber : IDescriber
    {
        private readonly Func<Type, MemberInfo, string> _getDescription;

        public CustomDescriber(Func<Type, MemberInfo, string> getDescription)
        {
            _getDescription = getDescription;
        }

        public string GetDescription(Type enumDefinition, MemberInfo member)
        {
            return _getDescription(enumDefinition, member);
        }
    }
}