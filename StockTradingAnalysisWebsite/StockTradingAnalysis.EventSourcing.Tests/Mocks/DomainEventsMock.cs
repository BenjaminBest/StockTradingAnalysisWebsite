using System;
using System.Collections.Generic;
using StockTradingAnalysis.Interfaces.Events;

namespace StockTradingAnalysis.EventSourcing.Tests.Mocks
{
    /// <summary>
    /// The class DomainEventsMock is used to create a list of events
    /// </summary>
    public static class DomainEventsMock
    {
        /// <summary>
        /// Creates an unordered list of domain events
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<IDomainEvent> CreateUnorderedList()
        {
            var list = new List<IDomainEvent>
            {
                new TestDomainEvent(Guid.Parse("9A68452D-D80B-4C58-B6A6-A2D6654F6E71"), typeof(DomainEventsMock))
                {
                    EventName = "2",
                    Id = Guid.Parse("9A68452D-D80B-4C58-B6A6-A2D6654F6E71"),
                    TimeStamp = DateTime.Parse("2018-01-02 15:00:00"),
                    Version = 1
                },
                new TestDomainEvent(Guid.Parse("D0C3E230-654F-4217-8E03-B2DC1BAE0B69"), typeof(DomainEventsMock))
                {
                    EventName = "1",
                    Id = Guid.Parse("D0C3E230-654F-4217-8E03-B2DC1BAE0B69"),
                    TimeStamp = DateTime.Parse("2018-01-01 15:00:00"),
                    Version = 0
                },
                new TestDomainEvent(Guid.Parse("7AB7CB93-ADF3-4E2E-88F9-6C9D36DFE440"), typeof(DomainEventsMock))
                {
                    EventName = "5",
                    Id = Guid.Parse("7AB7CB93-ADF3-4E2E-88F9-6C9D36DFE440"),
                    TimeStamp = DateTime.Parse("2018-05-01 15:00:00"),
                    Version = 0
                },
                new TestDomainEvent(Guid.Parse("8FCF5680-167C-4353-81E7-C4AD8408E1EF"), typeof(DomainEventsMock))
                {
                    EventName = "4",
                    Id = Guid.Parse("8FCF5680-167C-4353-81E7-C4AD8408E1EF"),
                    TimeStamp = DateTime.Parse("2018-04-01 15:00:00"),
                    Version = 0
                },
                new TestDomainEvent(Guid.Parse("226499F1-1265-4A28-83BD-89E8BBEB125C"), typeof(DomainEventsMock))
                {
                    EventName = "3",
                    Id = Guid.Parse("226499F1-1265-4A28-83BD-89E8BBEB125C"),
                    TimeStamp = DateTime.Parse("2018-03-01 15:00:00"),
                    Version = 0
                }
            };

            return list;
        }

        public class TestDomainEvent : IDomainEvent
        {
            /// <summary>
            /// Gets the time when this event was created
            /// </summary>
            public DateTime TimeStamp { get; set; }

            /// <summary>
            /// Gets the id of the aggregate this event is meant for
            /// </summary>
            public Guid AggregateId { get; set; }

            /// <summary>
            /// Gets the type of the aggregate this event is meant for
            /// </summary>
            public Type AggregateType { get; set; }

            /// <summary>
            /// Gets the id of this event
            /// </summary>
            public Guid Id { get; set; }

            /// <summary>
            /// Gets the name of this event
            /// </summary>
            public string EventName { get; set; }

            /// <summary>
            /// Gets or sets the version of this event in relation to the aggregate
            /// </summary>
            public int Version { get; set; }

            /// <summary>
            /// Initializes this object
            /// </summary>
            /// <param name="aggregateId">The id of an aggregate</param>
            /// <param name="aggregateType">The type of the aggregate</param>
            public TestDomainEvent(Guid aggregateId, Type aggregateType)
            {
                AggregateId = aggregateId;
                AggregateType = aggregateType;

                TimeStamp = DateTime.Now;
                EventName = GetType().Name;
                Id = Guid.NewGuid();
                Version = -1;
            }
        }
    }
}
