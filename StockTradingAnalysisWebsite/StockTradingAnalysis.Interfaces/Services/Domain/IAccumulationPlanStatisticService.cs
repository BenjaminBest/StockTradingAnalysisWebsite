using StockTradingAnalysis.Interfaces.Domain;

namespace StockTradingAnalysis.Interfaces.Services.Domain
{
    /// <summary>
    /// The interface IAccumulationPlanStatisticService defines a service to calculate time range based statistical information for saving plans
    /// </summary>
    public interface IAccumulationPlanStatisticService
    {
        /// <summary>
        /// Calculates the savings plan for the given <paramref name="tag" />
        /// </summary>
        /// <param name="tag">The tag.</param>
        /// <returns>Savingsplan</returns>
        ISavingsPlan CalculateSavingsPlan(string tag);
    }
}