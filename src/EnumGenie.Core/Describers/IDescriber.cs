using System;
using System.Reflection;

namespace EnumGenie.Core.Describers
{
    public interface IDescriber
    {
        string GetDescription(Type enumDefinition, MemberInfo member);
    }
}