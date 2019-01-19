using System;
using System.Collections.Generic;
using System.Linq;

namespace Reevo.Unbroken.Extensions
{
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Checks if the enumerable has only one item.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <returns></returns>
        public static bool IsSingleItem<T>(this IEnumerable<T> enumerable)
        {
            return enumerable.Count() == 1;
        }
        
        /// <summary>
        /// Checks if enumerable is null or empty
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> enumerable)
        {
            return (enumerable == null) || !enumerable.Any();
        }

        public static List<T> ToListOrNull<T>(this IEnumerable<T> enumerable)
        {
            return enumerable?.ToList();
        }                      

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> enumerable)
        {
            IList<T> list = new List<T>(enumerable);
            list.Shuffle();
            return list;
        }       

        public static IEnumerable<T> PadRight<T>(this IEnumerable<T> enumerable, T itemForPadding, int paddedCount)
        {
            var index = 0;
            foreach (var item in enumerable)
            {
                yield return item;
                index += 1;
            }
            for (; index < paddedCount; index++)
            {
                yield return itemForPadding;
            }
        }

        public static IEnumerable<T> Sort<T>(this IEnumerable<T> enumerable, Func<IEnumerable<T>, IEnumerable<T>> sortDel)
        {
            return sortDel(enumerable);
        }

        /// <summary>
        /// foreach extension for IEnumerable{T}
        /// <seealso cref="https://blogs.msdn.microsoft.com/ericlippert/2009/05/18/foreach-vs-foreach/"/> to understand why Microsoft doesn't provide it.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enumerable"></param>
        /// <param name="action"></param>
        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            foreach (T item in enumerable)
            {
                action(item);
            }
        }

        #region Equals extension

        /// <summary>
        /// Two collections are equivalent if they have the same elements in the same quantity, but in any order. 
        /// Elements are equal if their values are equal, not if they refer to the same object.
        /// 
        /// Remark:
        /// <list type="bullet">
        ///     <item>
        ///         <term>Ignores order and duplicate elements</term>
        ///         <description>Use <see cref="HashSet{T}.SetEquals"/> to compare two enumerables that ignores order and duplicate elements</description>
        ///     </item>
        ///     <item>
        ///         <term>Honors order and disallows duplicate elements</term>
        ///         <description>Use <see cref="Enumerable.SequenceEqual"/> to compare two enumerables that honors order and disallows duplicate elements</description>
        ///     </item>
        ///     <item>
        ///         <term>Ignores order and disallows duplicate elements</term>
        ///         <description>Use <see cref="Equals{T}"/> to compare two enumerables that ignores order and disallows duplicate elements</description>
        ///     </item>
        /// </list>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        public static bool Equals<T>(this IEnumerable<T> first, IEnumerable<T> second)
        {
            if (first == null)
            {
                return second == null;
            }

            if (second == null)
            {
                return false;
            }

            if (ReferenceEquals(first, second))
            {
                return true;
            }

            var firstCol = first as ICollection<T>;
            var secondCol = second as ICollection<T>;
            if (firstCol == null || secondCol == null)
            {
                return false;
            }

            if (firstCol.Count != secondCol.Count)
                return false;

            return !HaveMismatchedElement(firstCol, secondCol);
        }

        /// <summary>
        /// Checks for duplicate elements and null counts in the enumerables
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <returns></returns>
        private static bool HaveMismatchedElement<T>(this IEnumerable<T> first, IEnumerable<T> second)
        {
            int firstNullCount;
            int secondNullCount;

            var firstElementDict = GetElementCounts(first, out firstNullCount);
            var secondElementDict = GetElementCounts(second, out secondNullCount);

            if (firstNullCount != secondNullCount || firstElementDict.Count != secondElementDict.Count)
            {
                return true;
            }

            foreach (var kvp in firstElementDict)
            {
                var firstElementCount = kvp.Value;
                int secondElementCount;
                secondElementDict.TryGetValue(kvp.Key, out secondElementCount);

                if (firstElementCount != secondElementCount)
                    return true;
            }

            return false;
        }

        private static Dictionary<T, int> GetElementCounts<T>(IEnumerable<T> enumerable, out int nullCount)
        {
            var dictionary = new Dictionary<T, int>();
            nullCount = 0;

            foreach (var element in enumerable)
            {
                if (element == null)
                {
                    nullCount++;
                }
                else
                {
                    int num;
                    if (dictionary.TryGetValue(element, out num))
                    {
                        num++;
                        dictionary[element] = num;
                    }
                    else
                    {
                        dictionary.Add(element, num);
                    }                    
                }
            }

            return dictionary;
        }

        #endregion
    }
}
