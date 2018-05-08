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
4. Open Administration in the GUI and generate test data

Technologies & Approaches
--------------
1. [Command Query Responsibility Segregation (CQRS)](https://martinfowler.com/bliki/CQRS.html)
2. [Eventsourcing (ES)](https://martinfowler.com/eaaDev/EventSourcing.html)
3. [RavenDB](https://ravendb.net/)
4. [Bootstrap](https://getbootstrap.com/)
5. [SignalR](https://www.asp.net/signalr)
6. [ReactJs.NET](https://reactjs.net/])
7. [Axios](https://github.com/axios/axios)
8. [Automapper](http://automapper.org/)

Application architecture
--------------
The architecture was designed like most ES systems, but of course simpler than for example a professional product like [Event Store](https://eventstore.org/). Core components for the ES system are the _event store_ which reads and writes the _events_, the _event bus_ which distributes the events to the _event handlers_. The persistence layer is controlled by a DBMS dependent implementation called _persistent event store_. There is one for [RavenDB](https://ravendb.net/) and one for MS SQL.

The system supports _snapshots_ - a projection of the current state of an aggregate - as well. If snapshots for an aggregate are explicitly activated, the event store asks the _snapshot processor_ to take care, when events are persisted. Then the snapshot processor calculates if a snapshot is needed, then the _snapshot store_ persists it.

All reads and writes are separated by using the CQRS architectural pattern. The domain data to be written is transported by _commands_. These commands are sent to a _command dispatcher_ which locates the correct _command handler_ for this command. In the command handler the _aggregate repository_ (in fact every aggregate type has it's own repository) returns the _aggregate_ by loading and applying all events since existence of this aggregate. Then the command can be applied to the aggregate and uncommitted events are written to the event store.

The _process manager coordinator_ is involved when several events or commands need to be orchestrated. The coordinator is observing all events and commands from the eventbus and the command dispatcher. In case of a message it asks the _process manager finder repository_ if an instance of the correct process manager for a message is already running. The identification is done via a correlation id, which is a mapping of some information of a message to a process manager. In case no instance can be found the correct manager is created. Every process manager can publish events or dispatch commands and has a (in memory) state.

Aggregates as well as the MVC controllers, query handlers and event handlers use _domain services_ (may depend on external services) when domain logic is needed.

Every _event_ which is persisted will be published to the event bus. An _event handler_ catches the event and stores an optimized read model in the correct _model repository_. This is done in memory, but it could also be persisted to a database.

On the read side, the _MVC controllers_ for example asking the _query dispatcher_ to return the _domain model_ for a given _query_. The _query handler_ which was implemented to handle this specific query retrieves the model from the model repository and returns it. Then [Automapper](http://automapper.org/) maps the domain model to a view model which was requested by the controller and the data can be pushed towards the frontend.

For Web Sockets the [SignalR](https://www.asp.net/signalr) _hub_ retrieves the data also from the query dispatcher and sends it to the frontend.  

![architecture](https://user-images.githubusercontent.com/29073072/36641270-43c14cd2-1a2d-11e8-8cf6-d13aff801738.png)

Features
--------------
* Dashboard
  * Savings plan based on assigned categories for transactions
  * KPIs
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
* Transactions
  * Buy, sell, dividend, split/reverse split
* Security
  * Add, edit, delete, update quotes
  * Aggregated absolute profit per security
  * Transaction history and latest quote per security
  * Candlestick chat based on security quotes with flags for buy, sell, dividend and split
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
![transactions](https://user-images.githubusercontent.com/29073072/36640980-db2b004a-1a28-11e8-8a62-7b58b1fdf399.png)
Security details
![security_edit](https://user-images.githubusercontent.com/29073072/36342219-6027b808-13fb-11e8-8c34-3f6b8ddf8ffd.png)
Security charts
![security_charts](https://user-images.githubusercontent.com/29073072/36342220-61e068e8-13fb-11e8-830d-ad3fa23c1c44.png)
Dashboard KPIs
![dashboard](https://user-images.githubusercontent.com/29073072/39760693-4f45210a-52d6-11e8-90dc-d923647100f0.png)
