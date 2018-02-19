using System;
using System.Collections.Generic;
using System.Linq;

namespace GA.Core.Extensions
{
    public static class IdentificationExtensions
    {
        /// <summary>
        /// Gets a unique Guid identifier from a sequence of objects
        /// </summary>
        public static Guid GetUniqueId<T>(
            this IEnumerable<T> collection, 
            Func<T, int> selector, 
            int maxInt,
            bool applySort = false, 
            bool normalize = false)
        {
            var result = 
                collection
                    .Select(selector)
                    .GetUniqueId(maxInt, applySort, normalize);

            return result;
        }

        /// <summary>
        /// Gets a unique Guid identifier from a integer collection.
        /// </summary>
        public static Guid GetUniqueId(
            this IEnumerable<int> collection, 
            int maxInt, 
            bool applySort = false,
            bool normalize = false)
        {
            // Retrieve signature
            if (normalize) collection = collection.Normalize();
            var sortedIntList = collection.ToList();
            if (applySort) sortedIntList.Sort();
            long signature = 0;
            var weight = 1;

            foreach (var i in sortedIntList)
            {
                if (i > maxInt) throw new ArgumentException("Internal error in GetUniqueId");
                signature += i * weight;
                weight = weight * maxInt;
            }

            // Convert signature into Guid
            var bytes = new List<byte>();
            for (var i = 0; i < 16; i++)
            {
                var b = (byte) (signature & 0xFF);
                signature = signature >> 8;
                bytes.Add(b);
            }

            bytes.Reverse();
            var result = new Guid(bytes.ToArray());

            return result;
        }

        /// <summary>
        /// Subtract all elements with minimum
        /// </summary>
        private static IEnumerable<int> Normalize(this IEnumerable<int> collection)
        {
            collection = collection.ToList();
            var min = collection.Min();
            var result = collection.Select(p => p - min);

            return result;
        }

        /// <summary>
        /// Gets an int unique identifier from a sequence of booleans.
        /// </summary>
        public static int GetId(this IEnumerable<bool> bools)
        {
            var boolList = bools.ToList();
            var result = 0;
            var weight = 1;

            foreach (var b in boolList)
            {
                if (b) result += weight;
                weight = weight * 2;
            }

            return result;
        }
    }
}   