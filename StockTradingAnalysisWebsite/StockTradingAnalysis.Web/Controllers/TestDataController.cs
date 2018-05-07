using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using StockTradingAnalysis.Domain.CQRS.Cmd.Commands;
using StockTradingAnalysis.Interfaces.Commands;
using StockTradingAnalysis.Interfaces.Queries;
using StockTradingAnalysis.Interfaces.ReadModel;
using StockTradingAnalysis.Web.Common.Interfaces;

namespace StockTradingAnalysis.Web.Controllers
{
    public class TestDataController : Controller
    {
        /// <summary>
        /// The command dispatcher
        /// </summary>
        private readonly ICommandDispatcher _commandDispatcher;

        /// <summary>
        /// The query dispatcher
        /// </summary>
        private readonly IQueryDispatcher _queryDispatcher;

        /// <summary>
        /// The quote service client
        /// </summary>
        private readonly IQuotationServiceClient _quoteServiceClient;

        /// <summary>
        /// The data deletion coordinator
        /// </summary>
        private readonly IModelRepositoryDeletionCoordinator _dataDeletionCoordinator;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestDataController" /> class.
        /// </summary>
        /// <param name="commandDispatcher">The command dispatcher.</param>
        /// <param name="queryDispatcher">The query dispatcher.</param>
        /// <param name="quoteServiceClient">The quote service client.</param>
        /// <param name="dataDeletionCoordinator">The data deletion coordinator.</param>
        public TestDataController(
            ICommandDispatcher commandDispatcher,
            IQueryDispatcher queryDispatcher,
            IQuotationServiceClient quoteServiceClient,
            IModelRepositoryDeletionCoordinator dataDeletionCoordinator)
        {
            _commandDispatcher = commandDispatcher;
            _queryDispatcher = queryDispatcher;
            _quoteServiceClient = quoteServiceClient;
            _dataDeletionCoordinator = dataDeletionCoordinator;
        }


        // GET: Administration/GenerateTestData
        public ActionResult GenerateTestData()
        {
            return
                View();
        }

        // GET: Administration/GenerateTestData/id
        public JsonResult Generate(int id)
        {
            var result = string.Empty;

            //Delete stores
            if (id == 0)
            {
                _dataDeletionCoordinator.DeleteAll();

                _commandDispatcher.Execute(new FeedbackAddCommand(Guid.Parse("FD81F0FC-353F-4308-A4B4-810B8716E8DB"), -1, "Zu später Einstieg", "Der Kauf einer Aktie hätte früher beginnen sollen, da so ein Großteil der Bewegung verpasst wurde."));
                _commandDispatcher.Execute(new FeedbackAddCommand(Guid.Parse("C7F3E2F9-D292-402F-922C-027DA9BC2F68"), -1, "Stop Loss nicht nachgezogen", "Der Stopp Loss wurde nicht bei Gewinnen nachgezogen um so den Break Even abzusichern."));
                _commandDispatcher.Execute(new FeedbackAddCommand(Guid.Parse("A568BC1C-29C0-44C0-9EDD-4C0CFD09290D"), -1, "Gegen den Trend gehandelt", "Es wurde gegen den Intraday- oder Wochen-Trend gehandelt. Damit verringert sich automatisch die Chance auf Erfolg."));
                _commandDispatcher.Execute(new FeedbackAddCommand(Guid.Parse("B3ACE2B0-B820-4E1A-86C6-EF3D1991CF9A"), -1, "Trend nicht bestätigt/nicht erkannt", "Der Trend war zum Kaufzeitpunkt noch nicht bestätigt. Ein Kauf hätte (noch) nicht stattfinden sollen, da Sicherheit vorrang hat."));
                _commandDispatcher.Execute(new FeedbackAddCommand(Guid.Parse("3C5B176A-7085-480D-AA4C-6FB25CEC0C32"), -1, "In Widerstand/Unterstützung hineingekauft", "Es wurde kurz vor einem Widerstand gekauft."));
                _commandDispatcher.Execute(new FeedbackAddCommand(Guid.Parse("A9F112DB-D3CB-4254-B232-916C91FC4812"), -1, "Kein Stopp Loss gesetzt", "Es wurde initial oder über die gesamte Haltezeit kein Stopp Loss gesetzt. Dies kann zu massiven Verlusten führen, die nur sehr schwer wieder auszugleichen sind."));
                _commandDispatcher.Execute(new FeedbackAddCommand(Guid.Parse("BEC6DE2C-2E6A-4B84-8A11-E8726A5C5EC9"), -1, "Zu früher Einstieg", "Es wurde zu früh gekauft, evtl. sogar übereilt nur um den Trade einzugehen. Späterer Kauf hätte Gewinn maximiert oder Verlust durch niedrigere SL Grenze verhindert."));
                _commandDispatcher.Execute(new FeedbackAddCommand(Guid.Parse("47D36CC2-7A40-43BD-B8CB-940BA82E851B"), -1, "Plan wurde nachträglich  geändert", "Es heißt 'Plan your trade und trade your plan'. Der Plan sollte nicht mehr verändert werden, da bei Live-Kurse evtl. keine rationalen Entscheidungen getroffen werden können."));

                _commandDispatcher.Execute(new StrategyAddCommand(Guid.Parse("AD8B8EAF-DE34-489F-BB92-03B41039DA43"), -1, "Keine Strategie", "Der Einstieg fand ohne strategischen Hintergrund statt, oder es wurde hierzu keine Angabe gemacht.", null));
                _commandDispatcher.Execute(new StrategyAddCommand(Guid.Parse("1716E68A-3953-49C1-A7E6-0165CAE75FD5"), -1, "Breakout Trading Range", "Strategie\r\n-----------\r\nEs wird der Ausbruch aus einer Konsolidierungsbewegung in Trendrichtung gehandelt. Der Einstieg erfolgt überhalb der Schiebezone über dem höchsten Widerstand bzw. unterhalb der niedrigsten Unterstützung.\r\n\r\nUm False Breakouts zu vermeiden muss ein Abstand zum Widerstand bzw. zur Unterstützung verwendet werden.\r\n\r\nDie Strategie ist prozyklisch.\r\n\r\nSL\r\n-----------\r\nDer SL muss knapp unterhalb des Ausbruchs gesetzt werden, aber mit entsprechendem Abstand, da die Kurse evtl. wieder zu Ausgangspunkt zurückkehren.\r\n\r\nTP\r\n-----------\r\nIdealerweise sollte der TP dort liegen, wo die Kurse nach dem Ausbruch erneut konsolidieren.\r\n", null));
                _commandDispatcher.Execute(new StrategyAddCommand(Guid.Parse("67380559-9393-4628-AC3F-3807F61D83B9"), -1, "Rebound an Support/Osz. Daily", "Strategie\r\n-----------\r\n- Long / Short\r\n- Primärer Aufwärtstrend / Abwärtstrend\r\n- CCI(20)= Signallinie unterhalb von -100 / oberhalb von +100\r\n- MCAD(12,26,9)= Signallinie ist kurz vor kreuzen von unten nach oben / oben nach unten\r\n- RSI(14)= RSI ist max. 30 / min. 70\r\n- Bollinger Bänder(20,2)= Kurse berühren unteres Band / oberes Band\r\n\r\nDie Strategie ist antizyklisch und die Betrachtung erfolgt im Tageschart.\r\n\r\nSL\r\n----------\r\n- Der Sl sollte unterhalb/oberhalb der Unterstützung/des Widerstands liegen, bei dem gekauft/verkauft wurde.", null));
                _commandDispatcher.Execute(new StrategyAddCommand(Guid.Parse("B11D0E35-2B3D-4049-BA6F-061EC54DA63E"), -1, "Penny Stocks", "Kaufen von Penny Stocks", null));
                _commandDispatcher.Execute(new StrategyAddCommand(Guid.Parse("CC715911-FD9E-428B-8C1C-E49A08D2619F"), -1, "Kaufen und Halten", "Aktien werden gekauft und ohne Stopp Loss so lange gehalten, bis diese mit Gewinn verkauft werden können.\r\n\r\n- Kaufen und Halten\r\n- Nachkaufen bei größeren Verlusten (~10%)\r\n- Anlagehorizont: 1-2J", null));
                _commandDispatcher.Execute(new StrategyAddCommand(Guid.Parse("2B821592-E6A3-4C91-984A-1A27FBE06F88"), -1, "Ausbruch aus Dreieck", "Es werden steigende, fallende und symmetrische Dreiecke während des Ausbruchs gehandelt. Der Stopp Loss liegt innerhalb des Dreiecks und der TP wird durch die Breite des Dreiecks bestimmt.\r\n\r\nDie Strategie wird prozyklisch gehandelt.", null));
                _commandDispatcher.Execute(new StrategyAddCommand(Guid.Parse("7DB975E4-60EC-44A6-86F2-3CD808F6BC43"), -1, "Channel Trading Range", "Es wid am oberen Ende der Schiebezone (Widerstand) verkauft und am unteren Ende (Unterstützung) gekauft.\r\n\r\nDer SL darf nicht zu eng angelegt sein, auch werden die Kurse irgendwann die Schiebezone verlassen, der CRV ist dennoch sehr hoch.\r\n\r\nDie Strategie wird antizyklisch gehandelt.", null));
                _commandDispatcher.Execute(new StrategyAddCommand(Guid.Parse("EF545D8E-3FF1-42A4-9FD5-F6B3069CCE6F"), -1, "Altersvorsorge", "Haltedauer: ca. 30 Jahre bis zur Rente. Wiederanlage der Zinzen ab einer nennenswerten Summe", null));

                result = Resources.ViewTextTestDataControllerErase;
            }
            //Hochtief
            else if (id == 1)
            {
                var stockId = Guid.Parse("7108B1AC-0C3E-4324-A74A-B0E01C64645A");
                _commandDispatcher.Execute(new StockAddCommand(stockId, -1, "Hochtief", "607000", "Aktie", "Long"));
                DownloadQuotes(stockId);

                _commandDispatcher.Execute(new TransactionBuyCommand(Guid.NewGuid(), -1,
                    DateTime.Parse("2011-06-13 13:47"), 13, 60.58m, 0, string.Empty, string.Empty, null, 0, 0, stockId,
                    Guid.Parse("CC715911-FD9E-428B-8C1C-E49A08D2619F")));

                _commandDispatcher.Execute(new TransactionBuyCommand(Guid.NewGuid(), -1,
                    DateTime.Parse("2012-03-06 00:00"), 17, 53.74m, 0, string.Empty, string.Empty, null, 0, 0, stockId,
                    Guid.Parse("CC715911-FD9E-428B-8C1C-E49A08D2619F")));

                _commandDispatcher.Execute(new TransactionDividendCommand(Guid.NewGuid(), -1,
                    DateTime.Parse("2013-05-08 00:00"), 30, 1.00m, 0, string.Empty, string.Empty, null, stockId, 0));

                _commandDispatcher.Execute(new TransactionSellCommand(Guid.NewGuid(), -1,
                     DateTime.Parse("2013-10-01 08:02"), 30, 64.60m, 15.40m, string.Empty, string.Empty, null, stockId, 0, 34.67m, 69.70m, new List<Guid>()));

                result = string.Format(Resources.ViewTextTestDataControllerSecurityCreated, "Hochtief");
            }
            //Deutsche Bank
            else if (id == 2)
            {
                var stockId = Guid.Parse("619431DC-B852-4691-858B-CF529D9A134F");
                _commandDispatcher.Execute(new StockAddCommand(stockId, -1, "Deutsche Bank AG", "514000", "Aktie", "Long"));
                DownloadQuotes(stockId);

                _commandDispatcher.Execute(new TransactionBuyCommand(Guid.NewGuid(), -1,
                    DateTime.Parse("2011-05-03 11:49"), 22, 43.68m, 0, string.Empty, string.Empty, null, 0, 0, stockId,
                    Guid.Parse("CC715911-FD9E-428B-8C1C-E49A08D2619F")));

                _commandDispatcher.Execute(new TransactionDividendCommand(Guid.NewGuid(), -1,
                    DateTime.Parse("2011-05-27 00:00"), 22, 0.75m, 0, string.Empty, string.Empty, null, stockId, 0));

                _commandDispatcher.Execute(new TransactionDividendCommand(Guid.NewGuid(), -1,
                    DateTime.Parse("2012-06-01 00:00"), 22, 0.75m, 0, string.Empty, string.Empty, null, stockId, 0));

                _commandDispatcher.Execute(new TransactionDividendCommand(Guid.NewGuid(), -1,
                    DateTime.Parse("2013-05-24 00:00"), 22, 0.75m, 0, string.Empty, string.Empty, null, stockId, 0));

                _commandDispatcher.Execute(new TransactionDividendCommand(Guid.NewGuid(), -1,
                    DateTime.Parse("2014-05-23 00:00"), 22, 0.75m, 0, string.Empty, string.Empty, null, stockId, 0));

                _commandDispatcher.Execute(new TransactionDividendCommand(Guid.NewGuid(), -1,
                    DateTime.Parse("2014-06-24 00:00"), 22, 1.37m, 0,
                    "Dies ist eigentlich der Verkauf der Bezugsrechte der Deutschen Bank. Der Einfachheit halber wird dies als Dividende deklariert.",
                    string.Empty, null, stockId, 0));

                _commandDispatcher.Execute(new TransactionDividendCommand(Guid.NewGuid(), -1,
                    DateTime.Parse("2015-05-22 00:00"), 22, 0.75m, 0, string.Empty, string.Empty, null, stockId, 0));

                _commandDispatcher.Execute(new TransactionDividendCommand(Guid.NewGuid(), -1,
                    DateTime.Parse("2017-04-04 00:00"), 22, 1.95m, 0,
                    "Verk. Teil-/Bezugsr der Inhaber-Bezugsrechte. Nicht ausgeübten Bezugsrechte wurden bestens verkauft.",
                    string.Empty, null, stockId, 0));

                _commandDispatcher.Execute(new TransactionDividendCommand(Guid.NewGuid(), -1,
                    DateTime.Parse("2017-05-23 00:00"), 22, 0.19m, 0, string.Empty, string.Empty, null, stockId, 0));

                result = string.Format(Resources.ViewTextTestDataControllerSecurityCreated, "Deutsche Bank");

            }
            //Thyssen Krupp
            else if (id == 3)
            {
                var stockId = Guid.Parse("24A30C49-2293-4A46-83D9-719F67078094");
                _commandDispatcher.Execute(new StockAddCommand(stockId, -1, "Thyssen Krupp", "750000", "Aktie", "Long"));
                DownloadQuotes(stockId);

                _commandDispatcher.Execute(new TransactionBuyCommand(Guid.NewGuid(), -1,
                    DateTime.Parse("2011-06-09 00:00"), 40, 34.50m, 9.90m, string.Empty, string.Empty, null, 0, 0, stockId,
                    Guid.Parse("CC715911-FD9E-428B-8C1C-E49A08D2619F")));

                _commandDispatcher.Execute(new TransactionDividendCommand(Guid.NewGuid(), -1,
                    DateTime.Parse("2015-02-02 00:00"), 40, 0.11m, 0, string.Empty, string.Empty, null, stockId, 0));

                _commandDispatcher.Execute(new TransactionDividendCommand(Guid.NewGuid(), -1,
                    DateTime.Parse("2016-02-01 00:00"), 40, 0.15m, 0, string.Empty, string.Empty, null, stockId, 0));

                _commandDispatcher.Execute(new TransactionDividendCommand(Guid.NewGuid(), -1,
                    DateTime.Parse("2017-02-01 00:00"), 40, 0.15m, 0, string.Empty, string.Empty, null, stockId, 0));

                _commandDispatcher.Execute(new CalculationAddCommand(Guid.NewGuid(), -1, "Thyssen Krupp", "750000", 1, 0,
                    "Thyssen Krupp", 15.00m, 38.00m, 20.00m, 9.90m, "Kaufen und Halten", 100, true));

                result = string.Format(Resources.ViewTextTestDataControllerSecurityCreated, "Thyssen Krupp");
            }
            //Commerzbank
            else if (id == 4)
            {
                var stockId = Guid.Parse("3E376710-73F6-4DB5-A567-758EF930D682");
                _commandDispatcher.Execute(new StockAddCommand(stockId, -1, "Commerzbank AG", "CBK100", "Aktie", "Long"));
                DownloadQuotes(stockId);

                _commandDispatcher.Execute(new TransactionBuyCommand(Guid.NewGuid(), -1,
                    DateTime.Parse("2013-02-13 19:38"), 650, 1.42m, 9.90m, string.Empty, string.Empty, null, 1.35m, 1.58m, stockId,
                    Guid.Parse("7DB975E4-60EC-44A6-86F2-3CD808F6BC43")));

                _commandDispatcher.Execute(new TransactionSellCommand(Guid.NewGuid(), -1,
                    DateTime.Parse("2013-03-12 19:19"), 650, 1.39m, 11.15m, string.Empty, string.Empty, null, stockId,
                    10.71m, 1.10m, 3.58m,
                    new List<Guid>()
                    {
                        Guid.Parse("B3ACE2B0-B820-4E1A-86C6-EF3D1991CF9A"),
                        Guid.Parse("FD81F0FC-353F-4308-A4B4-810B8716E8DB")
                    }));

                _commandDispatcher.Execute(new TransactionBuyCommand(Guid.NewGuid(), -1,
                    DateTime.Parse("2013-03-18 09:03"), 700, 1.19m, 11.15m, string.Empty, string.Empty, null, 1.14m, 1.37m, stockId,
                    Guid.Parse("7DB975E4-60EC-44A6-86F2-3CD808F6BC43")));

                _commandDispatcher.Execute(new TransactionSellCommand(Guid.NewGuid(), -1,
                     DateTime.Parse("2013-03-25 16:22"), 700, 1.14m, 11.15m, string.Empty, string.Empty, null, stockId,
                     10.71m, 1.00m, 2.58m,
                     new List<Guid>()
                     {
                        Guid.Parse("BEC6DE2C-2E6A-4B84-8A11-E8726A5C5EC9"),
                        Guid.Parse("FD81F0FC-353F-4308-A4B4-810B8716E8DB")
                     }));

                result = string.Format(Resources.ViewTextTestDataControllerSecurityCreated, "Commerzbank");
            }
            //S&P 500
            else if (id == 5)
            {
                var stockId = Guid.Parse("2EF99FAD-7891-4021-8C77-4326C7F284EE");
                _commandDispatcher.Execute(new StockAddCommand(stockId, -1, "iShares PLC-S&P 500 Index Fd", "622391", "ETF", "Long"));
                DownloadQuotes(stockId);

                _commandDispatcher.Execute(new TransactionBuyCommand(Guid.NewGuid(), -1,
                    DateTime.Parse("2013-06-03 09:06"), 23.62m, 12.48m, 5.16m, string.Empty, "Altersvorsorge", null, 0,
                    0, stockId, Guid.Parse("EF545D8E-3FF1-42A4-9FD5-F6B3069CCE6F")));

                _commandDispatcher.Execute(new TransactionBuyCommand(Guid.NewGuid(), -1,
                    DateTime.Parse("2013-09-02 09:04"), 23.79m, 12.39m, 5.16m, string.Empty, "Altersvorsorge", null, 0,
                    0, stockId, Guid.Parse("EF545D8E-3FF1-42A4-9FD5-F6B3069CCE6F")));

                _commandDispatcher.Execute(new TransactionDividendCommand(Guid.NewGuid(), -1,
                    DateTime.Parse("2013-09-18 00:00"), 23.62m, 0.04m, 0, string.Empty, "Altersvorsorge", null, stockId, 0));

                _commandDispatcher.Execute(new TransactionBuyCommand(Guid.NewGuid(), -1,
                    DateTime.Parse("2013-12-02 09:04"), 22.27m, 13.24m, 5.16m, string.Empty, "Altersvorsorge", null, 0,
                    0, stockId, Guid.Parse("EF545D8E-3FF1-42A4-9FD5-F6B3069CCE6F")));

                _commandDispatcher.Execute(new TransactionDividendCommand(Guid.NewGuid(), -1,
                    DateTime.Parse("2013-12-21 00:00"), 47.41m, 0.04m, 0, string.Empty, "Altersvorsorge", null, stockId, 0));

                _commandDispatcher.Execute(new TransactionBuyCommand(Guid.NewGuid(), -1,
                    DateTime.Parse("2014-03-03 09:06"), 22.12m, 13.33m, 5.16m, string.Empty, "Altersvorsorge", null, 0,
                    0, stockId, Guid.Parse("EF545D8E-3FF1-42A4-9FD5-F6B3069CCE6F")));

                _commandDispatcher.Execute(new TransactionDividendCommand(Guid.NewGuid(), -1,
                    DateTime.Parse("2014-03-19 00:00"), 69.69m, 0.04m, 0, string.Empty, "Altersvorsorge", null, stockId, 0));

                result = string.Format(Resources.ViewTextTestDataControllerSecurityCreated, "iShares PLC-S&P 500 Index Fd");
            }
            //ProSiebenSat1 Media
            else if (id == 6)
            {
                var stockId = Guid.Parse("B3B26984-A5FC-4EE4-856F-0E58E43C8E37");
                _commandDispatcher.Execute(new StockAddCommand(stockId, -1, "ProSiebenSat1 Media", "PSM777", "Aktie", "Long"));
                DownloadQuotes(stockId);

                _commandDispatcher.Execute(new TransactionBuyCommand(Guid.NewGuid(), -1,
                    DateTime.Parse("2011-06-08 20:36"), 70, 19.05m, 9.90m, string.Empty, string.Empty, null, 0, 0, stockId,
                    Guid.Parse("67380559-9393-4628-AC3F-3807F61D83B9")));

                _commandDispatcher.Execute(new TransactionDividendCommand(Guid.NewGuid(), -1,
                    DateTime.Parse("2011-07-04 00:00"), 70, 1.14m, 0, string.Empty, String.Empty, null, stockId, 19.95m));

                _commandDispatcher.Execute(new TransactionDividendCommand(Guid.NewGuid(), -1,
                    DateTime.Parse("2012-05-16 00:00"), 70, 1.17m, 0, string.Empty, String.Empty, null, stockId, 0));

                _commandDispatcher.Execute(new TransactionSellCommand(Guid.NewGuid(), -1,
                    DateTime.Parse("2013-02-06 14:41"), 70, 25.02m, 11.15m, string.Empty, string.Empty, null, stockId,
                    0, 10m, 35.92m,
                    new List<Guid> { Guid.Parse("47D36CC2-7A40-43BD-B8CB-940BA82E851B") }));

                result = string.Format(Resources.ViewTextTestDataControllerSecurityCreated, "ProSiebenSat1 Media");
            }

            return Json(result, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Downloads the quotes.
        /// </summary>
        /// <param name="stockId">The stock identifier.</param>
        private void DownloadQuotes(Guid stockId)
        {
            var quotes = _quoteServiceClient.Get(stockId).ToList();

            if (quotes.Any())
            {
                var cmd = new StockQuotationsAddOrChangeCommand(stockId, 0, quotes);
                _commandDispatcher.Execute(cmd);
            }
        }
    }
}