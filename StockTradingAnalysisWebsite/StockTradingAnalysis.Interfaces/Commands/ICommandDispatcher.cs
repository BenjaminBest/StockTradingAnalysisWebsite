namespace StockTradingAnalysis.Interfaces.Commands
{
    /// <summary>
    /// Defines a base interface for a dispatcher that delegates the given command to 
    /// the corresponding <see cref="ICommandHandler{TCommand}"/> implementation
    /// </summary>
    public interface ICommandDispatcher
    {
        /// <summary>
        /// Delegates the specified command to a <see cref="ICommandHandler{TCommand}" /> implementation.
        /// </summary>
        /// <param name="command">The command.</param>
        void Execute(ICommand command);
    }
}
