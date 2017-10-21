using StockTradingAnalysis.Domain.Events.Events;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.Events;
using System;

namespace StockTradingAnalysis.Domain.Events.Aggregates
{
    public class StrategyAggregate : AggregateRoot,
        IHandle<StrategyAddedEvent>,
        IHandle<StrategyRemovedEvent>,
        IHandle<StrategyDescriptionChangedEvent>,
        IHandle<StrategyNameChangedEvent>,
        IHandle<StrategyImageChangedEvent>
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
        /// Gets the description
        /// </summary>
        private string Description { get; set; }

        /// <summary>
        /// Gets the image
        /// </summary>
        private IImage Image { get; set; }

        /// <summary>
        /// Initializes this object
        /// </summary>
        public StrategyAggregate()
        {
        }

        /// <summary>
        /// Initializes this object
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="name">Name</param>
        /// <param name="description">Description</param>
        /// <param name="image">Image</param>
        public StrategyAggregate(Guid id, string name, string description, IImage image)
        {
            ApplyChange(new StrategyAddedEvent(id, typeof(StrategyAggregate), name, description, image));
        }

        /// <summary>
        /// Changes the description
        /// </summary>
        public void ChangeDescription(string description)
        {
            if (!Equals(Description, description))
                ApplyChange(new StrategyDescriptionChangedEvent(Id, typeof(StrategyAggregate), description));
        }

        /// <summary>
        /// Changes the name
        /// </summary>
        public void ChangeName(string name)
        {
            if (!Equals(Name, name))
                ApplyChange(new StrategyNameChangedEvent(Id, typeof(StrategyAggregate), name));
        }

        /// <summary>
        /// Changes the image
        /// </summary>
        public void ChangeImage(IImage image)
        {
            if (!Equals(Image, image))
            {
                ApplyChange(new StrategyImageChangedEvent(Id, typeof(StrategyAggregate), image));
            }
        }

        /// <summary>
        /// Removes the stock
        /// </summary>
        public void Remove()
        {
            ApplyChange(new StrategyRemovedEvent(Id, typeof(StrategyAggregate)));
        }

        /// <summary>
        /// Handles the given event <paramref name="event"/>
        /// </summary>
        /// <param name="event">The event</param>
        public void Handle(StrategyAddedEvent @event)
        {
            Id = @event.AggregateId;
            Name = @event.Name;
            Description = @event.Description;
            Image = @event.Image;
        }

        /// <summary>
        /// Handles the given event <paramref name="event"/>
        /// </summary>
        /// <param name="event">The event</param>
        public void Handle(StrategyRemovedEvent @event)
        {
        }


        /// <summary>
        /// Handles the given event <paramref name="event"/>
        /// </summary>
        /// <param name="event">The event</param>
        public void Handle(StrategyNameChangedEvent @event)
        {
            Name = @event.Name;
        }

        /// <summary>
        /// Handles the given event <paramref name="event"/>
        /// </summary>
        /// <param name="event">The event</param>
        public void Handle(StrategyDescriptionChangedEvent @event)
        {
            Description = @event.Description;
        }

        /// <summary>
        /// Handles the given event <paramref name="event"/>
        /// </summary>
        /// <param name="event">The event</param>
        public void Handle(StrategyImageChangedEvent @event)
        {
            Image = @event.Image;
        }
    }
}