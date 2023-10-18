using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Common
{
    public static class EnumHelper
    {
        public static string DisplayName(this Enum value)
        {
            var type = value.GetType();
            var member = type.GetMember(value.ToString());
            var displayName =
                (DisplayAttribute)member[0].GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault();

            return displayName != null ? displayName.Name : value.ToString();
        }

        public static TEnum ToEnum<TEnum>(string strEnumValue)
        {
            if (!Enum.IsDefined(typeof(TEnum), strEnumValue))
            {
                throw new Exception($"For enum string {strEnumValue} not founded enum value");
            }

            return (TEnum)Enum.Parse(typeof(TEnum), strEnumValue);
        }
        
        public static TEnum FromDisplayName<TEnum>(string displayName)
        {
            var displayToEnumNameMap = typeof(TEnum).GetFields()
                .Where(t => t.CustomAttributes.Any(a => a.AttributeType == typeof(DisplayAttribute))).ToDictionary(t =>
                        t.CustomAttributes.Single().NamedArguments.Single().TypedValue.ToString().Replace("\"", ""),
                    t => t.Name);
            if (!displayToEnumNameMap.ContainsKey(displayName))
            {
                throw new Exception($"For DisplayName string {displayName} not founded enum value");
            }

            return ToEnum<TEnum>(displayToEnumNameMap[displayName]);
        }
    }
}