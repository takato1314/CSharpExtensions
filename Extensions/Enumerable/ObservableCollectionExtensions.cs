using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Reevo.Unbroken.Extensions
{
    public static class ObservableCollectionExtensions
    {
        //We need to reflect multiple things out of the collection (see below).
        private const BindingFlags IgnoreVisibility =
            BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;

        /// <summary>
        ///     Remove all elements matching a predicate from this ObservableCollection.
        /// </summary>
        /// <typeparam name="T">The type of the elements in this ObservableCollection.</typeparam>
        /// <param name="collection">The ObservableCollection.</param>
        /// <param name="predicate">
        ///     A method or lambda taking an element and returning true if that element shall be removed, false
        ///     otherwise.
        /// </param>
        /// <returns>The number of elements removed from the ObservableCollection.</returns>
        public static int RemoveAll<T>(this ObservableCollection<T> collection, Predicate<T> predicate)
        {
            //We remove from that list instead of removing from the collection because we want to avoid firing a 
            //PropertyChanged and a CollectionChanged event after the removal of every single element.
            var list = GetInternalList(collection);

            //Perform the removal.
            var removedElements = new List<T>();
            var oldCount = list.Count;
            for (var i = list.Count - 1; i >= 0; i--)
            {
                if (!predicate(list[i]))
                {
                    continue;
                }
                removedElements.Add(list[i]);
                list.RemoveAt(i);
            }

            //If nothing changed, we don't have to fire changed events.
            if (!removedElements.Any())
            {
                return 0;
            }

            RaiseCollectionChanged(
                collection,
                new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, removedElements));

            //Return the number of removed elements.
            return oldCount - list.Count;
        }

        /// <summary>
        ///     Add all elements from another enumerable in the order they appear. This will only trigger a single collection
        ///     changed event (after all elements have been added.)
        /// </summary>
        /// <typeparam name="TSource">The type of the elements that will be added. This must derive from TTarget.</typeparam>
        /// <typeparam name="TTarget">The type of the elements of this collection.</typeparam>
        /// <param name="collection">The collection to add to.</param>
        /// <param name="sourceValues">The enumerable to add from.</param>
        public static void AddRange<TSource, TTarget>(
            this ObservableCollection<TTarget> collection,
            IEnumerable<TSource> sourceValues) where TSource : TTarget
        {
            var sourceList = sourceValues as IList<TSource> ?? sourceValues.ToList();
            if (!sourceList.Any())
            {
                return;
            }

            var targetList = GetInternalList(collection);

            foreach (var value in sourceList)
            {
                targetList.Add(value);
            }

            RaiseCollectionChanged(
                collection,
                new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        /// <summary>
        ///     We need the collection's OnXChanged methods such that we can trigger a PropertyChanged event
        ///     and a CollectionChanged after all items have been removed.
        /// </summary>
        private static void RaiseCollectionChanged<T>(
            ObservableCollection<T> collection,
            NotifyCollectionChangedEventArgs eventArgs)
        {
            var onPropertyChanged =
                typeof(ObservableCollection<T>).GetMethods(IgnoreVisibility)
                    .Where(x => x.Name == "OnPropertyChanged")
                    .First(x => x.GetParameters().Length == 1);
            var onCollectionChanged =
                typeof(ObservableCollection<T>).GetMethods(IgnoreVisibility)
                    .Where(x => x.Name == "OnCollectionChanged")
                    .First(x => x.GetParameters().Length == 1);
            onPropertyChanged.Invoke(collection, new object[] {new PropertyChangedEventArgs("Count")});
            onPropertyChanged.Invoke(collection, new object[] {new PropertyChangedEventArgs("Item[]")});
            onCollectionChanged.Invoke(collection, new object[] {eventArgs});
        }

        /// <summary>
        ///     Access the internal list containing the items that we want to change.
        /// </summary>
        private static IList<T> GetInternalList<T>(ObservableCollection<T> collection)
        {
            return
                (IList<T>)
                typeof(ObservableCollection<T>).GetProperty("Items", IgnoreVisibility)
                    .GetValue(collection, new object[0]);
        }
    }
}