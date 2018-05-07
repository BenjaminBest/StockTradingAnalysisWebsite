using System.Collections.Generic;

namespace StockTradingAnalysis.Interfaces.Services.Core
{
    /// <summary>
    /// THe interface IMathCalculatorService defines a service to calculate standard math methods
    /// </summary>
    public interface IMathCalculatorService
    {
        /// <summary>
        /// Calculates the geometric mean for the given values.
        /// </summary>
        /// <param name="values">Values</param>
        /// <returns>Geometric mean</returns>
        decimal CalculateGeometricMean(IEnumerable<decimal> values);


        /// <summary>
        /// Calculates the square root of the given <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Sqrt</returns>
        double CalculateSquareRoot(double value);

        /// <summary>
        /// Calculates the standard deviation
        /// </summary>
        /// <param name="values">Values</param>
        /// <param name="decimalPlaces">Decimal places</param>
        /// <returns></returns>
        double CalculateStandardDeviation(IReadOnlyCollection<double> values, int decimalPlaces);
    }
}