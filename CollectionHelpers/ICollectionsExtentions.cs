using System;
using System.Collections.Generic;
using System.Linq;

namespace CollectionHelpers
{
    /// <summary>
    /// Contains extentions that can be applied on all ICollections
    /// </summary>
    public static class ICollectionsExtentions
    {
        /// <summary>
        /// Append one collection to another
        /// </summary>
        /// <remarks>
        /// The performance of this operation has an expected time complexity of O(n + m)
        /// where n is the size of <c>source</c>
        /// and m is the size of <c>toAdd</c>.
        ///
        /// The best (average) case is O(m) given that most implementations of <c>ICollection</c> have inserts in O(1)
        /// </remarks>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">Collection in which to insert new elements</param>
        /// <param name="toAdd">Collection of elements to insert</param>
        public static void Add<T>(this ICollection<T> source, ICollection<T> toAdd)
        {
            if (source == null)
                throw new NullReferenceException("Collection is null");

            foreach (var item in toAdd.NullToEmpty())
            {
                source.Add(item);
            }
        }

        /// <summary>
        /// Sorts a collection (preferably a list/array/...) and returns a list 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="source"></param>
        /// <param name="func"></param>
        /// <param name="descending"></param>
        /// <returns></returns>
        public static List<TSource> OrderByToList<TSource, TKey>(this ICollection<TSource> source, Func<TSource, TKey> func, bool descending = false)
        {
            if (descending)
                return source
                    .NullToEmpty()
                    .OrderByDescending(func)
                    .ToList();

            return source
                .NullToEmpty()
                .OrderBy(func)
                .ToList();
        }

        /// <summary>
        /// Get element at index 
        /// </summary>
        /// <remarks>
        /// It is a wrapper around ElementAt(index).
        /// Less typing
        /// </remarks>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="index"></param>
        /// <returns></returns>
        public static T Get<T>(this ICollection<T> source, int index)
        {
            return source.ElementAt(index);
        }

        /// <summary>
        /// Checks if a collection is Null or Empty
        /// </summary>
        /// <remarks>
        /// The operation runs in O(1)
        /// </remarks>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty<T>(this ICollection<T> collection)
        {
            return collection == null || collection.Count == 0;
        }

        /// <summary>
        /// Applies a where-filter to a ICollection and returns the result as a list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static List<T> WhereToList<T>(this ICollection<T> source, Func<T, bool> func)
        {
            return source
                .NullToEmpty()
                .Where(func)
                .ToList();
        }

        /// <summary>
        /// Convert Collection that is null to an empty collection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">collection or null to convert</param>
        /// <returns>a collection</returns>
        public static ICollection<T> NullToEmpty<T>(this ICollection<T> source)
        {
            if (source == null)
                return new List<T>();

            return source;
        }

        /// <summary>
        /// Remove mulitple elements from a collection
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="toRemove"></param>
        /// <remarks>
        /// The performance dependce on the implementation of <see cref="ICollection{T}"/>.
        /// Worst case is O(n*m) where n = #elements in <c>source</c> and m = #elements in <c>toRemove</c>
        /// </remarks>
        public static void Remove<T>(this ICollection<T> source, ICollection<T> toRemove)
        {
            foreach (var item in toRemove.NullToEmpty())
            {
                source.Remove(item);
            }
        }

        /// <summary>
        /// Performce the specified action on each element of the <see cref="ICollection{T}"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="action">The <see cref="Action{T}"/> delegate to perform on each element of the <see cref="ICollection{T}"/></param>
        public static void ForEach<T>(this ICollection<T> source, Action<T> action)
        {
            foreach (T item in source)
            {
                action(item);
            }
        }

        /// <summary>
        /// Shuffle an <see cref="ICollection{T}"/> using <see href="http://en.wikipedia.org/wiki/Fisher-Yates_shuffle">Fisher-Yates shuffle</see> 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">An <see cref="ICollection{T}"/> to shuffle</param>
        /// <returns>A shuffled <see cref="ICollection{T}"/></returns>
        public static ICollection<T> Shuffle<T>(this ICollection<T> source)
        {
            var list = source.ToList();
            list.ShuffleInplace();
            return list;
        }

        /// <summary>
        /// Shuffle an <see cref="ICollection{T}"/> using <see href="http://en.wikipedia.org/wiki/Fisher-Yates_shuffle">Fisher-Yates shuffle</see> 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">An <see cref="ICollection{T}"/> to shuffle</param>
        /// <param name="seed">A seed to use for the <see cref="Random(Int32)"/></param>
        /// <returns>A shuffled <see cref="ICollection{T}"/></returns>
        public static ICollection<T> Shuffle<T>(this ICollection<T> source, int seed)
        {
            var list = source.ToList();
            list.ShuffleInplace(seed);
            return list;
        }

        public static ICollection<T> Shuffle<T>(this ICollection<T> source, Random random)
        {
            var list = source.ToList();
            list.ShuffleInplace(random);
            return list;
        }
    }
}
