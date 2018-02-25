using System;

namespace StockTradingAnalysis.Domain.Process.Exceptions
{
    /// <summary>
    /// The excepion ProcessManagerFinderException is thrown when a process manager finder could not be found.
    /// </summary>
    /// <seealso cref="Exception" />
    public class ProcessManagerFinderException : Exception
    {
        /// <summary>
        /// Initializes this object
        /// </summary>
        public ProcessManagerFinderException()
        {
        }

        /// <summary>
        /// Initializes this object
        /// </summary>
        /// <param name="message">The message</param>
        public ProcessManagerFinderException(string message)
            : base(message)
        {
        }
    }
}