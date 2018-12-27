using System;
using StockTradingAnalysis.Core.Common;
using StockTradingAnalysis.Domain.Process.Exceptions;
using StockTradingAnalysis.Interfaces.Common;
using StockTradingAnalysis.Interfaces.Events;
using StockTradingAnalysis.Interfaces.Services;

namespace StockTradingAnalysis.Domain.Process.Finders
{
	/// <summary>
	/// The ProcessManagerFinderRepository stores all process manager finders and based on a correlation id those can be found.
	/// </summary>
	/// <seealso cref="IProcessManagerFinderRepository" />
	public class ProcessManagerFinderRepository : IProcessManagerFinderRepository
	{
		/// <summary>
		/// The process manager repository
		/// </summary>
		private readonly IProcessManagerRepository _processManagerRepository;

		/// <summary>
		/// The logging service
		/// </summary>
		private readonly ILoggingService _loggingService;

		/// <summary>
		/// Initializes a new instance of the <see cref="ProcessManagerFinderRepository" /> class.
		/// </summary>
		/// <param name="processManagerRepository">The process manager repository.</param>
		/// <param name="loggingService"></param>
		public ProcessManagerFinderRepository(
			IProcessManagerRepository processManagerRepository,
			ILoggingService loggingService)
		{
			_processManagerRepository = processManagerRepository;
			_loggingService = loggingService;
		}

		/// <summary>
		/// Gets the process manager based on the given <paramref name="message"/>
		/// </summary>
		/// <param name="message">The message.</param>
		/// <returns></returns>
		public IProcessManager GetOrCreateProcessManager(IMessage message)
		{
			var messageType = message.GetType();
			var mapperType = typeof(IMessageCorrelationIdCreator<>).MakeGenericType(messageType);

			dynamic mapper = DependencyResolver.Current.GetService(mapperType);

			if (mapper == null)
				throw new ProcessManagerFinderException($"No process manager finder for message type {messageType} could be found");

			Guid correlationId = mapper.GetCorrelationId((dynamic)message);

			var processManager = _processManagerRepository.GetById(correlationId);

			//No process manager instance available, so create a new one
			if (processManager == null)
			{
				var processManagerType = typeof(IStartedByMessage<>).MakeGenericType(messageType);

				var result = DependencyResolver.Current.GetService(processManagerType);

				processManager = result as IProcessManager ?? throw new ProcessManagerFinderException("No process manager was found");
				processManager.Id = correlationId;

				_processManagerRepository.Add(processManager);
				_loggingService.Debug($"Created new instance of process manager '{processManager.GetType()}' for message '{message.GetType()}'");
			}
			else
			{
				_loggingService.Debug($"Found and re-used process manager '{processManager.GetType()}' for message '{message.GetType()}'");
			}

			processManager.RegisterForStatusUpdate(this);
			return processManager;
		}

		/// <summary>
		/// Adds the process manager.
		/// </summary>
		/// <param name="processManager">The process manager.</param>
		public void AddProcessManager(IProcessManager processManager)
		{
			if (_processManagerRepository.GetById(processManager.Id) != null)
				throw new ProcessManagerRepositoryAddException(
					"Replacing the instance of a process manager for a specific correlation id which already exists is not allowed");

			_processManagerRepository.Add(processManager);
		}

		/// <summary>
		/// Marks the process as completed.
		/// </summary>
		/// <param name="processManager">The process manager.</param>
		public void MarkAsCompleted(IProcessManager processManager)
		{
			if (_processManagerRepository.GetById(processManager.Id) != null)
				_processManagerRepository.Delete(processManager);
		}
	}
}
