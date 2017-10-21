using StockTradingAnalysis.Interfaces.Enumerations;
using StockTradingAnalysis.Interfaces.Types;
using System.Collections.Generic;

namespace StockTradingAnalysis.Interfaces.Services
{
    /// <summary>
    /// The interface IInterestCalculator defines a service for interest rate calculation
    /// </summary>
    public interface IInterestRateCalculatorService
    {
        /// <summary>
        /// Calculates the interest rate with the newton algorithm p.a. an recognizes outpayments
        /// Inpayments have to be negative, outpayments or the last value of a stock have to be postive
        /// </summary>
        /// <param name="cashFlows">Cashflow</param>
        /// <returns>interest rate</returns>
        AlgorithmResult<ApproximateResultKind, double> Calculate(IEnumerable<CashFlow> cashFlows);
    }
}