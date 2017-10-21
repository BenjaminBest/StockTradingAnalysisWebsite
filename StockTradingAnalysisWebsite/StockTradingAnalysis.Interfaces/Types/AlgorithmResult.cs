namespace StockTradingAnalysis.Interfaces.Types
{
    /// <summary>
    /// Struct AlgorithmResultdefines a result of an IIR calculation
    /// </summary>
    /// <typeparam name="TKindOfResult">Kind of result</typeparam>
    /// <typeparam name="TValue">Value of the calculation</typeparam>
    public struct AlgorithmResult<TKindOfResult, TValue>
    {
        public readonly TKindOfResult Kind;
        public readonly TValue Value;

        public AlgorithmResult(TKindOfResult kind, TValue value)
        {

            Kind = kind;
            Value = value;
        }
    }
}
