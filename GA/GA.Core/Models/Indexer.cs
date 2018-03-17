using System.Collections.Generic;
using GA.Core.Interfaces;

namespace GA.Core.Models
{
    public class Indexer<TKey, TValue> : IIndexer<TKey, TValue>
    {
        private readonly IReadOnlyDictionary<TKey, TValue> _dictionary;

        public Indexer(IReadOnlyDictionary<TKey, TValue> dictionary)
        {
            _dictionary = dictionary;
        }

        /// <summary>
        /// Get the value for the key.
        /// </summary>
        /// <param name="key">The <see cref="TKey"/>.</param>
        /// <returns>The <see cref="TValue"/>.</returns>
        public TValue this[TKey key]
        {
            get
            {
                _dictionary.TryGetValue(key, out var value);
                return value;
            }
        }
    }
}
