using System;

namespace StockTradingAnalysis.Interfaces.Commands
{
    /// <summary>
    /// Command defines a command which will be delivered to a command handler
    /// </summary>
    public class Command : ICommand
    {
        /// <summary>
        /// Gets the time when this command was created
        /// </summary>
        public DateTime TimeStamp { get; private set; }

        /// <summary>
        /// Gets the id of this command
        /// </summary>
        public Guid CommandId { get; private set; }

        /// <summary>
        /// Gets the id of this command
        /// </summary>
        public int OriginalVersion { get; private set; }

        /// <summary>
        /// Gets the id of the aggregate this command is for
        /// </summary>
        public Guid AggregateId { get; private set; }

        /// <summary>
        /// Initializes this object
        /// </summary>
        /// <param name="originalVersion">The original aggregate version</param>
        /// <param name="aggregateId">The aggregate id</param>
        public Command(int originalVersion, Guid aggregateId)
        {
            CommandId = Guid.NewGuid();
            TimeStamp = DateTime.Now;
            OriginalVersion = originalVersion;
            AggregateId = aggregateId;
        }
    }
}