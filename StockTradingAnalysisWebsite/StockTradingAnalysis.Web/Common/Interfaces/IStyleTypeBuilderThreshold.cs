using StockTradingAnalysis.Web.Common.Formatting;

namespace StockTradingAnalysis.Web.Common.Interfaces
{
    /// <summary>
    /// The interface IStyleTypeBuilderThreshold defines a step in the style type builder where a style rules can be inserted
    /// </summary>
    /// <typeparam name="TType">The type of the type.</typeparam>
    public interface IStyleTypeBuilderThreshold<TType>
    {
        /// <summary>
        /// Whens the value is equal than.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="style">The style.</param>
        /// <returns></returns>
        IStyleTypeBuilder<TType> WhenEqualThan(TType value, StyleType style);

        /// <summary>
        /// Whens the value is greater than.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="style">The style.</param>
        /// <returns></returns>
        IStyleTypeBuilder<TType> WhenGreaterThan(TType value, StyleType style);

        /// <summary>
        /// Whens the value is greater or equal than.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="style">The style.</param>
        /// <returns></returns>
        IStyleTypeBuilder<TType> WhenGreaterOrEqualThan(TType value, StyleType style);

        /// <summary>
        /// Whens the value is smaller than.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="style">The style.</param>
        /// <returns></returns>
        IStyleTypeBuilder<TType> WhenSmallerThan(TType value, StyleType style);

        /// <summary>
        /// Whens the value is smaller or equal than.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="style">The style.</param>
        /// <returns></returns>
        IStyleTypeBuilder<TType> WhenSmallerOrEqualThan(TType value, StyleType style);

        /// <summary>
        /// Defines the default style.
        /// </summary>
        /// <param name="style">The style.</param>
        /// <returns></returns>
        IStyleTypeBuilderThreshold<TType> Default(StyleType style);
    }
}