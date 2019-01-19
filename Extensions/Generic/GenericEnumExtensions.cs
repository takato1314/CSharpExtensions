using System;
using System.Collections.Generic;
using System.Linq;

namespace Reevo.Unbroken.Extensions
{
    public static class GenericEnumExtensions
    {
        public static IEnumerable<T> GetFlags<T>(this T input)
            where T : struct
        {
            if (!typeof(T).IsEnum)
            {
                throw new InvalidOperationException("Invalid type in GetFlags<T>.");
            }
               
            var source = (Enum)(object)input;
            return from Enum value
                   in Enum.GetValues(typeof (T))
                   where source.HasFlag(value)
                   select (T)(object)value;
        }


    }
}