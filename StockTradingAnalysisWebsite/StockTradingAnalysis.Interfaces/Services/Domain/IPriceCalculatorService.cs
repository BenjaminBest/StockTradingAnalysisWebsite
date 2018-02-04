using StockTradingAnalysis.Interfaces.Domain;

namespace StockTradingAnalysis.Interfaces.Services.Domain
{
    /// <summary>
    /// The interface IPriceCalculatorService defines a service to calculate certificates based on the current price and their underlyings
    /// </summary>
    public interface IPriceCalculatorService
    {
        /// <summary>
        /// Calculates the price of a hedged certificate
        /// </summary>
        /// <param name="underlyingPrice">Current price of the underlying</param>
        /// <param name="multiplier">Multiplier</param>
        /// <param name="strikePrice">Strike price</param>
        /// <param name="isLong">Long or short</param>
        /// <returns>Underlying Price</returns>
        decimal CalculatePriceFromUnderlying(decimal? underlyingPrice, decimal? multiplier,
            decimal? strikePrice, bool? isLong);

        /// <summary>
        /// Calculates based on SL,TP, price per unit etc the potential earnings, risk management and P&L
        /// </summary>
        /// <param name="parameters">Base parameters</param>
        /// <returns>List with all quotations between SL and TP</returns>
        ICalculationResult CalculatePotentialEarnings(ICalculation parameters);
    }
}