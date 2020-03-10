using System;
using StockTradingAnalysis.Interfaces.Domain;

namespace StockTradingAnalysis.Domain.Events.Domain
{
    /// <summary>
    /// Defines a feedback
    /// </summary>
    [Serializable]
    public class Feedback : IFeedback
    {
        /// <summary>
        /// Initializes this object
        /// </summary>
        /// <param name="id">The id of a feedback</param>
        public Feedback(Guid id)
        {
            Id = id;
        }

        /// <summary>
        /// Gets/sets the Id
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets/sets the version
        /// </summary>
        public int OriginalVersion { get; set; }

        /// <summary>
        /// Gets/sets the name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets/sets the description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets/sets the current overall performance for this stock
        /// </summary>
        public decimal Performance { get; set; }
    }
}