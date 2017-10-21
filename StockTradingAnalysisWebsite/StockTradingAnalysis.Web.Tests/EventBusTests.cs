using System;
using System.Collections.Generic;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StockTradingAnalysis.EventSourcing.Messaging;
using StockTradingAnalysis.Interfaces.Events;
using StockTradingAnalysis.Web.Tests.Mocks;
using StockTradingAnalysis.Web.Tests.Objects;

namespace StockTradingAnalysis.Web.Tests
{
    [TestClass]
    public class EventBusTests
    {
        [TestMethod]
        [Description("The event bus should call an registered event handler")]
        public void EventBusShouldCallTheRegisteredEventHandler()
        {
            var result = String.Empty;

            var handlers = new List<DependecyDescriptor>
            {
                new DependecyDescriptor(typeof (IEventHandler<TestEvent>), new TestEventHandler((name) => result = name))
            };

            var eventBus = new EventBus(DependencyServiceMock.GetMock(handlers));

            eventBus.Publish(new TestEvent(Guid.NewGuid()));

            result.Should().Be("TestEvent");
        }

        [TestMethod]
        [Description("The event bus should not throw an exeption in no registered event handler is available")]
        public void EventBusShouldNotThrowAnExceptionWhenNoEventHandlerAvailable()
        {
            var result = String.Empty;

            var eventBus = new EventBus(DependencyServiceMock.GetMock(new List<DependecyDescriptor>()));

            eventBus.Publish(new TestEvent(Guid.NewGuid()));

            result.Should().Be("");
        }

        [TestMethod]
        [Description("The event bus should call the right event handler based on the event")]
        public void EventBusShouldCallTheRightEventHandler()
        {
            var result = String.Empty;

            var handlers = new List<DependecyDescriptor>
            {
                new DependecyDescriptor(typeof (IEventHandler<TestEvent>), new TestEventHandler((name) => result += name)),
                new DependecyDescriptor(typeof (IEventHandler<TestAlternativeEvent>),
                    new TestAlternativeEventHandler((name) => result += name))
            };

            var eventBus = new EventBus(DependencyServiceMock.GetMock(handlers));

            eventBus.Publish(new TestAlternativeEvent(Guid.NewGuid()));

            result.Should().Be("TestAlternativeEvent");
        }

        [TestMethod]
        [Description("The event bus should call an event handler which receives multiple specific events")]
        public void EventBusShouldCallAnEventHandlerWhichReceivesSpecificEvents()
        {
            var result = String.Empty;

            var handlers = new List<DependecyDescriptor>
            {
                new DependecyDescriptor(typeof (IEventHandler<TestEvent>), new TestAllSpecificEventHandler((name) => result += name)),
                new DependecyDescriptor(typeof (IEventHandler<TestAlternativeEvent>),
                    new TestAllSpecificEventHandler((name) => result += name))
            };

            var eventBus = new EventBus(DependencyServiceMock.GetMock(handlers));

            eventBus.Publish(new TestAlternativeEvent(Guid.NewGuid()));
            eventBus.Publish(new TestEvent(Guid.NewGuid()));

            result.Should().Be("TestAlternativeEventTestEvent");
        }
    }
}