using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StockTradingAnalysis.Domain.CQRS.Query.Exceptions;
using StockTradingAnalysis.Domain.CQRS.Query.ReadModel;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Services.Domain;

namespace StockTradingAnalysis.Domain.CQRS.Query.Tests.ReadModel
{
    [TestClass]
    public class TimeRangeModelRepositoryTests
    {
        [TestMethod]
        [Description("TimeRangeModelRepository should properly delete all items when DeleteAll is called")]
        public void TimeRangeModelRepositoryDeleteAllShouldDeleteAllItems()
        {
            var item1 = new Statistic(DateTime.MinValue, DateTime.MaxValue);
            var item2 = new Statistic(DateTime.Parse("2018-01-01 00:00:00"), DateTime.Parse("2018-03-31 00:00:00"));

            var repository = new TimeRangeModelRepository<IStatistic>();
            repository.Add(item1);
            repository.Add(item2);

            repository.DeleteAll();
            repository.GetAll().Should().HaveCount(0);
        }

        [TestMethod]
        [Description("TimeRangeModelRepository should properly add new items when their keys are different")]
        public void TimeRangeModelRepositoryAddShouldAddNewItems()
        {
            var item1 = new Statistic(DateTime.MinValue, DateTime.MaxValue);
            var item2 = new Statistic(DateTime.Parse("2018-01-01 00:00:00"), DateTime.Parse("2018-03-31 00:00:00"));

            var repository = new TimeRangeModelRepository<IStatistic>();
            repository.Add(item1);
            repository.Add(item2);

            repository.GetAll().Should().HaveCount(2);
        }

        [TestMethod]
        [Description("TimeRangeModelRepository should throw an exception when item already exists")]
        public void TimeRangeModelRepositoryAddShouldThrowExceptionWhenItemExists()
        {
            var item1 = new Statistic(DateTime.MinValue, DateTime.MaxValue);
            var item2 = new Statistic(DateTime.MinValue, DateTime.MaxValue);

            var repository = new TimeRangeModelRepository<IStatistic>();

            Action act = () => { repository.Add(item1); repository.Add(item2); };

            act.ShouldThrow<ModelRepositoryAddException>();
        }

        [TestMethod]
        [Description("TimeRangeModelRepository should throw an exception when item is null")]
        public void TimeRangeModelRepositoryAddShouldThrowExceptionWhenItemIsNull()
        {
            var repository = new TimeRangeModelRepository<IStatistic>();

            Action act = () => { repository.Add(null); };

            act.ShouldThrow<ModelRepositoryAddException>();
        }

        [TestMethod]
        [Description("TimeRangeModelRepository should return correct statisic object based on the key")]
        public void TimeRangeModelRepositoryGetByIdShouldReturnCorrectObjectBasedOnKey()
        {
            var item1 = new Statistic(DateTime.MinValue, DateTime.MaxValue);
            var item2 = new Statistic(DateTime.Parse("2018-01-01 00:00:00"), DateTime.Parse("2018-03-31 00:00:00"));

            var repository = new TimeRangeModelRepository<IStatistic>();

            repository.Add(item1);
            repository.Add(item2);

            var result = repository.GetById(Statistic.CreateKey(DateTime.MinValue, DateTime.MaxValue));
            result.Should().Be(item1);
        }

        [TestMethod]
        [Description("TimeRangeModelRepository should return all statisic objects")]
        public void TimeRangeModelRepositoryGetAllShouldReturnAllObjects()
        {
            var item1 = new Statistic(DateTime.MinValue, DateTime.MaxValue);
            var item2 = new Statistic(DateTime.Parse("2018-01-01 00:00:00"), DateTime.Parse("2018-03-31 00:00:00"));

            var repository = new TimeRangeModelRepository<IStatistic>();

            repository.Add(item1);
            repository.Add(item2);

            var result = repository.GetAll();
            result.Should().HaveCount(2);
        }

        [TestMethod]
        [Description("TimeRangeModelRepository should update correct time range")]
        public void TimeRangeModelRepositoryUpdateShouldUpdateCorrectTimeRange()
        {
            var item1 = new Statistic(DateTime.MinValue, DateTime.MaxValue) { AmountPositionTrades = 1 };
            var item2 = new Statistic(DateTime.Parse("2018-01-01 00:00:00"), DateTime.Parse("2018-03-31 00:00:00"));

            var repository = new TimeRangeModelRepository<IStatistic>();

            repository.Add(item1);
            repository.Add(item2);

            var item1Update = new Statistic(DateTime.MinValue, DateTime.MaxValue) { AmountPositionTrades = 2 };

            repository.Update(item1Update);
            repository.GetById(Statistic.CreateKey(DateTime.MinValue, DateTime.MaxValue)).AmountPositionTrades.Should()
                .Be(2);
        }


        [TestMethod]
        [Description("TimeRangeModelRepository should delete correct time range")]
        public void TimeRangeModelRepositoryDeleteShouldDeleteCorrectTimeRange()
        {
            var item1 = new Statistic(DateTime.MinValue, DateTime.MaxValue) { AmountPositionTrades = 1 };
            var item2 = new Statistic(DateTime.Parse("2018-01-01 00:00:00"), DateTime.Parse("2018-03-31 00:00:00"));

            var repository = new TimeRangeModelRepository<IStatistic>();

            repository.Add(item1);
            repository.Add(item2);

            repository.Delete(item2);
            repository.GetById(Statistic.CreateKey(DateTime.MinValue, DateTime.MaxValue)).Should().NotBeNull();
            repository.GetAll().Should().HaveCount(1);
        }
    }
}
