using System;

namespace StockTradingAnalysis.Domain.Process.Exceptions
{
    /// <summary>
    /// The ProcessManagerRepositoryDeleteException is thrown when a process manager could not be removed from the repository.
    /// </summary>
    /// <seealso cref="Exception" />
    public class ProcessManagerRepositoryDeleteException : Exception
    {
        /// <summary>
        /// Initializes this object
        /// </summary>
        public ProcessManagerRepositoryDeleteException()
        {
        }

        /// <summary>
        /// Initializes this object
        /// </summary>
        /// <param name="message">The message</param>
        public ProcessManagerRepositoryDeleteException(string message)
            : base(message)
        {
        }
    }
}