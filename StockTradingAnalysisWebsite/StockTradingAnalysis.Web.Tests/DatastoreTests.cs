using System;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StockTradingAnalysis.Web.Tests.Mocks;
using StockTradingAnalysis.Web.Tests.Objects;

namespace StockTradingAnalysis.Web.Tests
{
	[TestClass]
	public class DatastoreTests
	{
		[Ignore] //NOTE: RavenDB should be updated to 4.0
		[TestMethod]
		[Description("Datastore should load and save events with no eventual consistency problem")]
		public void DatastoreShouldLoadAndSaveWithNoEventualConsistency()
		{
			var repository = EnvironmentMock.CreateDatabaseEnvironment<TestSnapshotAggregate>(out _,
				out _,
				out _, out var documentEventStore, out _, out var documentSnapshotStore, 100);

			var guid = Guid.NewGuid();

			repository.Save(new TestSnapshotAggregate(guid, "Test Snapshot Aggregate v0"), -1);
			var test = repository.GetById(guid);
			test.Id.Should().NotBe(Guid.Empty);

			Action act = () =>
			{
				for (var i = 1; i <= 299; i++)
				{
					var aggregate = repository.GetById(guid);
					aggregate.ChangeName("Test Snapshot Aggregate v" + i);
					repository.Save(aggregate, i - 1);
				}
			};

			act.Should().NotThrow();

			documentEventStore.Find(guid).Should().NotBeNull();
			documentEventStore.Find(guid).Count().Should().Be(300);
			documentSnapshotStore.Find(guid).Should().NotBeNull();
			documentSnapshotStore.Find(guid).Count().Should().Be(2);
		}
	}
}