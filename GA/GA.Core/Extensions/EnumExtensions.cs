using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;

namespace GA.Core.Extensions
{
    public static class EnumExtensions
    {
        public static TAttribute GetFieldAttribute<TAttribute>(this Enum enumValue)
            where TAttribute : Attribute
        {
            var fi = enumValue.GetType().GetTypeInfo().GetDeclaredField(enumValue.ToString());

            if (fi != null)
            {
                var attrs = fi.GetCustomAttributes(typeof(TAttribute), true).ToArray();
                if (attrs.Length > 0)
                    return (TAttribute)attrs[0];
            }

            return null;
        }

        public static IEnumerable<TAttribute> GetFieldAttributes<TAttribute>(this IEnumerable<Enum> enumList)
            where TAttribute : Attribute
        {
            return enumList.Select(p => p.GetFieldAttribute<TAttribute>());
        }

        public static string GetFieldDescription(this Enum enumValue)
        {
            var fi = enumValue.GetType().GetTypeInfo().GetDeclaredField(enumValue.ToString());

            if (fi != null)
            {
                var attrs = fi.GetCustomAttributes(typeof(DescriptionAttribute), true).ToArray();
                if (attrs.Length > 0)
                    return ((DescriptionAttribute)attrs[0]).Description;
            }

            return Convert.ToString(enumValue, CultureInfo.CurrentCulture);
        }

        public static IEnumerable<string> GetFieldDescriptions(this IEnumerable<Enum> enumList)
        {
            return enumList.Select(p => p.GetFieldDescription());
        }

        public static Enum ParseFromDescription(this string description, Type enumType)
        {
            foreach (Enum enumValue in Enum.GetValues(enumType))
                if (string.Equals(GetFieldDescription(enumValue), description, StringComparison.OrdinalIgnoreCase))
                    return enumValue;
            return null;
        }
    }
}
