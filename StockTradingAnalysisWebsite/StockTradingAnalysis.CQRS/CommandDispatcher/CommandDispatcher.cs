using StockTradingAnalysis.Core.Common;
using StockTradingAnalysis.CQRS.Exceptions;
using StockTradingAnalysis.Interfaces.Commands;
using StockTradingAnalysis.Interfaces.Services;

namespace StockTradingAnalysis.CQRS.CommandDispatcher
{
    public class CommandDispatcher : ICommandDispatcher
    {
        private readonly IDependencyService _dependencyService;
        private readonly IPerformanceMeasurementService _performanceMeasurementService;

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
            }
        }
    }
}