using System;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StockTradingAnalysis.EventSourcing.Exceptions;
using StockTradingAnalysis.EventSourcing.Storage;
using StockTradingAnalysis.Interfaces.Common;
using StockTradingAnalysis.Interfaces.Events;
using StockTradingAnalysis.Interfaces.Services.Core;

namespace StockTradingAnalysis.EventSourcing.Tests.Storage
{
    [TestClass]
    public class EventStoreInitializerTests
    {
        [TestMethod]
        [Description("The eventstoreinitializer should throw an exception when started twice")]
        public void EventStoreInitializerShouldThrowExceptionWhenReplayCalledTwice()
        {
            var eventBus = new Mock<IEventBus>();
            var loggingService = new Mock<ILoggingService>();
            var performance = new Mock<IPerformanceMeasurementService>();

            var eventStore = new Mock<IEventStore>();
            eventStore.Setup(s => s.GetEvents()).Returns(Enumerable.Empty<IDomainEvent>());

            var initializer = new EventStoreInitializer(eventStore.Object, eventBus.Object, loggingService.Object, performance.Object);
            initializer.Replay();

            Action action = () => initializer.Replay();
            action.ShouldThrow<EventStoreInitializeException>();
        }
    }
}
