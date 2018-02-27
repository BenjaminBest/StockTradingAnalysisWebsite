using System;
using StockTradingAnalysis.Interfaces.Events;

namespace StockTradingAnalysis.Domain.Events.Events
{
    /// <summary>
    /// The event StaticsticsBasicDataChangedEvent is fired when some part of the basic data which is needed to calculate the statistics has changed.
    /// </summary>
    /// <seealso cref="DomainEvent" />
    public class StaticsticsBasicDataChangedEvent : DomainEvent
    {
        /// <summary>
        /// Gets the date where the basic data has changed.
        /// </summary>
        /// <value>
        /// The start.
        /// </value>
        public DateTime Date { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="StaticsticsBasicDataChangedEvent" /> class.
        /// </summary>
        /// <param name="date">The date.</param>
        public StaticsticsBasicDataChangedEvent(DateTime date)
        {
            Date = date;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StaticsticsBasicDataChangedEvent"/> class.
        /// </summary>
        protected StaticsticsBasicDataChangedEvent()
        {

        }
    }
}