using System;

namespace StockTradingAnalysis.Domain.Process.Exceptions
{
    /// <summary>
    /// The excepion ProcessManagerRepositoryAddException is thrown when a process manager could not be added to the repository
    /// </summary>
    /// <seealso cref="Exception" />
    public class ProcessManagerRepositoryAddException : Exception
    {
        /// <summary>
        /// Initializes this object
        /// </summary>
        public ProcessManagerRepositoryAddException()
        {
        }

        /// <summary>
        /// Initializes this object
        /// </summary>
        /// <param name="message">The message</param>
        public ProcessManagerRepositoryAddException(string message)
            : base(message)
        {
        }
    }
}