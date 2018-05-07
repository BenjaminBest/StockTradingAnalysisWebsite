using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using StockTradingAnalysis.Domain.Events.Exceptions;
using StockTradingAnalysis.Interfaces.Events;

namespace StockTradingAnalysis.Domain.Events.Aggregates
{
    /// <summary>
    /// The class AggregateEventApplier looks up and calls the proper overload of
    /// Handle(SpecificEventType event). Reflection information is cached statically once per type. 
    /// </summary>
    public static class AggregateEventApplier
    {
        /// <summary>
        /// Returns the cache of all types.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        private static class Cache<T>
        {
            public static readonly IDictionary<Type, MethodInfo> Dict = typeof(T)
                .GetMethods()
                .Where(m => m.Name == "Handle")
                .Where(m => m.GetParameters().Length == 1)
                .ToDictionary(m => m.GetParameters().First().ParameterType, m => m);
        }

        /// <summary>
        /// Calls the overload of the handle method with the given <paramref name="event"/>.
        /// </summary>
        /// <typeparam name="TAggregate">The type of the event</typeparam>
        /// <param name="aggregate">The aggregate instance</param>
        /// <param name="event">The event</param>
        public static void InvokeEvent<TAggregate>(TAggregate aggregate, IDomainEvent @event)
        {
            var type = @event.GetType();
            if (!Cache<TAggregate>.Dict.TryGetValue(type, out var info))
            {
                var s = $"Failed to locate {typeof(TAggregate).Name}.Handle({type.Name})";
                throw new AggregateApplyException(s);
            }

            info.Invoke(aggregate, new object[] { @event });
        }
    }
}