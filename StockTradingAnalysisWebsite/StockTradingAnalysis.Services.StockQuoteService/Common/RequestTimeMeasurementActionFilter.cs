using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Filters;
using NLog;

namespace StockTradingAnalysis.Services.StockQuoteService.Common
{
    public class RequestTimeMeasurementActionFilter : ActionFilterAttribute
    {
        private string _controllerName = string.Empty;
        private string _actionName = string.Empty;
        private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
        private readonly Stopwatch _intertime = Stopwatch.StartNew();

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            _intertime.Restart();
            _controllerName = filterContext.RouteData.Values["controller"].ToString();
            _actionName = filterContext.RouteData.Values["action"].ToString();
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            _logger.Info($"Request for {_controllerName}.{_actionName} took {_intertime.ElapsedMilliseconds} ms");
        }
    }
}