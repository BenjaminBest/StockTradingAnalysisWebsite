using System;
using StockTradingAnalysis.Interfaces.Domain;

namespace StockTradingAnalysis.Domain.Events.Domain
{
    /// <summary>
    /// Defines a strategy
    /// </summary>
    public class Strategy : IStrategy
    {
        /// <summary>
        /// Initializes this object
        /// </summary>
        /// <param name="id">The id of a feedback</param>
        public Strategy(Guid id)
        {
            Id = id;
        }

        /// <summary>
        /// Gets/sets the id
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
        /// Gets/sets the image
        /// </summary>
        public IImage Image { get; set; }
    }
}