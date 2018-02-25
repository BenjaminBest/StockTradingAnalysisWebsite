using System.Collections.Concurrent;
using StockTradingAnalysis.Core.Common;
using StockTradingAnalysis.CQRS.Exceptions;
using StockTradingAnalysis.Interfaces.Commands;
using StockTradingAnalysis.Interfaces.Events;
using StockTradingAnalysis.Interfaces.Services;
using StockTradingAnalysis.Interfaces.Services.Core;

namespace StockTradingAnalysis.CQRS.CommandDispatcher
{
    /// <summary>
    /// The class CommandDispatcher is a bus for commands. It will find all command handlers which can handle a specific command and then executes the command handler
    /// by invoking the command. 
    /// </summary>
    /// <seealso cref="ICommandDispatcher" />
    public class CommandDispatcher : ICommandDispatcher
    {
        /// <summary>
        /// The dependency service
        /// </summary>
        private readonly IDependencyService _dependencyService;

        /// <summary>
        /// The performance measurement service
        /// </summary>
        private readonly IPerformanceMeasurementService _performanceMeasurementService;

        /// <summary>
        /// The observers
        /// </summary>
        private readonly ConcurrentBag<IMessageObserver> _observers = new ConcurrentBag<IMessageObserver>();

        /// <summary>
        /// Initializes this object
        /// </summary>
        /// <param name="dependencyService">The dependency service</param>
        /// <param name="performanceMeasurementService">The performance measurement service</param>
        public CommandDispatcher(
            IDependencyService dependencyService,
            IPerformanceMeasurementService performanceMeasurementService)
        {
            _dependencyService = dependencyService;
            _performanceMeasurementService = performanceMeasurementService;
        }

        /// <summary>
        /// Delegates the specified command to a <see cref="ICommandHandler{TCommand}" /> implementation.
        /// </summary>
        /// <param name="command">The command.</param>
        public void Execute(ICommand command)
        {
            using (new TimeMeasure(ms => _performanceMeasurementService.CountCommand(ms)))
            {
                if (command == null)
                    throw new CommandException("Command is null");

                var commandType = command.GetType();

                var handlerType = typeof(ICommandHandler<>).MakeGenericType(commandType);

                dynamic handler = _dependencyService.GetService(handlerType);

                if (handler == null)
                    throw new CommandDispatcherException(handlerType);

                handler.Execute((dynamic)command);

                //Notify observers
                foreach (var commandObserver in _observers)
                {
                    commandObserver.Notify(command);
                }
            }
        }

        /// <summary>
        /// Subscribes the specified message observer which should be notified in case of any message.
        /// </summary>
        /// <param name="messageObserver">The message observer.</param>
        public void Subscribe(IMessageObserver messageObserver)
        {
            _observers.Add(messageObserver);
        }
    }
}