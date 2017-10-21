using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StockTradingAnalysis.EventSourcing.Exceptions;
using StockTradingAnalysis.EventSourcing.Messaging;
using StockTradingAnalysis.EventSourcing.Storage;
using StockTradingAnalysis.Web.Tests.Mocks;
using StockTradingAnalysis.Web.Tests.Objects;

namespace StockTradingAnalysis.Web.Tests
{
    [TestClass]
    public class EventStoreInitializerTests
    {
        [TestMethod]
        [Description("Eventstore initializer should throw exception when started twice")]
        public void EventstoreInitializerShouldThrowExceptionWhenStartedTwice()
        {
            EventBus eventBus;
            EventStore eventStore;
            EnvironmentMock.CreateEnvironment<TestAggregate>(out eventBus, out eventStore);

            var initializer = new EventStoreInitializer(eventStore, eventBus);

            initializer.Replay();

            Action act = () => initializer.Replay();
            act.ShouldThrow<EventStoreInitializeException>();
        }
    }
}