using System;
using System.Collections.Generic;
using System.Linq;

namespace CollectionHelpers.IDictionary
{
    /// <summary>
    /// This contains extention methods for <see cref="IDictionary{TKey, TValue}"/>
    /// </summary>
    public static class Extentions
    {
        /// <summary>
        /// Creates a dictionary for a list, based on a given key
        /// </summary>
        /// <typeparam name="TKey">Type of key</typeparam>
        /// <typeparam name="TValue">Type of elements in list</typeparam>
        /// <param name="source">list to divide into a dictionary</param>
        /// <param name="func">selection function for the key which will be used for grouping</param>
        /// <returns>a dictionary</returns>
        /// <remarks>
        /// The Time Complexity of this method is around O(2N)
        /// The selection of the key is important for the hashing.
        /// If the key is selected poorly the Time Complexity increases to O(N^2)
        /// </remarks>
        /// <example>
        /// <code>
        /// // SomeObject has an Author (string) and a Title (string).
        /// var list = ICollection<SomeObject>();
        /// 
        /// // An author can have multiple titles
        /// // so to achier this use
        /// var result = list.MakeDictionary(el => el.Author);
        /// // now each KeyValuePair in result will contain a Author (as key) and a list of SomeObjects (as value).
        /// // in this list (the value) only object where the Author == key are pressent.
        /// </code>
        /// </example>
        public static IDictionary<TKey, ICollection<TValue>> MakeDictionary<TKey, TValue>(this ICollection<TValue> source, Func<TValue, TKey> func)
        {
            return source
                .GroupBy(func)
                .ToDictionary<IGrouping<TKey, TValue>, TKey, ICollection<TValue>>(t => t.Key, t => t.ToList());
        }

        /// <summary>
        /// Performs the specified action on each KeyValuePair<TKey, TValue>
        /// </summary>
        /// <remarks>
        /// The time complexity is O(N*a) where N is the number of <see cref="KeyValuePair{TKey, TValue}"/>s in the <see cref="IDictionary{TKey, TValue}"/>
        /// (= <see cref="Generic.ICollection.Count"/>),
        /// a is the time complexity of <c>action</c>. 
        /// </remarks>
        /// <example>
        /// <code>
        /// var dict = new Dictionary<int, string>();
        /// dict.Add(1, "hello");
        /// dict.Add(2, "world");
        ///
        /// dict.ForEach((key, value) =>
        /// {
        ///     Console.Writeln($"{key} - {value}");
        /// });
        ///
        /// // results:
        /// // "1 - hello"
        /// // "2 - world"
        /// // in this example is the time complexity O(N) because the action is O(1)
        /// </code>
        /// </example>
        /// <typeparam name="TKey">Key type of a <see cref="KeyValuePair{TKey, TValue}"/></typeparam>
        /// <typeparam name="TValue">Value type of a <see cref="KeyValuePair{TKey, TValue}"/></typeparam>
        /// <param name="source">A <see cref="IDictionary{TKey, TValue}"/> on which to perform a function on every <see cref="KeyValuePair{TKey, TValue}"/></param>
        /// <param name="action">An <see cref="Action{T1, T2}"/> which to perform on a <see cref="KeyValuePair{TKey, TValue}"/></param>
        public static void ForEach<TKey, TValue>(this IDictionary<TKey, TValue> source, Action<TKey, TValue> action)
            => source.ForEach(x => action(x.Key, x.Value));
    }
}
