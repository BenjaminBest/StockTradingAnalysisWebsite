using System.Collections.Generic;

namespace StockTradingAnalysis.Interfaces.Services
{
    /// <summary>
    /// THe interface IMathCalculatorService defines a service to calculate standard math methods
    /// </summary>
    public interface IMathCalculatorService
    {
        /// <summary>
        /// Calculates the geometric mean for the given values
        /// </summary>
        /// <param name="values">Values</param>
        /// <returns>Geometric mean</returns>
        decimal CalculateGeometricMean(IEnumerable<decimal> values);
    }
}