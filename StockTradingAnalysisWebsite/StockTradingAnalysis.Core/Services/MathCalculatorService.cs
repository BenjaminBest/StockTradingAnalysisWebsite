using System;
using System.Collections.Generic;
using System.Linq;
using StockTradingAnalysis.Interfaces.Services.Core;

namespace StockTradingAnalysis.Core.Services
{
    /// <summary>
    /// The class MathCalculatorService defines a service to calculate standard math methods
    /// </summary>
    public class MathCalculatorService : IMathCalculatorService
    {
        /// <summary>
        /// Calculates the geometric mean for the given values
        /// </summary>
        /// <param name="values">Values</param>
        /// <returns>Geometric mean</returns>
        public decimal CalculateGeometricMean(IEnumerable<decimal> values)
        {
            if (values == null)
                return 0.0m;

            var list = values as IList<decimal> ?? values.ToList();

            if (!list.Any())
                return 0.0m;

            var mul = list.Aggregate<decimal, double>(1, (current, val) => current * Convert.ToDouble(val));
            var mean = Math.Pow(mul, 1 / Convert.ToDouble(list.Count));

            return decimal.Round(Convert.ToDecimal(mean), 2);
        }

        /// <summary>
        /// Calculates the square root of the given <paramref name="value"/>.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <returns>Sqrt</returns>
        public double CalculateSquareRoot(double value)
        {
            return Math.Sqrt(value);
        }

        /// <summary>
        /// Calculates the standard deviation
        /// </summary>
        /// <param name="values">Values</param>
        /// <param name="decimalPlaces">Decimal places</param>
        /// <returns></returns>
        public double CalculateStandardDeviation(IReadOnlyCollection<double> values, int decimalPlaces)
        {
            if (values.Count < 2)
                return 0.0;

            var sumSqrt = 0.0;
            var midValue = values.Average();

            foreach (var value in values)
                sumSqrt += Math.Pow(value - midValue, 2);

            return Math.Round(CalculateSquareRoot(sumSqrt / (values.Count - 1)), decimalPlaces);
        }
    }
}
