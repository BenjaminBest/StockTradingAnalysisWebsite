using System.Collections.Concurrent;
using System.Collections.Generic;

namespace StockTradingAnalysis.Core.Common
{
    /// <summary>
    /// The class DirectoryList is used to store lists of items based on a key
    /// </summary>
    /// <typeparam name="TKey">The type of the key</typeparam>
    /// <typeparam name="TItem">The type of the list item</typeparam>
    public class DictionaryList<TKey, TItem>
    {
        /// <summary>
        /// The items
        /// </summary>
        private readonly IDictionary<TKey, IList<TItem>> _items = new ConcurrentDictionary<TKey, IList<TItem>>();

        /// <summary>
        /// Returns a list of all items which are assigned to the given <paramref name="key"/>. If the list
        /// doesnt exist, it will be created and added to the internal directory as well.
        /// </summary>
        /// <param name="key">Key</param>
        /// <returns>List of all items</returns>
        public IList<TItem> GetOrAdd(TKey key)
        {
            IList<TItem> items;

            if (!_items.TryGetValue(key, out items))
            {
                items = new List<TItem>();
                _items.Add(key, items);
            }

            return items;
        }

        /// <summary>
        /// Adds the given <paramref name="item"/> to the list with the given <paramref name="key"/>
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="item">Item</param>
        public void Add(TKey key, TItem item)
        {
            var items = GetOrAdd(key);

            items.Add(item);
        }

        /// <summary>
        /// Removes the entry with the given key
        /// </summary>
        /// <param name="key">Key</param>
        public void Delete(TKey key)
        {
            if (_items.ContainsKey(key))
                _items.Remove(key);
        }

        /// <summary>
        /// Removes the given item from the list with the given key
        /// </summary>
        /// <param name="key">Key</param>
        /// <param name="item">The item.</param>
        public void Delete(TKey key, TItem item)
        {
            var items = GetOrAdd(key);

            items.Remove(item);
        }

        /// <summary>
        /// Removes all entries from the list with the given key
        /// </summary>
        /// <param name="key">Key</param>
        public void Clear(TKey key)
        {
            if (_items.ContainsKey(key))
                _items[key].Clear();
        }

        /// <summary>
        /// Removes all entries
        /// </summary>
        public void Clear()
        {
            _items.Clear();
        }
    }
}
