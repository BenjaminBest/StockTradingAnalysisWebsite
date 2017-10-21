namespace StockTradingAnalysis.Interfaces.Queries
{
    /// <summary>
    /// Defines a interface for a query handler
    /// </summary>
    /// <typeparam name="TQuery">The type of the query.</typeparam>
    /// <typeparam name="TResult">The type of the result.</typeparam>
    public interface IQueryHandler<in TQuery, out TResult> where TQuery : IQuery<TResult>
    {
        /// <summary>
        /// Executes the specified query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns>The result.</returns>
        TResult Execute(TQuery query);
    }
}