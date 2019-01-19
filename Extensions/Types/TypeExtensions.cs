using System;
using System.Collections.Generic;
using System.Linq;

namespace Reevo.Unbroken.Extensions
{
    public static class TypeExtensions
    {
        /// <summary>
        /// Determine whether a type is simple (String, Decimal, DateTime, etc) or complex (i.e. custom class with public properties and methods).
        /// </summary>
        /// <see cref="http://stackoverflow.com/questions/2442534/how-to-test-if-type-is-primitive"/>
        public static bool IsSimpleType(this Type type)
        {
            return                
                type.IsPrimitive ||
                type.IsEnum ||
                new[] 
                {
                    typeof(string),
                    typeof(decimal),
                    typeof(Enum),
                    typeof(DateTime),
                    typeof(DateTimeOffset),
                    typeof(TimeSpan),
                    typeof(Guid)
                }.Contains(type) ||
                Convert.GetTypeCode(type) != TypeCode.Object ||
                type.IsValueType ? type.IsStructType().All(_ => _) : (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>) && IsSimpleType(type.GetGenericArguments()[0]));
        }

        /// <summary>
        /// Determies whether a type is a struct by checking if all the properties declared in the type is of simple types.
        /// </summary>
        /// <param name="type"></param>
        /// <returns>A collection result containing all the checks whether if the properties are of simple types.</returns>
        /// <see cref="IsSimpleType"/>
        private static IEnumerable<bool> IsStructType(this Type type)
        {
            foreach (var propertyInfo in type.GetProperties())
            {
                if (propertyInfo.PropertyType != type)
                {
                    yield return propertyInfo.PropertyType.IsSimpleType();
                }

                yield return true;
            }            
        }

        public static IEnumerable<T> GetEnumEnumerable<T>(this Type type)
            where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("Invalid type in GetEnumEnumerable<T>.");
            }
            if (typeof(T) != type)
            {
                throw new ArgumentException("GetEnumEnumerable called on wrong type");
            }
            return Enum.GetValues(typeof(T)).Cast<T>();
        }

        public static IOrderedEnumerable<T> GetEnumOrderedEnumerable<T>(this Type type)
            where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                throw new ArgumentException("Invalid type in GetEnumOrderedEnumerable<T>.");
            }
            if (typeof(T) != type)
            {
                throw new ArgumentException("GetEnumOrderedEnumerable called on wrong type");
            }

            return Enum.GetValues(typeof(T)).Cast<T>().OrderBy(_ => _);
        }
    }
}
