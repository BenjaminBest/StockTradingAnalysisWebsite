using System;
using StockTradingAnalysis.Domain.Events.Events;
using StockTradingAnalysis.Interfaces.Events;

namespace StockTradingAnalysis.Domain.Events.Aggregates
{
    public class CalculationAggregate : AggregateRoot,
        IHandle<CalculationAddedEvent>,
        IHandle<CalculationRemovedEvent>,
        IHandle<CalculationNameChangedEvent>,
        IHandle<CalculationWknChangedEvent>,
        IHandle<CalculationMultiplierChangedEvent>,
        IHandle<CalculationStrikePriceChangedEvent>,
        IHandle<CalculationUnderlyingChangedEvent>,
        IHandle<CalculationInitialSlChangedEvent>,
        IHandle<CalculationInitialTpChangedEvent>,
        IHandle<CalculationPricePerUnitChangedEvent>,
        IHandle<CalculationOrderCostsChangedEvent>,
        IHandle<CalculationDescriptionChangedEvent>,
        IHandle<CalculationUnitsChangedEvent>,
        IHandle<CalculationIsLongChangedEvent>,
        IHandle<CalculationWasCopiedEvent>
    {
        /// <summary>
        /// Gets the aggregate id
        /// </summary>
        public override Guid Id { get; protected set; }

        /// <summary>
        /// Gets/sets the name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets/sets the wkn
        /// </summary>
        public string Wkn { get; private set; }

        /// <summary>
        /// Gets/sets the multiplier
        /// </summary>
        public decimal Multiplier { get; private set; }

        /// <summary>
        /// Gets/sets the strike price if selling
        /// </summary>
        public decimal? StrikePrice { get; private set; }

        /// <summary>
        /// Gets/sets the underlying
        /// </summary>
        public string Underlying { get; private set; }

        /// <summary>
        /// Gets/sets the initial stop loss
        /// </summary>
        public decimal InitialSl { get; private set; }

        /// <summary>
        /// Gets/sets the initial take profit
        /// </summary>
        public decimal InitialTp { get; private set; }

        /// <summary>
        /// Gets/sets the price per unit
        /// </summary>
        public decimal PricePerUnit { get; private set; }

        /// <summary>
        /// Gets/sets the order costs
        /// </summary>
        public decimal OrderCosts { get; private set; }

        /// <summary>
        /// Gets/sets the description
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Gets/sets the amount of units
        /// </summary>
        public decimal Units { get; private set; }

        /// <summary>
        /// Gets/sets if its about selling or buying
        /// </summary>
        public bool IsLong { get; private set; }

        /// <summary>
        /// Initializes this object
        /// </summary>
        public CalculationAggregate()
        {
        }

        /// <summary>
        /// Initializes this object
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="name">Name</param>
        /// <param name="orderCosts">Order costs</param>
        /// <param name="description">Description</param>
        /// <param name="wkn">Wkn</param>
        /// <param name="multiplier">Multiplier</param>
        /// <param name="strikePrice">StrikePrice</param>
        /// <param name="underlying">Underlying</param>
        /// <param name="initialSl">Initial stopp loss</param>
        /// <param name="initialTp">Initial take profit</param>
        /// <param name="pricePerUnit">Price per unit</param>
        /// <param name="units">Amount of units</param>
        /// <param name="isLong">Buying/selling</param>
        public CalculationAggregate(Guid id, string name, string wkn, decimal multiplier,
            decimal? strikePrice, string underlying, decimal initialSl, decimal initialTp, decimal pricePerUnit,
            decimal orderCosts, string description, decimal units, bool isLong)
        {
            ApplyChange(new CalculationAddedEvent(id, typeof (CalculationAggregate), name, wkn, multiplier, strikePrice,
                underlying, initialSl, initialTp, pricePerUnit, orderCosts, description, units, isLong));
        }

        /// <summary>
        /// Copy the given aggregate <paramref name="original"/>
        /// </summary>
        public void Copy(CalculationAggregate original, Guid newId)
        {
            var name = string.Format("{0} {1}", original.Name, DateTime.Now);

            ApplyChange(new CalculationWasCopiedEvent(newId, typeof (CalculationAggregate), original.Id, name,
                original.Wkn, original.Multiplier, original.StrikePrice,
                original.Underlying, original.InitialSl, original.InitialTp, original.PricePerUnit, original.OrderCosts,
                original.Description, original.Units, original.IsLong));
        }

        /// <summary>
        /// Changes the name
        /// </summary>
        public void ChangeName(string name)
        {
            if (!Equals(Name, name))
                ApplyChange(new CalculationNameChangedEvent(Id, typeof (CalculationAggregate), name));
        }

        /// <summary>
        /// Changes the wkn
        /// </summary>
        public void ChangeWkn(string wkn)
        {
            if (!Equals(Wkn, wkn))
                ApplyChange(new CalculationWknChangedEvent(Id, typeof (CalculationAggregate), wkn));
        }

        /// <summary>
        /// Changes the multiplier
        /// </summary>
        public void ChangeMultiplier(decimal multiplier)
        {
            if (Multiplier != multiplier)
                ApplyChange(new CalculationMultiplierChangedEvent(Id, typeof (CalculationAggregate), multiplier));
        }

        /// <summary>
        /// Changes the strike price if selling
        /// </summary>
        public void ChangeStrikePrice(decimal? strikePrice)
        {
            if (StrikePrice != strikePrice)
                ApplyChange(new CalculationStrikePriceChangedEvent(Id, typeof (CalculationAggregate), strikePrice));
        }

        /// <summary>
        /// Changes the underlying
        /// </summary>
        public void ChangeUnderlying(string underlying)
        {
            if (!Equals(Underlying, underlying))
                ApplyChange(new CalculationUnderlyingChangedEvent(Id, typeof (CalculationAggregate), underlying));
        }

        /// <summary>
        /// Changes the initial stopp loss
        /// </summary>
        public void ChangeInitialSl(decimal initialSl)
        {
            if (InitialSl != initialSl)
                ApplyChange(new CalculationInitialSlChangedEvent(Id, typeof (CalculationAggregate), initialSl));
        }

        /// <summary>
        /// Changes the initial take profit
        /// </summary>
        public void ChangeInitialTp(decimal initialTp)
        {
            if (InitialTp != initialTp)
                ApplyChange(new CalculationInitialTpChangedEvent(Id, typeof (CalculationAggregate), initialTp));
        }

        /// <summary>
        /// Changes the price per unit
        /// </summary>
        public void ChangePricePerUnit(decimal pricePerUnit)
        {
            if (PricePerUnit != pricePerUnit)
                ApplyChange(new CalculationPricePerUnitChangedEvent(Id, typeof (CalculationAggregate), pricePerUnit));
        }

        /// <summary>
        /// Changes the order costs
        /// </summary>
        public void ChangeOrderCosts(decimal orderCosts)
        {
            if (OrderCosts != orderCosts)
                ApplyChange(new CalculationOrderCostsChangedEvent(Id, typeof (CalculationAggregate), orderCosts));
        }

        /// <summary>
        /// Changes the description
        /// </summary>
        public void ChangeDescription(string description)
        {
            if (!Equals(Description, description))
                ApplyChange(new CalculationDescriptionChangedEvent(Id, typeof (CalculationAggregate), description));
        }

        /// <summary>
        /// Changes the units
        /// </summary>
        public void ChangeUnits(decimal units)
        {
            if (Units != units)
                ApplyChange(new CalculationUnitsChangedEvent(Id, typeof (CalculationAggregate), units));
        }

        /// <summary>
        /// Changes the if its about selling or buying
        /// </summary>
        public void ChangeIsLong(bool isLong)
        {
            if (IsLong != isLong)
                ApplyChange(new CalculationIsLongChangedEvent(Id, typeof (CalculationAggregate), isLong));
        }

        /// <summary>
        /// Removes the stock
        /// </summary>
        public void Remove()
        {
            ApplyChange(new CalculationRemovedEvent(Id, typeof (CalculationAggregate)));
        }

        /// <summary>
        /// Handles the given event <paramref name="event"/>
        /// </summary>
        /// <param name="event">The event</param>
        public void Handle(CalculationAddedEvent @event)
        {
            Id = @event.AggregateId;
            Name = @event.Name;
            Wkn = @event.Wkn;
            Multiplier = @event.Multiplier;
            StrikePrice = @event.StrikePrice;
            Underlying = @event.Underlying;
            InitialSl = @event.InitialSl;
            InitialTp = @event.InitialTp;
            PricePerUnit = @event.PricePerUnit;
            OrderCosts = @event.OrderCosts;
            Description = @event.Description;
            Units = @event.Units;
            IsLong = @event.IsLong;
        }

        /// <summary>
        /// Handles the given event <paramref name="event"/>
        /// </summary>
        /// <param name="event">The event</param>
        public void Handle(CalculationWasCopiedEvent @event)
        {
            Id = @event.AggregateId;
            Name = @event.Name;
            Wkn = @event.Wkn;
            Multiplier = @event.Multiplier;
            StrikePrice = @event.StrikePrice;
            Underlying = @event.Underlying;
            InitialSl = @event.InitialSl;
            InitialTp = @event.InitialTp;
            PricePerUnit = @event.PricePerUnit;
            OrderCosts = @event.OrderCosts;
            Description = @event.Description;
            Units = @event.Units;
            IsLong = @event.IsLong;
        }

        /// <summary>
        /// Handles the given event <paramref name="event"/>
        /// </summary>
        /// <param name="event">The event</param>
        public void Handle(CalculationRemovedEvent @event)
        {
        }


        /// <summary>
        /// Handles the given event <paramref name="event"/>
        /// </summary>
        /// <param name="event">The event</param>
        public void Handle(CalculationNameChangedEvent @event)
        {
            Name = @event.Name;
        }

        /// <summary>
        /// Handles the given event <paramref name="event"/>
        /// </summary>
        /// <param name="event">The event</param>
        public void Handle(CalculationWknChangedEvent @event)
        {
            Wkn = @event.Wkn;
        }

        /// <summary>
        /// Handles the given event <paramref name="event"/>
        /// </summary>
        /// <param name="event">The event</param>
        public void Handle(CalculationMultiplierChangedEvent @event)
        {
            Multiplier = @event.Multiplier;
        }

        /// <summary>
        /// Handles the given event <paramref name="event"/>
        /// </summary>
        /// <param name="event">The event</param>
        public void Handle(CalculationStrikePriceChangedEvent @event)
        {
            StrikePrice = @event.StrikePrice;
        }

        /// <summary>
        /// Handles the given event <paramref name="event"/>
        /// </summary>
        /// <param name="event">The event</param>
        public void Handle(CalculationUnderlyingChangedEvent @event)
        {
            Underlying = @event.Underlying;
        }

        /// <summary>
        /// Handles the given event <paramref name="event"/>
        /// </summary>
        /// <param name="event">The event</param>
        public void Handle(CalculationInitialSlChangedEvent @event)
        {
            InitialSl = @event.InitialSl;
        }

        /// <summary>
        /// Handles the given event <paramref name="event"/>
        /// </summary>
        /// <param name="event">The event</param>
        public void Handle(CalculationInitialTpChangedEvent @event)
        {
            InitialTp = @event.InitialTp;
        }

        /// <summary>
        /// Handles the given event <paramref name="event"/>
        /// </summary>
        /// <param name="event">The event</param>
        public void Handle(CalculationPricePerUnitChangedEvent @event)
        {
            PricePerUnit = @event.PricePerUnit;
        }

        /// <summary>
        /// Handles the given event <paramref name="event"/>
        /// </summary>
        /// <param name="event">The event</param>
        public void Handle(CalculationOrderCostsChangedEvent @event)
        {
            OrderCosts = @event.OrderCosts;
        }

        /// <summary>
        /// Handles the given event <paramref name="event"/>
        /// </summary>
        /// <param name="event">The event</param>
        public void Handle(CalculationDescriptionChangedEvent @event)
        {
            Description = @event.Description;
        }

        /// <summary>
        /// Handles the given event <paramref name="event"/>
        /// </summary>
        /// <param name="event">The event</param>
        public void Handle(CalculationUnitsChangedEvent @event)
        {
            Units = @event.Units;
        }

        /// <summary>
        /// Handles the given event <paramref name="event"/>
        /// </summary>
        /// <param name="event">The event</param>
        public void Handle(CalculationIsLongChangedEvent @event)
        {
            IsLong = @event.IsLong;
        }
    }
}