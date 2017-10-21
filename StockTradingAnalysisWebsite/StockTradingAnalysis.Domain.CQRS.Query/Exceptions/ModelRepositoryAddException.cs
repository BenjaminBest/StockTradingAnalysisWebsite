using System;

namespace StockTradingAnalysis.Domain.CQRS.Query.Exceptions
{
    public class ModelRepositoryAddException : Exception
    {
        /// <summary>
        /// Initializes this object
        /// </summary>
        public ModelRepositoryAddException()
        {
        }

        /// <summary>
        /// Initializes this object
        /// </summary>
        /// <param name="message">The message</param>
        public ModelRepositoryAddException(string message)
            : base(message)
        {
        }
    }
}