using System;
using System.Collections.Generic;
using System.Linq;

namespace Reevo.Unbroken.Extensions
{
    public static class ListExtensions
    {
        #region Get

        public static bool TryGet<TSource>(this IList<TSource> enumerable, Func<TSource, bool> predicate, out IList<TSource> result)
        {
            if (enumerable.Any(predicate))
            {
                result = enumerable.Where(predicate).ToList();
                return true;
            }
            result = enumerable.Where(predicate).ToList();
            return false;
        }

        public static bool TryGetSingle<TSource>(this IList<TSource> enumerable, Func<TSource, bool> predicate, out TSource result)
        {
            if (enumerable.Any(predicate) && enumerable.Count(predicate) == 1)
            {
                result = enumerable.Single(predicate);
                return true;
            }
            result = default(TSource);
            return false;
        }

        public static bool TryGetFirst<TSource>(this IList<TSource> enumerable, Func<TSource, bool> predicate, out TSource result)
        {
            if (enumerable.Any(predicate))
            {
                result = enumerable.First(predicate);
                return true;
            }
            result = default(TSource);
            return false;
        }

        #endregion

        #region Delete
        /// <summary>
        ///     Remove all elements matching a predicate from this list.
        ///     NOTE: this has surprising behaviour when called on an ObservableCollection, since the CollectionChanged event
        ///     will fire after the removal of each individual element (hence multiple times per call, not only once as one would
        ///     expect). See the ObservableCollectionExtensions in the non-portable Util project for a solution for this problem.
        /// </summary>
        /// <typeparam name="T">The type of the elements in this list.</typeparam>
        /// <param name="list">The list.</param>
        /// <param name="predicate">
        ///     A method or lambda taking an element and returning true if that element shall be removed, false
        ///     otherwise.
        /// </param>
        /// <returns>The number of elements removed from the list.</returns>
        public static int RemoveAll<T>(this IList<T> list, Predicate<T> predicate)
        {
            var oldCount = list.Count;
            for (var i = list.Count - 1; i >= 0; i--)
            {
                if (predicate(list[i]))
                {
                    list.RemoveAt(i);
                }
            }
            return oldCount - list.Count;
        }

        /// <summary>
        ///     Remove the first element in the list that matches a predicate.
        /// </summary>
        /// <typeparam name="T">The type of the elements in this list.</typeparam>
        /// <param name="list">The list.</param>
        /// <param name="predicate">
        ///     A method or lambda taking an element and returning true if that element shall be removed, false
        ///     otherwise.
        /// </param>
        /// <returns>Whether an element was removed or not.</returns>
        public static bool RemoveFirst<T>(this IList<T> list, Predicate<T> predicate)
        {
            for (var i = 0; i < list.Count; ++i)
            {
                if (!predicate(list[i]))
                {
                    continue;
                }
                list.RemoveAt(i);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Remove all elements from the list except the first element.
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="enumerable"></param>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public static int RemoveAllButFirst<TSource>(this IList<TSource> enumerable, Predicate<TSource> predicate)
            where TSource : IEquatable<TSource>
        {
            int count = 0;
            List<int> removedIndex = new List<int>();

            for (int i = 0; i < enumerable.Count; i++)
            {
                if (predicate.Invoke(enumerable[i]) && count == 0)
                {
                    count++;
                    continue;
                }

                if (predicate.Invoke(enumerable[i]))
                {
                    removedIndex.Add(i);
                    count++;
                }
            }

            if (!removedIndex.IsNullOrEmpty())
            {
                foreach (var index in removedIndex)
                {
                    enumerable.RemoveAt(index);
                }
            }

            return count - 1;
        }

        #endregion

        #region Misc

        public static IList<TSource> Clone<TSource>(this IList<TSource> enumerable) where TSource : ICloneable
        {
            return enumerable.Select(item => (TSource)item.Clone()).ToList();
        }

        /// <summary>
        /// Split generic list into multiple lists with a max size.
        /// Result lists are filled up to max size, as long as elements are left.
        /// </summary>
        /// <typeparam name="T">Type of list elements</typeparam>
        /// <param name="initialList">The list to split.</param>
        /// <param name="partSize">Max. items per result list</param>
        /// <returns></returns>
        public static IList<IList<T>> SplitList<T>(this IList<T> initialList, int partSize)
        {
            int startIdx = 0;
            IList<IList<T>> result = new List<IList<T>>();
            while (startIdx < initialList.Count)
            {
                result.Add(new List<T>(initialList.Skip(startIdx).Take(partSize)));
                startIdx += partSize;
            }
            return result;
        }

        /// <summary>
        /// Shuffles all the elements in the list randomly?
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        public static void Shuffle<T>(this IList<T> list)
        {
            Random rng = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        /// <summary>
        /// Filters the elements in the list with the specified predicate provided.
        /// (Aside: for some reason LINQ prefers Func{T,bool};)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="predicate"></param>
        /// <returns>Returns a new list with elements that are filtered.</returns>
        public static IList<T> Filter<T>(this IList<T> source, Predicate<T> predicate)
        {
            var result = new List<T>();

            foreach (var item in source)
            {
                if (predicate(item))
                {
                    result.Add(item);
                }
            }

            return result;
        }

        /// <summary>
        /// Add a unique item into the <see cref="IList{T}"/> if it does not exists.
        /// </summary>
        /// <param name="source">Source list</param>
        /// <param name="item">Item to be added</param>
        /// <param name="equality">Uses <see cref="StringComparer"/> as the default <see cref="IEqualityComparer{T}"/>. Please provide custom implementation for custom type T</param>
        public static void AddDistinct<T>(this IList<T> source, T item, IEqualityComparer<T> equality = null)
        {
            if (source == null)
            {
                return;
            }

            // Uses 
            var notFoundPredicate = source.Contains(item, equality ?? (IEqualityComparer<T>) StringComparer.CurrentCultureIgnoreCase);
            if (!notFoundPredicate)
            {
                source.Add(item);
            }
        }

        #endregion
    }
}