using System;

namespace StockTradingAnalysis.Interfaces.Domain
{
    /// <summary>
    /// Defines the interface for an image
    /// </summary>
    public interface IImage
    {
        /// <summary>
        /// Gets the id
        /// </summary>
        Guid Id { get; }

        /// <summary>
        /// Gets the contenttype
        /// </summary>
        string ContentType { get; }

        /// <summary>
        /// Gets the original name
        /// </summary>
        string OriginalName { get; }

        /// <summary>
        /// Gets the description
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Gets the image itself
        /// </summary>
        byte[] Data { get; }
    }
}