using log4net.Core;
using StockTradingAnalysis.Interfaces.Common;
using System;
using System.Reflection;
using System.Text;

namespace StockTradingAnalysis.Core.Services
{
    /// <summary>
    /// The LoggingService defines an logging service
    /// </summary>
    public class LoggingService : ILoggingService
    {
        private readonly ILogger _logger;

        /// <summary>
        /// Initializes this class
        /// </summary>
        public LoggingService()
        {
            _logger = LoggerManager.GetLogger(Assembly.GetCallingAssembly(), "Default");
        }

        /// <summary>
        /// Logs the given <paramref name="exception"/>
        /// </summary>
        /// <param name="exception">The Exception</param>
        public void Fatal(Exception exception)
        {
            if (!_logger.IsEnabledFor(Level.Fatal))
                return;

            _logger.Log(typeof(LoggingService), Level.Fatal, FlattenException(exception), null);
        }

        /// <summary>
        /// Logs the given <paramref name="message"/>
        /// </summary>
        /// <param name="message">The message</param>
        public void Fatal(string message)
        {
            if (!_logger.IsEnabledFor(Level.Fatal))
                return;

            _logger.Log(typeof(LoggingService), Level.Fatal, message, null);
        }

        /// <summary>
        /// Logs the given <paramref name="exception"/>
        /// </summary>
        /// <param name="exception">The Exception</param>
        public void Error(Exception exception)
        {
            if (!_logger.IsEnabledFor(Level.Error))
                return;

            _logger.Log(typeof(LoggingService), Level.Error, FlattenException(exception), null);
        }

        /// <summary>
        /// Logs the given <paramref name="message"/>
        /// </summary>
        /// <param name="message">The message</param>
        public void Error(string message)
        {
            if (!_logger.IsEnabledFor(Level.Error))
                return;

            _logger.Log(typeof(LoggingService), Level.Error, message, null);
        }

        /// <summary>
        /// Logs the given <paramref name="message"/>
        /// </summary>
        /// <param name="message">The message</param>
        public void Warn(string message)
        {
            if (!_logger.IsEnabledFor(Level.Warn))
                return;

            _logger.Log(typeof(LoggingService), Level.Warn, message, null);
        }

        /// <summary>
        /// Logs the given <paramref name="message"/>
        /// </summary>
        /// <param name="message">The message</param>
        public void Info(string message)
        {
            if (!_logger.IsEnabledFor(Level.Info))
                return;

            _logger.Log(typeof(LoggingService), Level.Info, message, null);
        }

        /// <summary>
        /// Logs the given <paramref name="message"/>
        /// </summary>
        /// <param name="message">The message</param>
        public void Debug(string message)
        {
            if (!_logger.IsEnabledFor(Level.Debug))
                return;

            _logger.Log(typeof(LoggingService), Level.Debug, message, null);
        }

        /// <summary>
        /// Formats the given <paramref name="exception"/> with their stacktrace and inner exceptions into a formats that can be logged
        /// </summary>
        /// <param name="exception">The exception</param>
        /// <returns></returns>
        private static string FlattenException(Exception exception)
        {
            var stringBuilder = new StringBuilder();

            while (exception != null)
            {
                stringBuilder.AppendLine(exception.Message);
                stringBuilder.AppendLine(exception.StackTrace);

                exception = exception.InnerException;
            }

            return stringBuilder.ToString();
        }
    }
}
