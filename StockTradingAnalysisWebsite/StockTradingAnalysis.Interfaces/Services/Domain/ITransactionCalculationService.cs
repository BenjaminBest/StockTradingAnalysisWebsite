using System;
using System.Collections.Generic;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.Queries;
using StockTradingAnalysis.Interfaces.Types;

namespace StockTradingAnalysis.Interfaces.Services.Domain
{
    /// <summary>
    /// The interface ITransactionCalculationService defines calulation methods based on transactions purely.
    /// </summary>
    public interface ITransactionCalculationService
    {
        /// <summary>
        /// Calculates the sum of all inpayments for the given transactions.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>Sum of inpayments</returns>
        decimal CalculateSumInpayments(IQuery<IEnumerable<ITransaction>> query);

        /// <summary>
        /// Calculates the sum of capital for the given transactions at the time <paramref name="date" />
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="date">The date.</param>
        /// <returns></returns>
        decimal CalculateSumCapital(IQuery<IEnumerable<ITransaction>> query, DateTime date);

        /// <summary>
        /// Calculates the sum of all dividends for the given transactions (without taxes and order costs).
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>Sum of inpayments</returns>
        decimal CalculateSumDividends(IQuery<IEnumerable<ITransaction>> query);

        /// <summary>
        /// The performance of the current period is calculated as if all shares whould have been sold at the beginning
        /// of this period and then the difference to the value at the end of the period is beeing calculated.
        /// All buys of the year <paramref name="year" /> will be calculated seperatly on a daily basis.
        /// This calculation should only be used for periods starting with the begin of a year till the end of a year (IIR problem)
        /// </summary>
        /// <param name="query">The query should only contain the base query with special filters. Date range filters will automatically applied.</param>
        /// <param name="start">The start.</param>
        /// <param name="end">The end.</param>
        /// <returns></returns>
        decimal CalculatePerformancePercentageForPeriod(IQuery<IEnumerable<ITransaction>> query, DateTime start, DateTime end);

        /// <summary>
        /// Calculates the performance with the internal rate of interest
        /// </summary>
        /// <param name="query">The query.</param>
        /// <param name="beginPeriod">The begin period.</param>
        /// <param name="endPeriod">The end period.</param>
        /// <returns>
        /// Performance in %
        /// </returns>
        decimal CalculatePerformancePercentageIir(IQuery<IEnumerable<ITransaction>> query, CashFlow beginPeriod, CashFlow endPeriod);
    }
}