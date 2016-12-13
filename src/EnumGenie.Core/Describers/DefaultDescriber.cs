using System;
using System.Reflection;

namespace EnumGenie.Core.Describers
{
    public class DefaultDescriber : IDescriber
    {
        public string GetDescription(Type enumDefinition, MemberInfo member)
        {
            return member.Name;
        }
    }
}