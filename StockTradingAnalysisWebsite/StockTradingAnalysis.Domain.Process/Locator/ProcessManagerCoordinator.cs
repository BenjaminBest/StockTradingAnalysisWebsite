using System;
using System.Collections.Generic;
using StockTradingAnalysis.Interfaces.Commands;
using StockTradingAnalysis.Interfaces.Events;
using StockTradingAnalysis.Interfaces.Services;

namespace StockTradingAnalysis.Domain.Process.Locator
{
    /// <summary>
    /// The class ProcessManagerCoordinator locates a process manager by a correlation id and the types of events and command which the
    /// process manager is capable of handling.
    /// 
    /// Therefore the locator subscribes to all events from the <see cref="IEventBus"/> and all commands from the <see cref="ICommandDispatcher"/>.
    /// </summary>
    /// <seealso cref="IProcessManagerCoordinator" />
    public class ProcessManagerCoordinator : IProcessManagerCoordinator, IMessageObserver
    {
        /// <summary>
        /// The process manager repository
        /// </summary>
        private readonly IProcessManagerFinderRepository _processManagerFinderRepository;

        /// <summary>
        /// The dependency service
        /// </summary>
        private readonly IDependencyService _dependencyService;

        /// <summary>
        /// The observable message types
        /// </summary>
        private HashSet<Type> _observableMessageTypes;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessManagerCoordinator" /> class.
        /// </summary>
        /// <param name="eventBus">The event bus.</param>
        /// <param name="commandDispatcher">The command dispatcher.</param>
        /// <param name="processManagerFinderRepository">The process manager finder repository.</param>
        /// <param name="dependencyService">The dependency service.</param>
        public ProcessManagerCoordinator(
            IEventBus eventBus,
            ICommandDispatcher commandDispatcher,
            IProcessManagerFinderRepository processManagerFinderRepository,
            IDependencyService dependencyService)
        {
            _processManagerFinderRepository = processManagerFinderRepository;
            _dependencyService = dependencyService;

            //Register for events and commands
            eventBus.Subscribe(this);
            commandDispatcher.Subscribe(this);
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public void Initialize()
        {
            //Cache for supported message types
            _observableMessageTypes = GetObservableMessages();
        }

        /// <summary>
        /// Notifies the observer about the <paramref name="message" />.
        /// </summary>
        /// <typeparam name="TMessage">The type of the message.</typeparam>
        /// <param name="message">The message.</param>
        void IMessageObserver.Notify<TMessage>(TMessage message)
        {
            HandleMessage(message.GetType(), message);
        }

        /// <summary>
        /// Handles the message.
        /// </summary>
        /// <param name="messageType">Type of the message.</param>
        /// <param name="message">The message.</param>
        private void HandleMessage(Type messageType, IMessage message)
        {
            if (!_observableMessageTypes.Contains(messageType))
                return;

            var processManager = _processManagerFinderRepository.GetOrCreateProcessManager(message);

            //Handle command
            var dyn = (dynamic)processManager;
            dyn.Handle((dynamic)message);
        }

        /// <summary>
        /// Gets the observable messages.
        /// </summary>
        /// <returns></returns>
        private HashSet<Type> GetObservableMessages()
        {
            var result = new HashSet<Type>();

            foreach (var manager in _dependencyService.GetServices<IProcessManager>())
            {
                foreach (var it in manager.GetType().GetInterfaces())
                {
                    if (!it.IsGenericType || it.GetGenericTypeDefinition() != typeof(IStartedByMessage<>))
                        continue;

                    foreach (var genericArgument in it.GetGenericArguments())
                    {
                        //TODO: What if multiple process managers handling the same message type?
                        if (!result.Contains(genericArgument))
                            result.Add(genericArgument);
                    }
                }
            }

            return result;
        }
    }
}
