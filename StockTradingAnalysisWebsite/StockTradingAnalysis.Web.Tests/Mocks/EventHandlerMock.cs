using Moq;
using StockTradingAnalysis.Interfaces.Events;
using System;

namespace StockTradingAnalysis.Web.Tests.Mocks
{
    public class EventHandlerMock
    {
        public static IEventHandler<IDomainEvent> GetMockWithExcecuteAction<TEvent>(Action action) where TEvent : IDomainEvent
        {
            var mock = new Mock<IEventHandler<IDomainEvent>>();
            mock.Setup(s => s.Handle(It.IsAny<TEvent>())).Callback(action);

            return mock.Object;
        }
    }
}