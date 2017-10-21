using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StockTradingAnalysis.Services.StockQuoteService.Common;
using StockTradingAnalysis.Services.StockQuoteService.Providers;

namespace StockTradingAnalysis.Services.StockQuoteService.Controllers
{
    [Route("quotations")]
    public class StockQuotationController : Controller
    {
        // GET quotations/BASF11
        //[HttpGet("{wkn}")]
        //public IEnumerable<Quotation> Get(string wkn)
        //{
        //    var result= new QuotationDownloadOnVista()
        //        .Initialize(wkn, $"http://www.onvista.de/aktien/kurshistorie.html?WKN={wkn}&RANGE=120M")
        //        .Download()
        //        .ExtractInformation().ToList();

        //    if (result == null || !result.Any())
        //        Response.StatusCode = 404;

        //    return result;
        //}

        // GET quotations/BASF11
        [HttpGet("{wkn}")]
        public IEnumerable<Quotation> Get(string wkn)
        {
            var provider = new QuotationDownloadBoerseDuesseldorf();

            var result = provider
                .Initialize(wkn, $"http://www.boerse-duesseldorf.de/aktien/wkn/{wkn}/historische_kurse")
                .Download()                
                .ExtractInformation().ToList();

            if (result == null || !result.Any())
            {
                Response.StatusCode = provider.Status.HttpResponseCode;                
            }

            return result;
        }


        //TODO: Validate: https://www.wmdaten.de/index.php?mid=70
    }
}