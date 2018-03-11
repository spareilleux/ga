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

        public static T Next<T>(this T enumValue)
            where T : struct
        {
            if (!typeof(T).IsEnum) throw new ArgumentException($"Parameter '{nameof(enumValue)}' is not an Enum", nameof(enumValue));

            var array = (T[])Enum.GetValues(enumValue.GetType());
            var index = Array.IndexOf(array, enumValue) + 1;

            var result = array.Length == index ? array[0] : array[index];

            return result;
        }

        public static T Invert<T>(this T enumValue)
            where T : struct
        {
            if (!typeof(T).IsEnum) throw new ArgumentException($"Parameter '{nameof(enumValue)}' is not an Enum", nameof(enumValue));

            var array = (T[])Enum.GetValues(enumValue.GetType());
            var index = Array.IndexOf(array, enumValue);
            index = array.Length - index;

            var result = array[index];

            return result;
        }

        public static int DistanceFrom<T>(this T enumValue, T fromEnumValue)
            where T : struct
        {
            if (!typeof(T).IsEnum) throw new ArgumentException($"Parameter '{nameof(enumValue)}' is not an Enum", nameof(enumValue));

            var array = (T[])Enum.GetValues(enumValue.GetType());
            var indexTo = Array.IndexOf(array, enumValue);
            var indexFrom = Array.IndexOf(array, fromEnumValue);

            var result = indexTo - indexFrom;

            return result;
        }

        public static int DistanceTo<T>(this T enumValue, T toEnumValue)
            where T : struct
        {
            var result = -DistanceFrom(enumValue, toEnumValue);

            return result;
        }
    }
}
