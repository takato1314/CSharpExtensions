using System.Collections.Generic;

namespace Reevo.Unbroken.Extensions
{
    public static class CollectionExtensions
    {
        /// <summary>
        ///     Add all elements from another enumerable in the order they appear.
        /// </summary>
        /// <typeparam name="TTarget">The type of the elements of this collection.</typeparam>
        /// <typeparam name="TSource">The type of the elements that will be added. This must derive from TTarget.</typeparam>
        /// <param name="collection">The collection to add to.</param>
        /// <param name="values">The enumerable to add from.</param>
        public static void AddRange<TTarget, TSource>(this ICollection<TTarget> collection, IEnumerable<TSource> values)
            where TSource : TTarget
        {
            foreach (var value in values)
            {
                collection.Add(value);
            }
        }
    }
}