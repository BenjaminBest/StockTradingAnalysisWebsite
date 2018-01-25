using System;
using StockTradingAnalysis.Interfaces.Events;

namespace StockTradingAnalysis.Domain.Events.Events
{
    public class FeedbackRemovedEvent : DomainEvent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="FeedbackRemovedEvent"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="aggregateType">Type of the aggregate.</param>
        public FeedbackRemovedEvent(Guid id, Type aggregateType)
            : base(id, aggregateType)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FeedbackRemovedEvent"/> class.
        /// </summary>
        protected FeedbackRemovedEvent()
        {
            
        }
    }
}