USE [TransactionManagement]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[EventDataStore]') AND TYPE IN (N'U')) DROP TABLE [dbo].[EventDataStore]

-- Events
CREATE TABLE [dbo].[EventDataStore](
	[AggregateId] [uniqueidentifier] NOT NULL,
	[Version] [int] NOT NULL,
	[Data] [nvarchar](max) NULL,
	[TimeStamp] [DateTime] NOT NULL
) ON [PRIMARY]

GO


CREATE NONCLUSTERED INDEX [IX_AggregateId] ON [dbo].[EventDataStore]
(
	[AggregateId] ASC
)
INCLUDE ([Data]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO

IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[SnapshotDataStore]') AND TYPE IN (N'U')) DROP TABLE [dbo].[SnapshotDataStore]

-- Events
CREATE TABLE [dbo].[SnapshotDataStore](
	[AggregateId] [uniqueidentifier] NOT NULL,
	[Data] [nvarchar](max) NULL
) ON [PRIMARY]

GO

CREATE NONCLUSTERED INDEX [IX_AggregateId] ON [dbo].[SnapshotDataStore]
(
	[AggregateId] ASC
)
INCLUDE ([Data]) WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
GO