using System;
using System.Reflection;

namespace EnumGenie.Core.Describers
{
    public static class DescriberExtensions
    {
        /// <summary>
        /// Description comes from the name of the member. Default.
        /// </summary>
        public static EnumGenieGenerator Name(this Describer describer)
        {
            return describer.Custom(new DefaultDescriber());
        }

        /// <summary>
        /// Description comes from custom logic.
        /// </summary>
        public static EnumGenieGenerator Custom(this Describer describer, Func<Type, MemberInfo, string> getDescription)
        {
            return describer.Custom(new CustomDescriber(getDescription));
        }

        /// <summary>
        /// Description comes via an attribute, or default if it does not exist.
        /// </summary>
        public static EnumGenieGenerator Attribute<TAttribute>(this Describer describer, Func<TAttribute, string> getDescription) where TAttribute : Attribute
        {
            return describer.Custom(new AttributeDescriber<TAttribute>(getDescription));
        }
    }
}