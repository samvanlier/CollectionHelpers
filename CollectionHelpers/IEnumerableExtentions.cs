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

        /// <summary>
        /// Checks if a collection is Null or Empty
        /// </summary>
        /// <remarks>
        /// The operation runs in O(n) because of <see cref="Enumerable.Count{TSource}(IEnumerable{TSource})"/>.
        /// <para>
        /// If the type of <paramref name="collection"/> implements <see cref="ICollection{T}"/>,
        /// that implentation is used to obtain the count of elements.
        /// Otherwise <see cref="Enumerable.Count{TSource}(IEnumerable{TSource})"/> is used
        /// </para>
        /// </remarks>
        /// <typeparam name="T"></typeparam>
        /// <param name="collection"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> collection)
        {
            return collection == null || collection.Count() == 0;
        }

        /// <summary>
        /// Shuffle a <see cref="IEnumerable{T}"/> using <see href="http://en.wikipedia.org/wiki/Fisher%E2%80%93Yates_shuffle#The_modern_algorithm">Fisher-Yates-Durstenfeld shuffle</see>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">An <see cref="IEnumerable{T}"/> to shuffle</param>
        /// <returns>A shuffled <see cref="IEnumerable{T}"/></returns>
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
        {
            return source.Shuffle(new Random());
        }

        /// <summary>
        /// Shuffle a <see cref="IEnumerable{T}"/> using <see href="http://en.wikipedia.org/wiki/Fisher%E2%80%93Yates_shuffle#The_modern_algorithm">Fisher-Yates-Durstenfeld shuffle</see>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">An <see cref="IEnumerable{T}"/> to shuffle</param>
        /// <param name="seed">A seed to use for the <see cref="Random(Int32)"/></param>
        /// <returns>A shuffled <see cref="IEnumerable{T}"/></returns>
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source, int seed)
        {
            return source.Shuffle(new Random(seed));
        }

        /// <summary>
        /// Shuffle a <see cref="IEnumerable{T}"/> using <see href="http://en.wikipedia.org/wiki/Fisher%E2%80%93Yates_shuffle#The_modern_algorithm">Fisher-Yates-Durstenfeld shuffle</see>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">An <see cref="IEnumerable{T}"/> to shuffle</param>
        /// <param name="random">A <see cref="Random"/> object that is used for the shuffeling</param>
        /// <returns>A shuffled <see cref="IEnumerable{T}"/></returns>
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source, Random random)
        {
            if (source == null) 
                throw new ArgumentNullException("source cannot be null");
            if (random == null)
                throw new ArgumentNullException("random cannot be null");

            var buffer = source.ToList();
            for (int i = 0; i < buffer.Count; i++)
            {
                int j = random.Next(i, buffer.Count);
                yield return buffer[j];

                buffer[j] = buffer[i];
            }
        }

        /// <summary>
        /// Checks if a <see cref="IEnumerable{T}"/> is a subset of another <see cref="IEnumerable{T}"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="superset">The super set which might contain the <paramref name="subset"/></param>
        /// <param name="subset">A subset to test</param>
        /// <returns>A boolean indicating if the <paramref name="subset"/> is a propper subset of the <paramref name="superset"/></returns>
        public static bool ContainsAll<T>(this IEnumerable<T> superset, IEnumerable<T> subset)
        {
            return !subset.Except(superset).Any();
        }
    }
}
