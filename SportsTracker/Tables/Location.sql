CREATE TABLE [dbo].[Location]
(
	[LocationID] INT IDENTITY(1,1) PRIMARY KEY,
    [LocationName] VARCHAR(256) NOT NULL,
    [City] VARCHAR(256),
    [Country] VARCHAR(256)
)
