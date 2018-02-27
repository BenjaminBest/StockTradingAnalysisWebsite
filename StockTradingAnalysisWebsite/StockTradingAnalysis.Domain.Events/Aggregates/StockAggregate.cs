using System;
using System.Collections.Generic;
using System.Linq;
using StockTradingAnalysis.Core.Extensions;
using StockTradingAnalysis.Domain.Events.Events;
using StockTradingAnalysis.Domain.Events.Snapshots;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.DomainContext;
using StockTradingAnalysis.Interfaces.Events;

namespace StockTradingAnalysis.Domain.Events.Aggregates
{
    public class StockAggregate : AggregateRoot,
        IHandle<StockAddedEvent>,
        IHandle<StockRemovedEvent>,
        IHandle<StockLongShortChangedEvent>,
        IHandle<StockTypeChangedEvent>,
        IHandle<StockNameChangedEvent>,
        IHandle<StockWknChangedEvent>,
        IHandle<StockQuotationAddedEvent>,
        IHandle<StockQuotationChangedEvent>,
        IHandle<StockQuotationsAddedOrChangedEvent>,
        ISnapshotOriginator
    {
        /// <summary>
        /// Gets the aggregate id
        /// </summary>
        public override Guid Id { get; protected set; }

        /// <summary>
        /// Gets the name
        /// </summary>
        private string Name { get; set; }

        /// <summary>
        /// Gets the wkn
        /// </summary>
        private string Wkn { get; set; }

        /// <summary>
        /// Gets the type
        /// </summary>
        private string Type { get; set; }

        /// <summary>
        /// Gets if the stock is used when buying or selling
        /// </summary>
        private string LongShort { get; set; }

        /// <summary>
        /// List of quotations for this stock
        /// </summary>
        private HashSet<IQuotation> Quotations { get; set; }

        /// <summary>
        /// Initializes this object
        /// </summary>
        public StockAggregate()
        {
            Quotations = new HashSet<IQuotation>();
        }

        /// <summary>
        /// Initializes this object
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="name">Name</param>
        /// <param name="wkn">Wkn</param>
        /// <param name="type">Type</param>
        /// <param name="longShort">Long or short</param>
        /// <param name="quotations">Quotations</param>
        public StockAggregate(Guid id, string name, string wkn, string type, string longShort, IEnumerable<IQuotation> quotations)
        {
            ApplyChange(new StockAddedEvent(id, typeof(StockAggregate), name, wkn, type, longShort, quotations));
        }

        /// <summary>
        /// Changes the longShort
        /// </summary>
        public void ChangeLongShort(string longShort)
        {
            if (!Equals(LongShort, longShort))
                ApplyChange(new StockLongShortChangedEvent(Id, typeof(StockAggregate), longShort));
        }

        /// <summary>
        /// Changes the type
        /// </summary>
        public void ChangeType(string type)
        {
            if (!Equals(Type, type))
                ApplyChange(new StockTypeChangedEvent(Id, typeof(StockAggregate), type));
        }

        /// <summary>
        /// Changes the wkn
        /// </summary>
        public void ChangeWkn(string wkn)
        {
            if (!Equals(Wkn, wkn))
                ApplyChange(new StockWknChangedEvent(Id, typeof(StockAggregate), wkn));
        }

        /// <summary>
        /// Changes the name
        /// </summary>
        public void ChangeName(string name)
        {
            if (!Equals(Name, name))
                ApplyChange(new StockNameChangedEvent(Id, typeof(StockAggregate), name));
        }

        /// <summary>
        /// Removes the stock
        /// </summary>
        public void Remove()
        {
            ApplyChange(new StockRemovedEvent(Id, typeof(StockAggregate)));
        }

        /// <summary>
        /// Adds a stock price
        /// </summary>
        /// <remarks>Checks if quotation already exists, but has changed</remarks>
        public void AddOrChangeQuotation(IQuotation quotation)
        {
            if (Quotations.Contains(quotation))
            {
                var existentQuotation = Quotations.FirstOrDefault(q => q.Date.Equals(quotation.Date));

                if (existentQuotation != null &&
                    existentQuotation.Open == quotation.Open &&
                    existentQuotation.Close == quotation.Close &&
                    existentQuotation.High == quotation.High &&
                    existentQuotation.Low == quotation.Low)
                    return;

                ApplyChange(new StockQuotationChangedEvent(Id, typeof(StockAggregate), quotation));
            }
            else
            {
                ApplyChange(new StockQuotationAddedEvent(Id, typeof(StockAggregate), quotation));
            }
        }

        /// <summary>
        /// Adds multiple stock prices
        /// </summary>
        /// <remarks>Checks if quotation already exists, but has changed</remarks>
        public void AddOrChangeQuotations(IEnumerable<IQuotation> quotations)
        {
            var changedQuotations = new List<IQuotation>();

            foreach (var quotation in quotations)
            {
                if (Quotations.Contains(quotation))
                {
                    var existentQuotation = Quotations.FirstOrDefault(q => q.Date.Equals(quotation.Date));

                    if (existentQuotation != null &&
                        existentQuotation.Open == quotation.Open &&
                        existentQuotation.Close == quotation.Close &&
                        existentQuotation.High == quotation.High &&
                        existentQuotation.Low == quotation.Low)
                        return;

                    changedQuotations.Add(quotation);
                }
                else
                {
                    changedQuotations.Add(quotation);
                }
            }

            ApplyChange(new StockQuotationsAddedOrChangedEvent(Id, typeof(StockAggregate), changedQuotations));
        }

        /// <summary>
        /// Handles the given event <paramref name="event"/>
        /// </summary>
        /// <param name="event">The event</param>
        public void Handle(StockAddedEvent @event)
        {
            Id = @event.AggregateId;
            Name = @event.Name;
            Wkn = @event.Wkn;
            Type = @event.Type;
            LongShort = @event.LongShort;

            if (@event.Quotations != null)
                Quotations.AddRange(@event.Quotations);
        }

        /// <summary>
        /// Handles the given event <paramref name="event"/>
        /// </summary>
        /// <param name="event">The event</param>
        public void Handle(StockRemovedEvent @event)
        {
        }

        /// <summary>
        /// Handles the given event <paramref name="event"/>
        /// </summary>
        /// <param name="event">The event</param>
        public void Handle(StockLongShortChangedEvent @event)
        {
            LongShort = @event.LongShort;
        }

        /// <summary>
        /// Handles the given event <paramref name="event"/>
        /// </summary>
        /// <param name="event">The event</param>
        public void Handle(StockTypeChangedEvent @event)
        {
            Type = @event.Type;
        }

        /// <summary>
        /// Handles the given event <paramref name="event"/>
        /// </summary>
        /// <param name="event">The event</param>
        public void Handle(StockNameChangedEvent @event)
        {
            Name = @event.Name;
        }

        /// <summary>
        /// Handles the given event <paramref name="event"/>
        /// </summary>
        /// <param name="event">The event</param>
        public void Handle(StockWknChangedEvent @event)
        {
            Wkn = @event.Wkn;
        }

        /// <summary>
        /// Handles the given event <paramref name="event"/>
        /// </summary>
        /// <param name="event">The event</param>
        public void Handle(StockQuotationAddedEvent @event)
        {
            Quotations.Add(@event.Quotation);
        }

        /// <summary>
        /// Handles the given event <paramref name="event"/>
        /// </summary>
        /// <param name="event">The event</param>
        public void Handle(StockQuotationChangedEvent @event)
        {
            Quotations.RemoveWhere(p => p.Date == @event.Quotation.Date);
            Quotations.Add(@event.Quotation);
        }

        /// <summary>
        /// Handles the given event <paramref name="event" />
        /// </summary>
        /// <param name="event">The event</param>
        /// <exception cref="NotImplementedException"></exception>
        public void Handle(StockQuotationsAddedOrChangedEvent @event)
        {
            Quotations.RemoveWhere(q => @event.Quotations.Contains(q));
            Quotations.AddRange(@event.Quotations);
        }

        #region snapshots

        /// <summary>
        /// Returns a snapshot of the instance
        /// </summary>
        /// <returns>Snapshot</returns>
        public SnapshotBase GetSnapshot()
        {
            return new StockAggregateSnapshot(Id, Version, Name, Wkn, Type, LongShort, Quotations);
        }

        /// <summary>
        /// Applies the given <paramref name="snapshot"/> to this instance
        /// </summary>
        /// <param name="snapshot">The saved state</param>
        public void SetSnapshot(SnapshotBase snapshot)
        {
            var item = snapshot as StockAggregateSnapshot;

            if (item == null)
                return;

            Version = item.Version;
            Id = item.AggregateId;

            Name = item.Name;
            Wkn = item.Wkn;
            Type = item.Type;
            LongShort = item.LongShort;

            Quotations = item.Quotations;
        }

        /// <summary>
        /// Gets the id of the originator
        /// </summary>
        public Guid OriginatorId => Id;

        #endregion
    }
}