namespace StockTradingAnalysis.Interfaces.Commands
{
    /// <summary>
    /// Defines a interface for a command handler
    /// </summary>
    /// <typeparam name="TCommand">The type of the command.</typeparam>
    public interface ICommandHandler<in TCommand> where TCommand : ICommand
    {
        /// <summary>
        /// Executes the specified command.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <returns></returns>
        void Execute(TCommand command);
    }
}