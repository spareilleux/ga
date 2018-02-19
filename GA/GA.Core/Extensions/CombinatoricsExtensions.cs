using System;
using System.Collections.Generic;
using System.Linq;

namespace GA.Core.Extensions
{
    public static class CombinatoricsExtensions
    {
        /// <summary>
        /// Gets all combinations from a collection.
        /// </summary>
        public static IEnumerable<IReadOnlyList<T>> GetCombinations<T>(
            this IEnumerable<T> source)
        {
            return GetCombinations(source, null);
        }

        /// <summary>
        /// Gets all combinations from a collection (with predicate)
        /// </summary>
        /// <param name="source">The <see cref="IEnumerable{T}"/>.</param>
        /// <param name="predicate">The <see cref="IList{T}" /> predicate.</param>
        public static IEnumerable<IReadOnlyList<T>> GetCombinations<T>(
            this IEnumerable<T> source, 
            Func<IList<T>, bool> predicate)
        {
            var sourceArray = source.ToArray();
            for (var formula = 1; formula < 1u << sourceArray.Length; formula++)
            {
                // Compute a new combination
                var combination = new List<T>();
                var index = 0;
                var currentFormula = formula;

                while (currentFormula > 0)
                {
                    if ((currentFormula & 1) == 1)
                    {
                        var sourceItem = sourceArray[index];
                        combination.Add(sourceItem);
                    }

                    currentFormula >>= 1;
                    index++;
                }

                var item = combination.AsReadOnly();
                if (!(predicate is null) && !predicate(item)) continue;

                // Yield item
                yield return item;
            }
        }

        /// <summary>
        /// Returns all combinations from a collection of groupings (with predicate)
        /// </summary>
        public static IEnumerable<IList<TElement>> GetCombinations<TKey, TElement>(
            this IEnumerable<IGrouping<TKey, TElement>> source)
        {
            // Inits
            var groups = new List<List<TElement>>();
            var indexes = new List<int>();
            var maximums = new List<int>();
            foreach (var grouping in source)
            {
                var group = grouping.ToList();
                groups.Add(group);
                indexes.Add(0);
                maximums.Add(group.Count - 1);
            }

            var done = false;

            void Reset()
            {
                for (var groupIndex = 0; groupIndex < groups.Count; groupIndex++)
                {
                    var index = indexes[groupIndex];
                    var maximum = maximums[groupIndex];

                    if (index == maximum)
                    {
                        // Re-initialize the group index
                        indexes[groupIndex] = 0;
                        done = groupIndex == groups.Count - 1;
                    }
                    else
                    {
                        // Next group index
                        indexes[groupIndex]++;
                        break;
                    }
                }
            }

            while (!done)
            {
                // Compute a new combination
                var combination = new List<TElement>();
                for (var groupIndex = 0; groupIndex < groups.Count; groupIndex++)
                {
                    var index = indexes[groupIndex];
                    var group = groups[groupIndex];

                    combination.Add(group[index]);
                }

                var item = combination.AsReadOnly();

                // Yield item
                yield return item;

                // Prepare for next combination
                Reset();
            }
        }

        /// <summary>
        /// Rotates a collection.
        /// </summary>
        /// <typeparam name="T">The item type,</typeparam>
        /// <param name="source">The source <see cref="IEnumerable{T}"/></param>
        /// <param name="count">The </param>
        public static IEnumerable<T> Rotate<T>(
            this IEnumerable<T> source, 
            int count)
        {
            source = source.ToList();

            while (count < 0) count += source.Count();
            count = count % source.Count();

            var result = source.Skip(count).Concat(source.Take(count));

            return result;
        }

        /// <summary>
        /// Gets all inversions from a collection
        /// </summary>
        public static IEnumerable<IReadOnlyCollection<T>> GetInversions<T>(this IEnumerable<T> source)
        {
            var sourceList = source as IList<T> ?? source.ToList();
            var result = sourceList.Select((t, i) => Rotate(sourceList, i).ToList().AsReadOnly());

            return result;
        }

        /// <summary>
        /// Gets permutations from a collection.
        /// </summary>
        public static IEnumerable<List<T>> AllPermutations<T>(this IEnumerable<T> source)
        {
            source = source.ToList();

            if (!source.Any()) yield return new List<T>();

            foreach (var i in source)
            {
                var copy = new List<T>(source);
                copy.Remove(i);
                foreach (var rest in AllPermutations(copy))
                {
                    rest.Insert(0, i);
                    yield return rest;
                }
            }
        }

        public static IEnumerable<IReadOnlyList<T>> AllArrangements<T>(this IEnumerable<T> source)
        {
            int Selector(T p) => Convert.ToInt32(p);
            var equatilyComparer = SameSequenceEqualityComparer<T>.SharedInstance;

            return
                source
                    .AllPermutations()
                    .SelectMany(p => p.GetCombinations())
                    .Distinct(equatilyComparer)
                    .Select(p => p.ToList().AsReadOnly())
                    .OrderBy(p => p.AsEnumerable().Reverse().GetUniqueId(Selector, 8));
        }

        internal class SameSequenceEqualityComparer<T> : IEqualityComparer<IEnumerable<T>>
        {
            public static SameSequenceEqualityComparer<T> SharedInstance = new SameSequenceEqualityComparer<T>();

            public bool Equals(IEnumerable<T> x, IEnumerable<T> y)
            {
                return x.SequenceEqual(y);
            }

            public int GetHashCode(IEnumerable<T> obj)
            {
                int result = 33;
                foreach (T item in obj)
                {
                    result = result * 7 + Convert.ToInt32(item);
                }
                return result;
            }
        }
    }
}