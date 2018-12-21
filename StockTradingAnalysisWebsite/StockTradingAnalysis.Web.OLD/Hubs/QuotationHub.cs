using System.Threading.Tasks;
using System.Web.Mvc;
using Microsoft.AspNet.SignalR;
using StockTradingAnalysis.Web.Common.Interfaces;

namespace StockTradingAnalysis.Web.Hubs
{
    /// <summary>
    /// The QuotationHub is a signalR hub which broadcasts quotation related information to all clients which are listening.
    /// </summary>
    /// <seealso cref="Hub" />
    public class QuotationHub : Hub
    {
        /// <summary>
        /// The quotation service client
        /// </summary>
        private readonly IQuotationServiceClient _quotationServiceClient;

        /// <summary>
        /// Contains the last status of the service
        /// </summary>
        private bool? _lastStatus = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="QuotationHub" /> class.
        /// </summary>
        public QuotationHub()
        {
            _quotationServiceClient = DependencyResolver.Current.GetService<IQuotationServiceClient>();

            BroadcastQuotationServiceStatus();
        }

        public void Hello()
        {
            Clients.All.hello();
        }

        /// <summary>
        /// Broadcasts the quotation service status by using an asynchronous loop.
        /// </summary>
        private void BroadcastQuotationServiceStatus()
        {
            Task.Factory.StartNew(async () =>
               {
                   while (true)
                   {
                       var isOnline = _quotationServiceClient.IsOnline();

                       //Only sent update to clients when something has changed
                       if (!_lastStatus.HasValue || _lastStatus.Value != isOnline)
                       {
                           _lastStatus = isOnline;
                           Clients.All.SendQuotationServiceStatus(isOnline);
                       }

                       await Task.Delay(10000);
                   }
               }, TaskCreationOptions.LongRunning
           );
        }
    }
}