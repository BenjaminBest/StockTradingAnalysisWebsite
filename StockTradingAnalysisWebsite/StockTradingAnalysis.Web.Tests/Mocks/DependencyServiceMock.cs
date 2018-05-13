﻿using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using StockTradingAnalysis.Interfaces.Services;

namespace StockTradingAnalysis.Web.Tests.Mocks
{
    public class DependencyServiceMock
    {
        public static IDependencyService GetMock(DependencyDescriptor instance)
        {
            var mock = new Mock<IDependencyService>();

            mock.Setup(s => s.GetService(It.IsAny<Type>()))
                .Returns<object>(r => instance.RequestType == (Type)r ? instance.Service : null);

            return mock.Object;
        }

        public static IDependencyService GetMock(List<DependencyDescriptor> instances)
        {
            var mock = new Mock<IDependencyService>();

            mock.Setup(s => s.GetServices(It.IsAny<Type>())).Returns<object>(r =>
            {
                var services = instances.Where(i => i.RequestType == (Type)r);

                return services.Select(d => d.Service).ToList();
            });

            return mock.Object;
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