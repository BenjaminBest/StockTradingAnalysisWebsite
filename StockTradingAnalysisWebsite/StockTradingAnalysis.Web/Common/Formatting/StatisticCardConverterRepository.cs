using System.Collections.Generic;
using System.Linq;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Web.Common.Interfaces;
using StockTradingAnalysis.Web.Models;

namespace StockTradingAnalysis.Web.Common.Formatting
{
    /// <summary>
    /// The StatisticCardConverterRepository holds all statistic property to card converters.
    /// </summary>
    public class StatisticCardConverterRepository : IStatisticCardConverterRepository
    {
        /// <summary>
        /// The converters
        /// </summary>
        private readonly IEnumerable<IStatisticCardConverter> _converters;

        /// <summary>
        /// Initializes a new instance of the <see cref="StatisticCardConverterRepository"/> class.
        /// </summary>
        /// <param name="converters">The converters.</param>
        public StatisticCardConverterRepository(IEnumerable<IStatisticCardConverter> converters)
        {
            _converters = converters;
        }

        /// <summary>
        /// Converts the statistic to a cards viewmodel.
        /// </summary>
        /// <param name="statistic">The statistic.</param>
        /// <returns>Cards viewmodel</returns>
        public CardsViewModel ConvertStatistic(IStatistic statistic)
        {
            var result = new CardsViewModel();

            foreach (var converter in _converters.OrderBy(c => c.Order))
            {
                result.Cards.Add(converter.Convert(statistic));
            }

            return result;
        }
    }
}