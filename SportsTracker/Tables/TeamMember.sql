CREATE TABLE dbo.[TeamMember] (
    [TeamMemberId] INT IDENTITY(1,1) PRIMARY KEY,
    [FirstName] VARCHAR(256) NOT NULL,
    [LastName] VARCHAR(256) NOT NULL,
    [DateOfBirth] DATE,
    [Position] VARCHAR(100),
    [TeamID] INT NOT NULL,

    FOREIGN KEY (TeamID) REFERENCES Team(TeamID) ON DELETE CASCADE
);