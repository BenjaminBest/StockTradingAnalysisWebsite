Stock Trading Analysis
=====================================
This application can be used to manage stock transactions and maintaining a diary by adding stocks, transactions for these stocks, feedback and strategies. Based on all transactions key performance indicators etc. are calculated for different time periods. Also stock quotes can be downloaded automatically.


History
--------------
This application already exists in an old version which was developed by the motto "getting things done" and by utilizing a relational database. I've decided to re-develop the whole application with techniques which are more sophisticated like [CQRS](https://martinfowler.com/bliki/CQRS.html) and [Eventsourcing](https://martinfowler.com/eaaDev/EventSourcing.html).

The project `StockTradingAnalysis.Web.Migration` is used to load the data from the relational database of my legacy application to the object oriented database by firing commands and thus using the event sourcing system (can be ignored by you).

Setup project with MSSQL ([RavenDB](https://ravendb.net/) exists as well)
--------------
1. Create a new database
2. Execute the script `StockTradingAnalysis.Data.MSSQL.Scripts.Create_DataStore_Table.sql`
3. Run multiple projects (`StockTradingAnalysis.Web` and `StockTradingAnalysis.Services.StockQuoteService`). Can be done by right-click on the solution in Visual Studio and go to "Set Startup Projects".
4. Open Administration-> Generate test data

Technologies & Approaches
--------------
1. [CQRS](https://martinfowler.com/bliki/CQRS.html)
2. [Eventsourcing](https://martinfowler.com/eaaDev/EventSourcing.html)
3. [RavenDB](https://ravendb.net/)
4. [Bootstrap](https://getbootstrap.com/)
5. [SignalR](https://www.asp.net/signalr)
6. [ReactJs.NET](https://reactjs.net/])
7. [Axios](https://github.com/axios/axios)

Application infrastructure
--------------
Diagram coming soon

Features
--------------
* Dashboard
  * Savings plan based on assigned categories for transactions
* Transactions
  * Buy, sell, dividend, split/reverse split
* Security
  * Add, edit, delete, update quotes
  * Aggregated absolute profit per security
  * Transaction history and latest quote per security
* Feedback
  * Add, edit, delete
  * Percentage of transactions assigned to feedbacks
* Strategies
  * Add, edit, delete
* Calculation for buying decisions and open positions
  * Stop loss, take profit, price, amount etc. can be configured
  * Historical/daily basis quotes can be downloaded (if stock with WKN was configured)
* Administration
  * Test data generation

Features (not yet migrated)
--------------
* Dashboard
  * KPI
    * Amount of trades
    * Amount of winning/losing trades
    * Amount of trades per year/month/week
    * System quality number (SQN)
    * Pay-Off-Ratio
    * CRV
    * Expected values
    * Average win/loss
    * Maximum win/loss
    * Average buying volume
    * Order costs/taxes
    * Average holding period intraday/long term positions
    * Best asset class
    * Best assert
    * Maximum drawdown
    * Maximum consecutive winners/losers
    * Exit/Entry efficiency
    * Open positions
  * Statistics: Last 10/25/50/75/100/150/All Trades (profit, loss, payoff-ratio, CRV,CQN etc.)
  * Performance: over asset class, long/short, strategy, monthly performance
  * Potential: MAE,MFE
  * G/V: Expected value, trade stability, G/V size distribution, depot P&L, hit rate
  * Risk: cluster analysis, monte carlo simulation
  * Savings plan: products, export
* Performance
  * All performance key indicators with filtering over time range & asset class
* Transactions  
  * Export

Images
--------------
Calculations
![calculation](https://user-images.githubusercontent.com/29073072/35777955-055ff46a-09b7-11e8-9ec1-3704a4aca895.png)
Transactions
![transactions](https://user-images.githubusercontent.com/29073072/36072645-5a183582-0f23-11e8-94f2-f69e4c981812.png)
Security details
![security_edit](https://user-images.githubusercontent.com/29073072/36073653-736b7f92-0f34-11e8-8ac8-23b9ab53a9d1.png)
