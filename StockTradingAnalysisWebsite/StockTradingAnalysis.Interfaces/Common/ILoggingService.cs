using System;

namespace StockTradingAnalysis.Interfaces.Common
{
    /// <summary>
    /// The interface ILoggingService defines an logging service
    /// </summary>
    public interface ILoggingService
    {
        /// <summary>
        /// Logs the given <paramref name="exception"/>
        /// </summary>
        /// <param name="exception">The Exception</param>
        void Fatal(Exception exception);

        /// <summary>
        /// Logs the given <paramref name="message"/>
        /// </summary>
        /// <param name="message">The message</param>
        void Fatal(string message);

        /// <summary>
        /// Logs the given <paramref name="exception"/>
        /// </summary>
        /// <param name="exception">The Exception</param>
        void Error(Exception exception);

        /// <summary>
        /// Logs the given <paramref name="message"/>
        /// </summary>
        /// <param name="message">The message</param>
        void Error(string message);

        /// <summary>
        /// Logs the given <paramref name="message"/>
        /// </summary>
        /// <param name="message">The message</param>
        void Warn(string message);

        /// <summary>
        /// Logs the given <paramref name="message"/>
        /// </summary>
        /// <param name="message">The message</param>
        void Info(string message);

        /// <summary>
        /// Logs the given <paramref name="message"/>
        /// </summary>
        /// <param name="message">The message</param>
        void Debug(string message);
    }
}