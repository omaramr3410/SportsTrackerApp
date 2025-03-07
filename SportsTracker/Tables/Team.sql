CREATE TABLE [dbo].[Team]
(
	[TeamID] INT IDENTITY(1,1) PRIMARY KEY,
	[TeamName] VARCHAR(256) NOT NULL,
	[FoundedYear] VARCHAR(256),
	[CoachName] VARCHAR(256),
	[ManagerID] NVARCHAR(450) NOT NULL,
	[HomeLocationID] INT NOT NULL

	FOREIGN KEY (ManagerID) REFERENCES [AspNetUsers](ID)
	FOREIGN KEY (HomeLocationID) REFERENCES [Location](LocationID)
)
