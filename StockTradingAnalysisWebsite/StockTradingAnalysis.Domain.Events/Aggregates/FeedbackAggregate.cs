using System;
using StockTradingAnalysis.Domain.Events.Events;
using StockTradingAnalysis.Interfaces.Events;

namespace StockTradingAnalysis.Domain.Events.Aggregates
{
    public class FeedbackAggregate : AggregateRoot,
        IHandle<FeedbackAddedEvent>,
        IHandle<FeedbackRemovedEvent>,
        IHandle<FeedbackDescriptionChangedEvent>,
        IHandle<FeedbackNameChangedEvent>
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
        /// Initializes this object
        /// </summary>
        public FeedbackAggregate()
        {
        }

        /// <summary>
        /// Initializes this object
        /// </summary>
        /// <param name="id">Id</param>
        /// <param name="name">Name</param>
        /// <param name="description">Description</param>
        public FeedbackAggregate(Guid id, string name, string description)
        {
            ApplyChange(new FeedbackAddedEvent(id, typeof (FeedbackAggregate), name, description));
        }

        /// <summary>
        /// Changes the description
        /// </summary>
        public void ChangeDescription(string description)
        {
            if (!Equals(Description, description))
                ApplyChange(new FeedbackDescriptionChangedEvent(Id, typeof (FeedbackAggregate), description));
        }

        /// <summary>
        /// Changes the name
        /// </summary>
        public void ChangeName(string name)
        {
            if (!Equals(Name, name))
                ApplyChange(new FeedbackNameChangedEvent(Id, typeof (FeedbackAggregate), name));
        }

        /// <summary>
        /// Removes the stock
        /// </summary>
        public void Remove()
        {
            ApplyChange(new FeedbackRemovedEvent(Id, typeof (FeedbackAggregate)));
        }

        /// <summary>
        /// Handles the given event <paramref name="event"/>
        /// </summary>
        /// <param name="event">The event</param>
        public void Handle(FeedbackAddedEvent @event)
        {
            Id = @event.AggregateId;
            Name = @event.Name;
            Description = @event.Description;
        }

        /// <summary>
        /// Handles the given event <paramref name="event"/>
        /// </summary>
        /// <param name="event">The event</param>
        public void Handle(FeedbackRemovedEvent @event)
        {
        }


        /// <summary>
        /// Handles the given event <paramref name="event"/>
        /// </summary>
        /// <param name="event">The event</param>
        public void Handle(FeedbackNameChangedEvent @event)
        {
            Name = @event.Name;
        }

        /// <summary>
        /// Handles the given event <paramref name="event"/>
        /// </summary>
        /// <param name="event">The event</param>
        public void Handle(FeedbackDescriptionChangedEvent @event)
        {
            Description = @event.Description;
        }
    }
}