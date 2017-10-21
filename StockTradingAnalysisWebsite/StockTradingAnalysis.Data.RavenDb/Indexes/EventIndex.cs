using Raven.Client.Indexes;
using StockTradingAnalysis.Interfaces.Events;
using System.Linq;

namespace StockTradingAnalysis.Data.RavenDb.Indexes
{
    public class EventIndex : AbstractMultiMapIndexCreationTask
    {
        public EventIndex()
        {
            AddMap<IDomainEvent>(
                events => from @event in events
                          select new
                          {
                              @event.AggregateId
                          });
        }
    }
}