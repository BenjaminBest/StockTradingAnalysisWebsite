using System;

namespace StockTradingAnalysis.Domain.CQRS.Query.Exceptions
{
    public class ModelRepositoryUpdateException : Exception
    {
        /// <summary>
        /// Initializes this object
        /// </summary>
        public ModelRepositoryUpdateException()
        {
        }

        /// <summary>
        /// Initializes this object
        /// </summary>
        /// <param name="message">The message</param>
        public ModelRepositoryUpdateException(string message)
            : base(message)
        {
        }
    }
}