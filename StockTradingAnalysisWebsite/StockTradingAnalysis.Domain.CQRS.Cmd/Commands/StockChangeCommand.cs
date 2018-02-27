using System;
using StockTradingAnalysis.Interfaces.Commands;

namespace StockTradingAnalysis.Domain.CQRS.Cmd.Commands
{
    public class StockChangeCommand : Command
    {
        /// <summary>
        /// Gets the name
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the wkn
        /// </summary>
        public string Wkn { get; private set; }

        /// <summary>
        /// Gets the type
        /// </summary>
        public string Type { get; private set; }

        /// <summary>
        /// Gets if the stock is used when buying or selling
        /// </summary>
        public string LongShort { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="StockChangeCommand"/> class.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="version">The version.</param>
        /// <param name="name">The name.</param>
        /// <param name="wkn">The WKN.</param>
        /// <param name="type">The type.</param>
        /// <param name="longShort">The long short.</param>
        public StockChangeCommand(Guid id, int version, string name, string wkn, string type, string longShort)
            : base(version, id)
        {
            Name = name;
            Wkn = wkn;
            Type = type;
            LongShort = longShort;
        }
    }
}