using System;
using System.Collections.Generic;
using System.Text;

namespace CollectionHelpers
{
    public static class IListExtentions
    {
        /// <summary>
        /// Shuffle a <see cref="IList{T}"/> using <see href="http://en.wikipedia.org/wiki/Fisher-Yates_shuffle">Fisher-Yates shuffle</see>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">A <see cref="IList{T}"/> to shuffle</param>
        /// <remarks>
        /// This uses a thread safe alternative for its random so that when using offten or in a paralle process it still produces good random orders.
        /// </remarks>
        public static void ShuffleInplace<T>(this IList<T> list)
        {
            if (list == null)
                throw new ArgumentNullException();
            if (list.IsNullOrEmpty())
                return;

            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = ThreadSafeRandom.ThisThreadsRandom.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        /// <summary>
        /// Shuffle a <see cref="IList{T}"/> using <see href="http://en.wikipedia.org/wiki/Fisher-Yates_shuffle">Fisher-Yates shuffle</see>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">A <see cref="IList{T}"/> to shuffle</param>
        /// <param name="seed">A seed to use for the <see cref="Random(Int32)"/></param>
        public static void ShuffleInplace<T>(this IList<T> list, int seed)
        {
            if (list == null)
                throw new ArgumentNullException();
            if (list.IsNullOrEmpty())
                return;

            var random = new Random(seed);
            list.ShuffleInplace(random);
        }

        /// <summary>
        /// Shuffle a <see cref="IList{T}"/> using <see href="http://en.wikipedia.org/wiki/Fisher-Yates_shuffle">Fisher-Yates shuffle</see>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">A <see cref="IList{T}"/> to shuffle</param>
        /// <param name="random">A <see cref="Random"/> object that is used for the shuffeling</param>
        public static void ShuffleInplace<T>(this IList<T> list, Random random)
        {
            if (list == null)
                throw new ArgumentNullException();
            if (list.IsNullOrEmpty())
                return;

            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = random.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
