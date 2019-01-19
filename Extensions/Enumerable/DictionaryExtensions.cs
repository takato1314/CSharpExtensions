using System;
using System.Collections.Generic;

namespace Reevo.Unbroken.Extensions
{
    public static class DictionaryExtensions
    {
        /// <summary>
        /// Adds or updates dictionaryToUpdate with values in values
        /// </summary>
        /// <param name="dictionaryToUpdate">The dictionary that shall be updated. May not be null.</param>
        /// <param name="values">The dictionary holding the values that shall update the dictionaryToUpdate.
        /// Throws ArgumentException if dictionaryToUpdate is null.</param>
        /// <returns>The updated dictionary</returns>
        public static void AddOrUpdate<TKey, TValue>(this IDictionary<TKey, TValue> dictionaryToUpdate, IDictionary<TKey, TValue> values)
        {
            if (null == dictionaryToUpdate)
            {
                throw new ArgumentException("Dictionary to be updated is null!");
            }

            foreach (var value in values)
            {
                dictionaryToUpdate[value.Key] = value.Value;
            }
        }

        /// <summary>
        /// Replaces the <paramref name="oldKey"/> with the <paramref name="newKey"/> and keeps the value.
        /// </summary>
        /// <typeparam name="TKey">The type of keys in the <paramref name="dictionary"/>.</typeparam>
        /// <typeparam name="TValue">The type of values in the <paramref name="dictionary"/>.</typeparam>
        /// <param name="dictionary">The dictionary.</param>
        /// <param name="oldKey">The old key.</param>
        /// <param name="newKey">The new key.</param>
        public static void ReplaceKey<TKey, TValue>(this IDictionary<TKey, TValue> dictionary, TKey oldKey, TKey newKey)
        {
            var value = dictionary[oldKey];
            dictionary.Remove(oldKey);
            dictionary[newKey] = value;
        }
    }
}
