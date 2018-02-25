using System;
using StockTradingAnalysis.Interfaces.Domain;
using StockTradingAnalysis.Interfaces.Services.Domain;
using StockTradingAnalysis.Services.Domain;

namespace StockTradingAnalysis.Services.Services
{
    /// <summary>
    /// PriceCalculatorService is used to calculate certificates based on the current price and their underlyings
    /// </summary>
    public class PriceCalculatorService : IPriceCalculatorService
    {
        private readonly IStockQuoteService _stockQuoteService;

        /// <summary>
        /// Initializes this object
        /// </summary>
        /// <param name="stockQuoteService">The stock quote service</param>
        public PriceCalculatorService(IStockQuoteService stockQuoteService)
        {
            _stockQuoteService = stockQuoteService;
        }

        /// <summary>
        /// Calculates the price of a hedged certificate
        /// </summary>
        /// <param name="underlyingPrice">Current price of the underlying</param>
        /// <param name="multiplier">Multiplier</param>
        /// <param name="strikePrice">Strike price</param>
        /// <param name="isLong">Long or short</param>
        /// <returns>Underlying Price</returns>
        public decimal CalculatePriceFromUnderlying(decimal? underlyingPrice, decimal? multiplier,
            decimal? strikePrice, bool? isLong)
        {
            if (underlyingPrice == 0 || underlyingPrice == default(decimal) || underlyingPrice == null ||
                !isLong.HasValue)
                return 0;

            //Stock
            if (multiplier == 1 || multiplier == default(decimal) || multiplier == null)
                return underlyingPrice.Value;

            //Hedged product
            if (!strikePrice.HasValue || strikePrice == 0 || strikePrice == default(decimal))
                return 0;

            if (multiplier == 0 || multiplier == default(decimal))
                return 0;

            decimal result = 0;

            if (isLong.Value)
                result = (underlyingPrice.Value - strikePrice.Value) * multiplier.Value;
            else
                result = (strikePrice.Value - underlyingPrice.Value) * multiplier.Value;

            //Round or trunk based on the number of digits after the decimal point
            var amountOfDigits = Math.Floor(Math.Log10((double)result) + 1);

            if (amountOfDigits >= 4.0)
            {
                result = decimal.Round(result, 0);
            }
            else if (amountOfDigits == 3.0)
            {
                result = decimal.Round(result, 1);
            }
            else
            {
                result = decimal.Round(result, 2);
            }

            return result;
        }

        /// <summary>
        /// Calculates based on SL,TP, price per unit etc potential earnings, risk management and p&l
        /// </summary>
        /// <param name="parameters">Base parameters</param>
        /// <returns>List with all quotations between SL and TP</returns>
        public ICalculationResult CalculatePotentialEarnings(ICalculation parameters)
        {
            ICalculationResult result = new CalculationResult();

            if (parameters == null)
                return result;

            if (parameters.PricePerUnit == 0 || parameters.InitialSl == 0 || parameters.Units == 0)
                return result;

            if (parameters.PricePerUnit == parameters.InitialSl || parameters.InitialTp == parameters.InitialSl)
                return result;

            //WKN
            result.Wkn = parameters.Wkn;

            //ATR
            result.Atr = _stockQuoteService.GetAtr(parameters.Wkn);

            //CRV
            result.Crv =
                decimal.Round(
                    (parameters.InitialTp - parameters.PricePerUnit - (parameters.OrderCosts / parameters.Units)) /
                    (parameters.PricePerUnit - parameters.InitialSl + (parameters.OrderCosts / parameters.Units)), 2);

            //Posion size
            result.PositionSize = parameters.Units * parameters.PricePerUnit;

            //Is underlying used
            result.IsUnderlyingUsed = parameters.Multiplier != 1;

            //Break Even
            var be = decimal.Round(((2 * parameters.OrderCosts) / parameters.Units) + parameters.PricePerUnit, 2);

            //Current quotation
            var cur = _stockQuoteService.GetQuote(parameters.Wkn);

            //Step size for list
            var maxValue = parameters.InitialTp < cur ? cur : parameters.InitialTp;
            var minValue = parameters.InitialSl > cur ? cur : parameters.InitialSl;
            var stepSize = (maxValue - minValue) * Convert.ToDecimal(0.015);

            if ((maxValue / stepSize) % 2 != 0)
                maxValue = maxValue + stepSize;


            var value = parameters.InitialSl;
            var buyCalc = false;
            var beCalc = false;
            var tpCalc = false;
            var quoteCalc = false;

            //TP+ 10%
            while (value <= maxValue)
            {
                ICalculationQuotation quote = new CalculationQuotation();
                quote.Quotation = value;
                quote.QuotationUnderlying = CalculatePriceFromUnderlying(value, parameters.Multiplier,
                    parameters.StrikePrice, parameters.IsLong);

                quote.PlAbsolute =
                    decimal.Round(
                        (quote.Quotation * parameters.Units) - (2 * parameters.OrderCosts) -
                        (parameters.Units * parameters.PricePerUnit), 2);
                quote.PlPercentage = decimal.Round((quote.PlAbsolute / result.PositionSize) * 100, 2);

                quote.IsStoppLoss = value == parameters.InitialSl;
                quote.IsTakeProfit = value == parameters.InitialTp;
                quote.IsBuy = value == parameters.PricePerUnit;
                quote.IsBreakEven = value == be;

                if (cur != 0)
                    quote.IsCurrentQuotation = value == cur;

                result.Quotation.Add(quote);

                value += stepSize;

                if (!buyCalc && value > parameters.PricePerUnit)
                {
                    value = parameters.PricePerUnit;
                    buyCalc = true;
                }

                if (!beCalc && value > be)
                {
                    value = be;
                    beCalc = true;
                }

                if (cur != 0 && !quoteCalc && value > cur)
                {
                    value = cur;
                    quoteCalc = true;
                }

                if (!tpCalc && value > parameters.InitialTp)
                {
                    value = parameters.InitialTp;
                    tpCalc = true;
                }
            }

            return result;
        }
    }
}