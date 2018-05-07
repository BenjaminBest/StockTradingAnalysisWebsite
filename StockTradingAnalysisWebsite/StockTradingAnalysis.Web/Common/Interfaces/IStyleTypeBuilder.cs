using StockTradingAnalysis.Web.Common.Formatting;

namespace StockTradingAnalysis.Web.Common.Interfaces
{
    /// <summary>
    /// The interface IStyleTypeBuilder defines a step in the style type builder where the rules can be compiles and the result is calculated
    /// </summary>
    /// <typeparam name="TType">The type of the type.</typeparam>
    /// <seealso cref="IStyleTypeBuilderThreshold{TType}" />
    public interface IStyleTypeBuilder<TType> : IStyleTypeBuilderThreshold<TType>
    {
        StyleType Compile();
    }
}