using System;

namespace StockTradingAnalysis.Domain.CQRS.Query.Exceptions
{
    public class ModelRepositoryDeleteException : Exception
    {
        /// <summary>
        /// Initializes this object
        /// </summary>
        public ModelRepositoryDeleteException()
        {
        }

        /// <summary>
        /// Initializes this object
        /// </summary>
        /// <param name="message">The message</param>
        public ModelRepositoryDeleteException(string message)
            : base(message)
        {
        }
    }
}