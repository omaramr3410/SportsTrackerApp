CREATE TABLE [dbo].[Match]
(
	[MatchID] INT IDENTITY(1,1) PRIMARY KEY,
    [MatchDate] DATETIMEOFFSET NOT NULL,
    [LocationID] VARCHAR(256),
    [Team1ID] INT NOT NULL,
    [Team2ID] INT NOT NULL,
    [Team1Score] INT,
    [Team2Score] INT,

    FOREIGN KEY (Team1ID) REFERENCES Team(TeamID),
    FOREIGN KEY (Team2ID) REFERENCES Team(TeamID)
)
