using System;
using System.Collections.Generic;
using System.Linq;

namespace CollectionHelpers
{
    /// <summary>
    /// Contains extentions that can be applied on all <see cref="ICollection{T}"/>
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
        /// Performs the specified action on each element of the <see cref="ICollection{T}"/>
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

        /// <summary>
        /// Shuffle an <see cref="ICollection{T}"/> using <see href="http://en.wikipedia.org/wiki/Fisher-Yates_shuffle">Fisher-Yates shuffle</see> 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">An <see cref="ICollection{T}"/> to shuffle</param>
        /// <param name="random">A <see cref="Random"/> object that is used for the shuffeling</param>
        /// <returns>A shuffled <see cref="ICollection{T}"/></returns>
        public static ICollection<T> Shuffle<T>(this ICollection<T> source, Random random)
        {
            var list = source.ToList();
            list.ShuffleInplace(random);
            return list;
        }

        /// <summary>
        /// Split an <see cref="ICollection{T}"/> into a <see cref="ICollection{T}"/> of <see cref="ICollection{T}"/>s of size <paramref name="groupSize"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The <see cref="ICollection{T}"/> to split into chunkss</param>
        /// <param name="groupSize">The size of the chuncks. This value has to be strictly positive.</param>
        /// <returns>An <see cref="ICollection{T}"/> containing <see cref="ICollection{T}"/> of size <paramref name="groupSize"/></returns>
        /// <exception cref="ArgumentException">This is thrown when the <paramref name="groupSize"/> is smaller or equal then 0</exception>
        /// <remarks>
        /// The time complexity is O(N/<paramref name="groupSize"/>), where N is the number of elements.
        /// </remarks>
        public static ICollection<ICollection<T>> Split<T>(this ICollection<T> source, int groupSize)
        {
            if (groupSize <= 0)
                throw new ArgumentException("groupSize should be strictly positive (greater then 0)");

            var list = new List<ICollection<T>>();
            var temp = source.ToList(); //O(1)
            while (temp.Count != 0) // O(N/groupSize)
            {
                // O(1) for IList
                list.Add(temp.Take(groupSize).ToList());
                // Skip is O(1) for IList, ToList() is 0(1) because underlying implementation is IList
                temp = temp.Skip(groupSize).ToList();
            }

            return list;
        }

        /// <summary>
        /// Find the index of the largest value in the sequence.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The sequence to search in.</param>
        /// <returns>The index of the largest element in <paramref name="source"/>. If the sequence is empty, the return value is <c>-1</c></returns>
        /// <remarks>
        /// The time complexity is O(N), where N is the number of elements in the sequence.
        /// The size complexity is O(1).
        /// </remarks>
        public static int MaxIndex<T>(this ICollection<T> source) where T : IComparable<T>
        {
            int maxIndex = -1;
            T maxValue = default(T); // Immediately overwritten anyway

            int index = 0;
            foreach (T value in source)
            {
                if (value.CompareTo(maxValue) > 0 || maxIndex == -1)
                {
                    maxIndex = index;
                    maxValue = value;
                }
                index++;
            }
            return maxIndex;
        }

        /// <summary>
        /// Find the index of the smallest value in the sequence.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The sequence to search in.</param>
        /// <returns>The index of the smallest element in <paramref name="source"/>. If the sequence is empty, the return value is <c>-1</c></returns>
        /// <remarks>
        /// The time complexity is O(N), where N is the number of elements in the sequence.
        /// The size complexity is O(1).
        /// </remarks>
        public static int MinIndex<T>(this ICollection<T> source) where T : IComparable<T>
        {
            int maxIndex = -1;
            T maxValue = default(T); // Immediately overwritten anyway

            int index = 0;
            foreach (T value in source)
            {
                if (value.CompareTo(maxValue) < 0 || maxIndex == -1)
                {
                    maxIndex = index;
                    maxValue = value;
                }
                index++;
            }
            return maxIndex;
        }
    }
}
