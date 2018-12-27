using System.Collections.Concurrent;
using StockTradingAnalysis.Core.Common;
using StockTradingAnalysis.Interfaces.Events;
using StockTradingAnalysis.Interfaces.Services;

namespace StockTradingAnalysis.EventSourcing.Messaging
{
	/// <summary>
	/// The interface IEventBus defines a bus, which starts event handler by incoming events and raises events.
	/// </summary>
	public class EventBus : IEventBus
	{
		/// <summary>
		/// The observers
		/// </summary>
		private readonly ConcurrentBag<IMessageObserver> _observers = new ConcurrentBag<IMessageObserver>();

		/// <summary>
		/// Raises the given event <paramref name="domainEvent"/>
		/// </summary>
		/// <typeparam name="TEvent">Type of the event</typeparam>
		/// <param name="domainEvent"></param>
		public void Publish<TEvent>(TEvent domainEvent) where TEvent : IDomainEvent
		{
			var eventType = domainEvent.GetType();

			var handlerType = typeof(IEventHandler<>).MakeGenericType(eventType);

			//Event handler
			foreach (var handler in DependencyResolver.Current.GetServices(handlerType))
			{
				var dyn = (dynamic)handler;

				dyn.Handle((dynamic)domainEvent);
			}

			//Notify observers
			foreach (var eventObserver in _observers)
			{
				eventObserver.Notify(domainEvent);
			}
		}

		/// <summary>
		/// Subscribes the specified event observer which should be notfied in case of an message.
		/// </summary>
		/// <param name="messageObserver">The message observer.</param>
		void IEventBus.Subscribe(IMessageObserver messageObserver)
		{
			_observers.Add(messageObserver);
		}
	}
}