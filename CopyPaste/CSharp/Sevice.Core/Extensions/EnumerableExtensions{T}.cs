using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace Sevices.Core.Extensions
{
    /// <summary>
    /// Extension methods for <see cref="IEnumerable{T}"/> object.
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Executes an action for each element.
        /// </summary>
        /// <typeparam name="TSource">The type of the source enumerable.</typeparam>
        /// <param name="source">A sequence of values to invoke an action.</param>
        /// <param name="iterativeAction">An action for each element.</param>
        [DebuggerStepThrough]
        public static void ForEach<TSource>(this IEnumerable<TSource> source, Action<TSource> iterativeAction)
        {
            if (source is null || iterativeAction is null)
            {
                return;
            }

            foreach (var element in source)
            {
                iterativeAction(element);
            }
        }

        /// <summary>
        /// Checks whether or not there is the duplicated key of the source enumerable.
        /// </summary>
        /// <typeparam name="TSource">The type of the source enumerable.</typeparam>
        /// <typeparam name="TKey">The type of the key returned by <c>keySelector</c>.</typeparam>
        /// <param name="source">A sequence of values to check.</param>
        /// <param name="keySelector">A function to extract the key for each element.</param>
        /// <returns>
        /// <c>false</c>, if <c>source</c> or <c>keySelector</c> is <c>null</c>.
        /// <c>true</c>, if the duplicated key in the keys selected by <c>keySelector</c> exists. Otherwise; <c>false</c>.
        /// </returns>
        [DebuggerStepThrough]
        public static bool IsKeyDuplicated<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> keySelector)
        {
            if (source is null || keySelector is null)
            {
                return false;
            }

            return source.GroupBy(keySelector).Any(g => g.Count() > 1);
        }

        /// <summary>
        /// Advances the enumerator to the next element of the collection cyclically.
        /// </summary>
        /// <typeparam name="TSource">The type of the source enumerable.</typeparam>
        /// <param name="source">A sequence of values to invoke an action.</param>
        /// <param name="currentElement">The current element.</param>
        /// <returns>
        /// The next element, if the current element exists or the last element of the sequence,
        /// otherwise, returns the first element or <c>null</c>.
        /// <para>Returns default value, if <c>source</c> is <c>null</c>.</para>
        /// </returns>
        [DebuggerStepThrough]
        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public static TSource SafeMoveNext<TSource>(this IEnumerable<TSource> source, TSource currentElement)
        {
            if (source is null)
            {
                return default;
            }

            var foundCurrentElement = false;
            foreach (var element in source)
            {
                if (foundCurrentElement)
                {
                    return element; // next element
                }

                if (EqualityComparer<TSource>.Default.Equals(element, currentElement))
                {
                    foundCurrentElement = true;
                }
            }

            return source.FirstOrDefault();
        }

        /// <summary>
        /// Projects each element of a sequence into a new form except the elements of the new one is <c>null</c>.
        /// </summary>
        /// <typeparam name="TSource">The type of the source enumerable.</typeparam>
        /// <typeparam name="TResult">The type of the mapped items.</typeparam>
        /// <param name="source">A sequence of values to invoke a transform function on.</param>
        /// <param name="selector">A transform function to apply to each element.</param>
        /// <returns>An IEnumerable{TResult} whose elements except <c>null</c> are the result of invoking the transform function on each element of <c>source</c>.</returns>
        [DebuggerStepThrough]
        public static IEnumerable<TResult> SelectNotNull<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector)
        {
            if (source is null || selector is null)
            {
                yield break;
            }

            foreach (var element in source)
            {
                var result = selector(element);
                if (result != null)
                {
                    yield return result;
                }
            }
        }

        /// <summary>
        /// Filters the non-NULL elements in a sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the source enumerable.</typeparam>
        /// <param name="source">A sequence to except NULL elements.</param>
        /// <returns>A sequence with <c>null</c> elements excepted.</returns>
        [DebuggerStepThrough]
        public static IEnumerable<TSource> WhereNotNull<TSource>(this IEnumerable<TSource> source)
        {
            return source?.Where(element => element != null) ?? Enumerable.Empty<TSource>();
        }

        /// <summary>
        /// Indicates whether a sequence is <c>null</c> or does not contain any element.
        /// </summary>
        /// <typeparam name="TSource">The type of the source enumerable.</typeparam>
        /// <param name="source">A sequence of values to invoke a transform function on.</param>
        /// <returns><c>true</c> if a sequence is <c>null</c> or does not contain any element; otherwise, <c>false</c>.</returns>
        [DebuggerStepThrough]
        public static bool IsNullOrEmpty<TSource>(this IEnumerable<TSource> source)
        {
            return source is null || !source.Any();
        }

        /// <summary>
        /// Determines whether there is the same element both the two sequence.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of the sequence.</typeparam>
        /// <param name="source">The source sequence.</param>
        /// <param name="other">The other sequence.</param>
        /// <returns><c>true</c>, if the same element in the two sequence is found. Otherwise; <c>false</c>.</returns>
        [DebuggerStepThrough]
        [SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public static bool ContainsSameElement<TSource>(this IEnumerable<TSource> source, IEnumerable<TSource> other)
        {
            ArgumentNullException.ThrowIfNull(source);
            ArgumentNullException.ThrowIfNull(other);

            if (source.IsEmpty() || other.IsEmpty())
            {
                return false;
            }

            var otherElements = other.ToSafeArray();
            return source.Any(sourceElement => otherElements.Contains(sourceElement));
        }

        /// <summary>
        /// Indicates whether a sequence does not contain any element.
        /// </summary>
        /// <typeparam name="TSource">The type of the source enumerable.</typeparam>
        /// <param name="source">A sequence of values to invoke a transform function on.</param>
        /// <returns><c>true</c> if a sequence does not contain any element; otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException"><c>source</c> is <c>null</c>.</exception>
        [DebuggerStepThrough]
        public static bool IsEmpty<TSource>(this IEnumerable<TSource> source)
        {
            ArgumentNullException.ThrowIfNull(source);
            return !source.Any();
        }

        /// <summary>
        /// Creates an empty <see cref="IEnumerable{T}"/> that has the specified type argument if the source sequence is <c>null</c>.
        /// </summary>
        /// <typeparam name="TResult">The type to assign to the type parameter of the returned generic <see cref="IEnumerable{T}"/>.</typeparam>
        /// <param name="source">An <see cref="IEnumerable{T}"/> to create an Enumerable empty.</param>
        /// <returns>An empty <see cref="IEnumerable{TResult}"/> whose type argument is <c>TResult</c> if <c>source</c> is <c>null</c>. Otherwise; <c>source</c>.</returns>
        [DebuggerStepThrough]
        public static IEnumerable<TResult> ToEmptyIfNull<TResult>(this IEnumerable<TResult> source)
        {
            return source ?? Enumerable.Empty<TResult>();
        }

        /// <summary>
        /// Creates an array from a <see cref="IEnumerable{T}"/>. If the sequence is <c>null</c>, creates an empty array.
        /// </summary>
        /// <typeparam name="TSource">The type of the source enumerable.</typeparam>
        /// <param name="source">An <see cref="IEnumerable{T}"/> to create an array from.</param>
        /// <returns>An array that contains the elements from the input sequence, if the <c>source</c> is not <c>null</c>. Otherwise; An empty array, <c>Array.Empty()</c>.</returns>
        [DebuggerStepThrough]
        public static TSource[] ToSafeArray<TSource>(this IEnumerable<TSource> source)
        {
            if (source is null)
            {
                return Array.Empty<TSource>();
            }

            return source.ToArray();
        }

        /// <summary>
        /// Creates a list from a <see cref="IEnumerable{T}"/>. If the sequence is <c>null</c>, creates an empty list.
        /// </summary>
        /// <typeparam name="TSource">The type of the source enumerable.</typeparam>
        /// <param name="source">An <see cref="IEnumerable{T}"/> to create a list from.</param>
        /// <returns>A list that contains the elements from the input sequence, if the <c>source</c> is not <c>null</c>. Otherwise; A empty list.</returns>
        [DebuggerStepThrough]
        public static List<TSource> ToSafeList<TSource>(this IEnumerable<TSource> source)
        {
            return (source ?? Enumerable.Empty<TSource>()).ToList();
        }

        /// <summary>
        /// Returns the only element of a sequence that satisfies a specified condition, or a specified default value if no such element exists or more than one element satisfies the condition.
        /// This method throws no exception.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <c>source</c>.</typeparam>
        /// <param name="source">An <see cref="IEnumerable{T}"/> to return the single element of.</param>
        /// <param name="predicate">A function to test an element for a condition.</param>
        /// <param name="defaultValue">The default value to return if the sequence has multiple elements.</param>
        /// <returns>
        /// The single element of the input sequence that satisfies the condition
        /// , or defaultValue if no such element is found or more than one element satisfies the condition.
        /// </returns>
        [DebuggerStepThrough]
        public static TSource SafeSingleOrDefault<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate, TSource defaultValue = default)
        {
            if (predicate is null)
            {
                return source.SafeSingleOrDefault(defaultValue);
            }

            if (source is null)
            {
                return defaultValue;
            }

            var result = defaultValue;
            var foundFirst = false;
            foreach (var element in source)
            {
                if (predicate.Invoke(element) == false)
                {
                    continue;
                }

                if (foundFirst)
                {
                    return defaultValue;
                }

                result = element;
                foundFirst = true;
            }

            return result;
        }

        /// <summary>
        /// Returns the only element of a sequence, or a default value if the sequence is empty or there is more than one element in the sequence.
        /// This method throws no exception.
        /// </summary>
        /// <typeparam name="TSource">The type of the elements of <c>source</c>.</typeparam>
        /// <param name="source">An <see cref="IEnumerable{T}"/> to return the single element of.</param>
        /// <param name="defaultValue">The default value to return if the sequence has multiple elements.</param>
        /// <returns>The single element of the input sequence, or <c>defaultValue</c> if the sequence contains no elements or more than one element.</returns>
        [DebuggerStepThrough]
        public static TSource SafeSingleOrDefault<TSource>(this IEnumerable<TSource> source, TSource defaultValue = default)
        {
            if (source is null)
            {
                return defaultValue;
            }

            var result = defaultValue;
            var foundFirst = false;
            foreach (var element in source)
            {
                if (foundFirst)
                {
                    return defaultValue;
                }

                result = element;
                foundFirst = true;
            }

            return result;
        }
    }
}
