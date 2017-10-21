using System;
using System.Diagnostics;

namespace StockTradingAnalysis.Core.Common
{
    /// <summary>
    /// The class TimeMeasure is used to wrap it around a using block and stop the amount of time it takes to execute the block.
    /// </summary>
    public class TimeMeasure : IDisposable
    {
        private readonly Stopwatch _watch = new Stopwatch();
        private readonly Action<long> _action;

        /// <summary>
        /// Initializes this object
        /// </summary>
        /// <param name="action">An action that will be excecuted when using block finished (disposing). It will receive
        /// the elapsed time in milliseconds</param>
        public TimeMeasure(Action<long> action)
        {
            _action = action;
            _watch.Start();
        }

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {
            _watch.Stop();
            _action(_watch.ElapsedMilliseconds);
        }
    }
}
