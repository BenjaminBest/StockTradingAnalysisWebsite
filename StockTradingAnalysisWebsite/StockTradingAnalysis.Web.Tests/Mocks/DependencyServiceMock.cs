using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using StockTradingAnalysis.Core.Common;
using StockTradingAnalysis.Interfaces.DI;

namespace StockTradingAnalysis.Web.Tests.Mocks
{
	public class DependencyServiceMock
	{
		public static void SetMock(DependencyDescriptor instance)
		{
			var mock = new Mock<IServiceProviderWrapper>();

			mock.Setup(s => s.GetService(It.IsAny<Type>()))
				.Returns<object>(r => instance.RequestType == (Type)r ? instance.Service : null);

			DependencyResolver.Init(mock.Object);
		}

		public static void SetMock(List<DependencyDescriptor> instances)
		{
			var mock = new Mock<IServiceProviderWrapper>();

			mock.Setup(s => s.GetServices(It.IsAny<Type>())).Returns<object>(r =>
			{
				var services = instances.Where(i => i.RequestType == (Type)r);

				return services.Select(d => d.Service).ToList();
			});

			DependencyResolver.Init(mock.Object);
		}
	}

	public class DependencyDescriptor
	{
		public Type RequestType { get; set; }
		public object Service { get; set; }

		public DependencyDescriptor(Type requestType, object service)
		{
			RequestType = requestType;
			Service = service;
		}
	}
}