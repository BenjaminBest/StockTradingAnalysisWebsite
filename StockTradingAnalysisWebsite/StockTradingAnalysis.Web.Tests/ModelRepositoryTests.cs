using System;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StockTradingAnalysis.Domain.CQRS.Query.Exceptions;
using StockTradingAnalysis.Web.Tests.Objects;

namespace StockTradingAnalysis.Web.Tests
{
	[TestClass]
	public class ModelRepositoryTests
	{
		[TestMethod]
		[Description("Repository should not throw exception if no item found")]
		public void ReadModelRepositoryShouldNotThrowExceptionIfNotItemFound()
		{
			var repository = new TestModelRepository();
			Action act = () => repository.GetById(Guid.NewGuid());
			act.Should().NotThrow();
		}

		[TestMethod]
		[Description("Repository should return null if no item found")]
		public void ReadModelRepositoryShouldReturnNullIfNotItemFound()
		{
			var repository = new TestModelRepository();
			repository.GetById(Guid.NewGuid()).Should().Be(null);
		}

		[TestMethod]
		[Description("Repository should return an item previously inserted by id")]
		public void ReadModelRepositoryShouldReturnAnItemById()
		{
			var guid = Guid.NewGuid();
			var item = new Test(guid) { Name = "Testitem" };

			var repository = new TestModelRepository();
			repository.Add(item);
			repository.GetById(guid).Should().Be(item);
		}

		[TestMethod]
		[Description("Repository should throw exception if null is added")]
		public void ReadModelRepositoryAddMethodShouldThrowExceptionIfNullIsAdded()
		{
			var repository = new TestModelRepository();
			Action act = () => repository.Add(null);
			act.Should().Throw<ModelRepositoryAddException>();
		}

		[TestMethod]
		[Description("Repository should throw exception if item with empty is added")]
		public void ReadModelRepositoryAddMethodShouldThrowExceptionIfItemWIthEmptyIsAdded()
		{
			var repository = new TestModelRepository();
			Action act = () => repository.Add(new Test(Guid.Empty));
			act.Should().Throw<ModelRepositoryAddException>();
		}

		[TestMethod]
		[Description("Repository should throw exception if duplicate item is added")]
		public void ReadModelRepositoryShouldThrowExceptionIfDuplicateItemIsAdded()
		{
			var testItem = new Test(Guid.NewGuid());
			var repository = new TestModelRepository();
			repository.Add(testItem);

			Action act = () => repository.Add(testItem);
			act.Should().Throw<ModelRepositoryAddException>();
		}

		[TestMethod]
		[Description("Repository should throw exception if null should be deleted")]
		public void ReadModelRepositoryDeleteMethodShouldThrowExceptionIfNullIsAdded()
		{
			var repository = new TestModelRepository();
			Action act = () => repository.Delete(null);
			act.Should().Throw<ModelRepositoryDeleteException>();
		}

		[TestMethod]
		[Description("Repository should throw exception if item with empty should be deleted")]
		public void ReadModelRepositoryDeleteMethodShouldThrowExceptionIfItemWIthEmptyIsAdded()
		{
			var repository = new TestModelRepository();
			Action act = () => repository.Delete(new Test(Guid.Empty));
			act.Should().Throw<ModelRepositoryDeleteException>();
		}

		[TestMethod]
		[Description("Repository should delete an item by id")]
		public void ReadModelRepositoryShouldDeleteAnItem()
		{
			var guid = Guid.NewGuid();
			var item = new Test(guid) { Name = "Testitem" };

			var repository = new TestModelRepository();
			repository.Add(item);
			repository.Delete(item);
			repository.GetById(guid).Should().Be(null);
		}

		[TestMethod]
		[Description("Repository should throw exception if null should be updated")]
		public void ReadModelRepositoryUpdateMethodShouldThrowExceptionIfNullIsAdded()
		{
			var repository = new TestModelRepository();
			Action act = () => repository.Update(null);
			act.Should().Throw<ModelRepositoryUpdateException>();
		}

		[TestMethod]
		[Description("Repository should throw exception if item with empty should be updated")]
		public void ReadModelRepositoryUpdateMethodShouldThrowExceptionIfItemWIthEmptyIsAdded()
		{
			var repository = new TestModelRepository();
			Action act = () => repository.Update(new Test(Guid.Empty));
			act.Should().Throw<ModelRepositoryUpdateException>();
		}

		[TestMethod]
		[Description("Repository should update an item by id")]
		public void ReadModelRepositoryShouldUpdateAnItem()
		{
			var guid = Guid.NewGuid();
			var item = new Test(guid) { Name = "Testitem" };
			var itemUpdate = new Test(guid) { Name = "Testitem Update" };

			var repository = new TestModelRepository();
			repository.Add(item);
			repository.Update(itemUpdate);
			repository.GetById(guid).Name.Should().Be("Testitem Update");
		}

		[TestMethod]
		[Description("Repository should return all items")]
		public void ReadModelRepositoryShouldReturnsAllItems()
		{
			var item1 = new Test(Guid.NewGuid()) { Name = "Testitem 1" };
			var item2 = new Test(Guid.NewGuid()) { Name = "Testitem 2" };

			var repository = new TestModelRepository();
			repository.Add(item1);
			repository.Add(item2);
			repository.GetAll().Count().Should().Be(2);
		}
	}
}