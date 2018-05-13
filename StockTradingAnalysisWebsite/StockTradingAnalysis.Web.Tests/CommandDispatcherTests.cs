﻿using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StockTradingAnalysis.CQRS.CommandDispatcher;
using StockTradingAnalysis.CQRS.Exceptions;
using StockTradingAnalysis.Interfaces.Commands;
using StockTradingAnalysis.Web.Tests.Mocks;
using StockTradingAnalysis.Web.Tests.Objects;

namespace StockTradingAnalysis.Web.Tests
{
    [TestClass]
    public class CommandDispatcherTests
    {
        [TestMethod]
        [Description("Command dispatcher should throw CommandDispatcherException when handler not found")]
        public void CommandDispatcherShouldThrowExceptionIfHandlerNotFound()
        {
            var dispatcher =
                new CommandDispatcher(
                    DependencyServiceMock.GetMock(new DependencyDescriptor(typeof(ICommandHandler<TestCommand>), null)),
                    PerformanceCounterMock.GetMock());

            Action act = () => dispatcher.Execute(new TestCommand());
            act.ShouldThrow<CommandDispatcherException>();
        }
    }
}