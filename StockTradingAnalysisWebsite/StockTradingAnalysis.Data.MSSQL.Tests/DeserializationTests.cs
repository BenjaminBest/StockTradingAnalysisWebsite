using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StockTradingAnalysis.Domain.Events.Events;
using StockTradingAnalysis.Interfaces.Configuration;
using StockTradingAnalysis.Interfaces.Events;
using StockTradingAnalysis.Interfaces.Services.Core;

namespace StockTradingAnalysis.Data.MSSQL.Tests
{
    [TestClass]
    public class DeserializationTests
    {
        private string _transactionPerformanceCalculatedEvent =
                "{\"$type\":\"StockTradingAnalysis.Domain.Events.Events.TransactionPerformanceCalculatedEvent, StockTradingAnalysis.Domain.Events\",\"ProfitAbsolute\":77.36,\"ProfitPercentage\":30.94,\"ProfitMade\":true,\"HoldingPeriod\":{\"$type\":\"StockTradingAnalysis.Interfaces.Types.HoldingPeriod, StockTradingAnalysis.Interfaces\",\"IsIntradayTrade\":false,\"Period\":\"6.00:11:00\",\"StartDate\":\"2011-05-10T10:51:00\",\"EndDate\":\"2011-05-16T11:02:00\"},\"R\":1.11,\"ExitEfficiency\":null,\"EntryEfficiency\":null,\"MAEAbsolute\":null,\"MFEAbsolute\":null,\"TimeStamp\":\"2018-01-25T10:07:17.0933351+01:00\",\"AggregateId\":\"fc03fe82-6a67-40b1-95c5-1579e2d74f2c\",\"AggregateType\":\"StockTradingAnalysis.Domain.Events.Aggregates.TransactionAggregate, StockTradingAnalysis.Domain.Events, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null\",\"Id\":\"b790f593-febf-424d-8f73-638f61a7f96e\",\"EventName\":\"TransactionPerformanceCalculatedEvent\",\"Version\":1}";

        [TestMethod]
        [Description("The eventstore should deserialize to the correct object type with all properties correctly filled")]
        public void EventDatastoreDeserializeShouldCorrectlyDeserializeTransactionPerformanceCalculatedEvent()
        {
            var configuration = new Mock<IConfigurationRegistry>();
            configuration.Setup(s => s.GetValue<string>("connection")).Returns("connection");

            var value = new EventDatastore("connection", "tableName", new Mock<IPerformanceMeasurementService>().Object, configuration.Object).Deserialize<IDomainEvent>(_transactionPerformanceCalculatedEvent);

            value.EventName.Should().Be("TransactionPerformanceCalculatedEvent");
            value.TimeStamp.Should().Be(DateTime.Parse("2018-01-25T10:07:17.0933351+01:00"));
            value.Id.Should().Be(Guid.Parse("b790f593-febf-424d-8f73-638f61a7f96e"));

            var castedValue = value as TransactionPerformanceCalculatedEvent;
            castedValue.Should().NotBeNull();

            castedValue.ProfitAbsolute.Should().Be((decimal)77.36);
            castedValue.ProfitPercentage.Should().Be((decimal)30.94);
            castedValue.ProfitMade.Should().BeTrue();
            castedValue.HoldingPeriod.Should().NotBeNull();
            castedValue.HoldingPeriod.IsIntradayTrade.Should().BeFalse();
            castedValue.HoldingPeriod.Period.Should().Be(TimeSpan.Parse("6.00:11:00"));
            castedValue.HoldingPeriod.StartDate.Should().Be(DateTime.Parse("2011-05-10T10:51:00"));
            castedValue.HoldingPeriod.EndDate.Should().Be(DateTime.Parse("2011-05-16T11:02:00"));
            castedValue.R.Should().Be((decimal)1.11);
        }
    }
}
