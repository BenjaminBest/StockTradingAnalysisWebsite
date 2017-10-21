using StockTradingAnalysis.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;

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
    }
}
