using System;
using System.Collections.Generic;
using System.Linq;
using StockTradingAnalysis.Core.Common;
using StockTradingAnalysis.Interfaces.Domain;

namespace StockTradingAnalysis.Domain.Events.Domain
{
    /// <summary>
    /// Defines a bucket for multiple transactions <see cref="ITransactionBookEntry"/> grouped by the stock id
    /// and uses FIFO to calculate the sold shares
    /// </summary>
    public class TransactionBook : ITransactionBook
    {
        /// <summary>
        /// All open positions
        /// </summary>
        private readonly DictionaryList<Guid, ITransactionBookEntry> _entries = new DictionaryList<Guid, ITransactionBookEntry>();

        /// <summary>
        /// The last changes that were made
        /// </summary>
        private readonly DictionaryList<Guid, ITransactionBookEntry> _changedEntries = new DictionaryList<Guid, ITransactionBookEntry>();

        /// <summary>
        /// Available units to sell for all positions
        /// </summary>
        private readonly IDictionary<Guid, IOpenPosition> _positions = new Dictionary<Guid, IOpenPosition>();

        /// <summary>
        /// Returns the amount of units which could be sold
        /// </summary>
        /// <param name="stockId">The id of the stock</param>
        /// <returns>Amount of units to sell</returns>
        public IOpenPosition GetOrAddOpenPosition(Guid stockId)
        {
            if (!_positions.ContainsKey(stockId))
                _positions.Add(stockId, new OpenPosition(stockId));

            return _positions[stockId];
        }

        /// <summary>
        /// Returns the amount of units which could be sold
        /// </summary>
        /// <returns>Amount of units to sell</returns>
        public IEnumerable<IOpenPosition> GetOpenPositions()
        {
            return _positions.Values;
        }

        /// <summary>
        /// Adds an entry to this bucket
        /// </summary>
        /// <param name="entry">The entry for a transaction</param>
        public void AddEntry(ITransactionBookEntry entry)
        {
            if (!(entry is ISplitTransactionBookEntry))
                _entries.Add(entry.StockId, entry.Copy());

            AdjustAvailableUnits(entry.Copy());
        }

        /// <summary>
        /// Returns all changes that were made by the commit (sell)
        /// </summary>
        /// <param name="stockId">The id of the stock</param>
        /// <returns>Book entries</returns>
        public IEnumerable<ITransactionBookEntry> GetLastCommittedChanges(Guid stockId)
        {
            return _changedEntries.GetOrAdd(stockId).OrderBy(e => e.OrderDate);
        }

        /// <summary>
        /// Adjusts the amount of available units for every transaction
        /// </summary>
        /// <param name="newEntry">New entry</param>
        private void AdjustAvailableUnits(ITransactionBookEntry newEntry)
        {
            var id = newEntry.StockId;

            if (newEntry is ISellingTransactionBookEntry && newEntry.Shares > GetOrAddOpenPosition(id).Shares)
                throw new InvalidOperationException("Cannot sell more units than available.");

            _changedEntries.Clear(id);

            if (newEntry is IBuyingTransactionBookEntry)
            {
                UpdateOpenPosition(id);
                return;
            }

            if (newEntry is ISplitTransactionBookEntry split)
            {
                var buys = _entries.GetOrAdd(id).Where(e => e is IBuyingTransactionBookEntry).ToList();

                buys.ForEach(t => _entries.Delete(id, t));
                _entries.Add(id, split.CreatePositionAfterSplit(buys));

                UpdateOpenPosition(id);
                return;
            }

            var newUnitsToSell = newEntry.Shares;

            var deletes = new List<ITransactionBookEntry>();

            foreach (var entry in _entries.GetOrAdd(id).OrderBy(e => e.OrderDate))
            {
                if (entry is IBuyingTransactionBookEntry)
                {
                    var changed = entry.Copy();

                    //Complete buying transaction filled
                    if (entry.Shares <= newUnitsToSell)
                    {
                        newUnitsToSell -= entry.Shares;

                        if (!(newEntry is IDividendTransactionBookEntry))
                            entry.Shares = 0;
                    }
                    //Partly filled
                    else
                    {
                        changed.Shares = newUnitsToSell;
                        changed.OrderCosts = (entry.OrderCosts / entry.Shares) * newUnitsToSell;

                        if (!(newEntry is IDividendTransactionBookEntry))
                        {
                            entry.OrderCosts = (entry.OrderCosts / entry.Shares) * (entry.Shares - newUnitsToSell);
                            entry.Shares -= newUnitsToSell;
                        }

                        newUnitsToSell = 0;
                    }

                    _changedEntries.Add(id, changed);

                    if (entry.Shares == 0 && !(newEntry is IDividendTransactionBookEntry))
                        deletes.Add(entry);

                    if (newUnitsToSell == 0)
                        break;
                }
            }

            _changedEntries.Add(id, newEntry);
            _entries.Delete(id, newEntry);

            if (!(newEntry is IDividendTransactionBookEntry))
                deletes.ForEach(t => _entries.Delete(id, t));

            UpdateOpenPosition(id);
        }

        /// <summary>
        /// Updates the open position for the given <paramref name="stockId"/>
        /// </summary>
        /// <param name="stockId">The id of an stock</param>       
        private void UpdateOpenPosition(Guid stockId)
        {
            if (!_positions.ContainsKey(stockId))
                _positions.Add(stockId, new OpenPosition(stockId));

            var transactions = _entries.GetOrAdd(stockId).Where(e => e is IBuyingTransactionBookEntry).ToList();

            var position = _positions[stockId];

            if (transactions.Any())
                position.FirstOrderDate = transactions.Min(t => t.OrderDate);

            position.Shares = transactions.Sum(t => t.Shares);
            position.PositionSize = transactions.Sum(t => t.PricePerShare * t.Shares + t.OrderCosts);
            position.PricePerShare = position.Shares == 0 ? 0 : position.PositionSize / position.Shares;
            position.OrderCosts = transactions.Sum(t => t.OrderCosts);

            //Open position was completely sold
            if (position.Shares == 0)
                _positions.Remove(stockId);
        }
    }
}