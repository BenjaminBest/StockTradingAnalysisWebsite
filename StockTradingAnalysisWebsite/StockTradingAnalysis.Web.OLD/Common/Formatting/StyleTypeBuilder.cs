using System;
using System.Collections.Generic;
using StockTradingAnalysis.Web.Common.Interfaces;

namespace StockTradingAnalysis.Web.Common.Formatting
{
    /// <summary>
    /// The class StyleTypeRule is used for applying rules on values and based on these rules a style is selected.
    /// </summary>
    public class StyleTypeBuilder<TType> : IStyleTypeBuilder<TType> where TType : IComparable<TType>
    {
        /// <summary>
        /// The value
        /// </summary>
        private readonly TType _value;

        /// <summary>
        /// The default
        /// </summary>
        private StyleType _default = StyleType.Undefined;

        /// <summary>
        /// The rules
        /// </summary>
        private readonly IList<Func<TType, StyleType>> _rules = new List<Func<TType, StyleType>>();

        /// <summary>
        /// Initializes a new instance of the <see cref="StyleTypeBuilder{TType}" /> class.
        /// </summary>
        /// <param name="value">The value which should be used to apply the rules to.</param>
        public StyleTypeBuilder(TType value)
        {
            _value = value;
        }

        /// <summary>
        /// Compiles all rules and runs them on the value
        /// </summary>
        /// <returns>StyleType</returns>
        StyleType IStyleTypeBuilder<TType>.Compile()
        {
            var result = StyleType.Undefined;

            foreach (var rule in _rules)
            {
                var tempResult = rule(_value);

                if (tempResult != StyleType.Undefined)
                    result = tempResult;
            }

            return result == StyleType.Undefined ? _default : result;
        }

        /// <summary>
        /// Whens the value is equal than.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="style">The style.</param>
        /// <returns></returns>
        public IStyleTypeBuilder<TType> WhenEqualThan(TType value, StyleType style)
        {
            _rules.Add(threshold => threshold.CompareTo(value) == 0 ? style : StyleType.Undefined);

            return this;
        }

        /// <summary>
        /// Whens the value is greater than.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="style">The style.</param>
        /// <returns></returns>
        public IStyleTypeBuilder<TType> WhenGreaterThan(TType value, StyleType style)
        {
            _rules.Add(threshold => threshold.CompareTo(value) > 0 ? style : StyleType.Undefined);

            return this;
        }

        /// <summary>
        /// Whens the value is greater or equal than.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="style">The style.</param>
        /// <returns></returns>
        public IStyleTypeBuilder<TType> WhenGreaterOrEqualThan(TType value, StyleType style)
        {
            _rules.Add(threshold => (threshold.CompareTo(value) > 0 || threshold.CompareTo(value) == 0) ? style : StyleType.Undefined);

            return this;
        }

        /// <summary>
        /// Whens the value is smaller than.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="style">The style.</param>
        /// <returns></returns>
        public IStyleTypeBuilder<TType> WhenSmallerThan(TType value, StyleType style)
        {
            _rules.Add(threshold => threshold.CompareTo(value) < 0 ? style : StyleType.Undefined);

            return this;
        }

        /// <summary>
        /// Whens the value is smaller or equal than.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="style">The style.</param>
        /// <returns></returns>
        public IStyleTypeBuilder<TType> WhenSmallerOrEqualThan(TType value, StyleType style)
        {
            _rules.Add(threshold => (threshold.CompareTo(value) < 0 || threshold.CompareTo(value) == 0) ? style : StyleType.Undefined);

            return this;
        }

        /// <summary>
        /// Defines the default style.
        /// </summary>
        /// <param name="style">The style.</param>
        /// <returns></returns>
        public IStyleTypeBuilderThreshold<TType> Default(StyleType style)
        {
            _default = style;

            return this;
        }
    }
}