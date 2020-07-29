using System;
using System.Collections.Generic;
using System.Linq;

namespace CollectionHelpers
{
    /// <summary>
    /// Contains extentions that can be applied on IEnumerables
    /// </summary>
    public static class IEnumerableExtentions
    {
        /// <summary>
        /// Sorts a Enumerable and returns a list 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="source"></param>
        /// <param name="func"></param>
        /// <param name="descending"></param>
        /// <returns></returns>
        public static IList<TSource> OrderByToList<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> func, bool descending = false)
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
        /// Convert Enumerable that is null to an empty Enumerable
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">Enumerable or null to convert</param>
        /// <returns>an empty Enumerable</returns>
        public static IEnumerable<T> NullToEmpty<T>(this IEnumerable<T> source)
        {
            if (source == null)
                return Enumerable.Empty<T>();

            return source;
        }

        /// <summary>
        /// Performce the specified action on each element of the <see cref="IEnumerable{T}"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="action">The <see cref="Action{T}"/> delegate to perform on each element of the <see cref="IEnumerable{T}"/></param>
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (T item in source)
            {
                action(item);
            }
        }
    }
}
