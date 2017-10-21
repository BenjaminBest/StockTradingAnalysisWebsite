namespace StockTradingAnalysis.Interfaces.Common
{
    /// <summary>
    /// Defines an interface for module which should be run on application start
    /// </summary>
    public interface IBootModule
    {
        /// <summary>
        /// You can define a greater priority here to have the module booted before others with
        /// a lower priority. 
        /// 0 should be the default priority. You can specify negative priorites (loaded later) or 
        /// higher positive priorities (loaded earlier).
        /// </summary>
        int Priority { get; }

        /// <summary>
        /// Boots up the module
        /// </summary>
        void Boot();
    }
}