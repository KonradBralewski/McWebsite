using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace McWebsite.Application.Common.Utilities
{
    public static class EnumUtilities
    {
        public static T ToEnum<T>(this string value)
        {
            object? result;

            if(Enum.TryParse(typeof(T), value, true, out result))
            {
                return (T)result;
            }

            throw new InvalidCastException($"{value} to ENUM casting failure.");
        }
        public static T ToEnum<T>(this int value)
        {
            var name = Enum.GetName(typeof(T), value);
            return name.ToEnum<T>();
        }
    }
}
