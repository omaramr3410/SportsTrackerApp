﻿/*
Deployment script for SportsTracker

This code was generated by a tool.
Changes to this file may cause incorrect behavior and will be lost if
the code is regenerated.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "SportsTracker"
:setvar DefaultFilePrefix "SportsTracker"
:setvar DefaultDataPath "C:\Users\sonic\AppData\Local\Microsoft\VisualStudio\SSDT\SportsTrackerApp"
:setvar DefaultLogPath "C:\Users\sonic\AppData\Local\Microsoft\VisualStudio\SSDT\SportsTrackerApp"

GO
:on error exit
GO
/*
Detect SQLCMD mode and disable script execution if SQLCMD mode is not supported.
To re-enable the script after enabling SQLCMD mode, execute the following:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'SQLCMD mode must be enabled to successfully execute this script.';
        SET NOEXEC ON;
    END


GO
USE [$(DatabaseName)];


GO
/*
The column [dbo].[Team].[HomeLocationID] on table [dbo].[Team] must be added, but the column has no default value and does not allow NULL values. If the table contains data, the ALTER script will not work. To avoid this issue you must either: add a default value to the column, mark it as allowing NULL values, or enable the generation of smart-defaults as a deployment option.

The column [dbo].[Team].[ManagerID] on table [dbo].[Team] must be added, but the column has no default value and does not allow NULL values. If the table contains data, the ALTER script will not work. To avoid this issue you must either: add a default value to the column, mark it as allowing NULL values, or enable the generation of smart-defaults as a deployment option.
*/

IF EXISTS (select top 1 1 from [dbo].[Team])
    RAISERROR (N'Rows were detected. The schema update is terminating because data loss might occur.', 16, 127) WITH NOWAIT

GO
PRINT N'Starting rebuilding table [dbo].[Team]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_Team] (
    [TeamID]         INT            IDENTITY (1, 1) NOT NULL,
    [TeamName]       VARCHAR (256)  NOT NULL,
    [FoundedYear]    VARCHAR (256)  NULL,
    [CoachName]      VARCHAR (256)  NULL,
    [ManagerID]      NVARCHAR (450) NOT NULL,
    [HomeLocationID] INT            NOT NULL,
    PRIMARY KEY CLUSTERED ([TeamID] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[Team])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Team] ON;
        INSERT INTO [dbo].[tmp_ms_xx_Team] ([TeamID], [TeamName])
        SELECT   [TeamID],
                 [TeamName]
        FROM     [dbo].[Team]
        ORDER BY [TeamID] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_Team] OFF;
    END

DROP TABLE [dbo].[Team];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_Team]', N'Team';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Creating Table [dbo].[Location]...';


GO
CREATE TABLE [dbo].[Location] (
    [LocationID]   INT           IDENTITY (1, 1) NOT NULL,
    [LocationName] VARCHAR (256) NOT NULL,
    [City]         VARCHAR (256) NULL,
    [Country]      VARCHAR (256) NULL,
    PRIMARY KEY CLUSTERED ([LocationID] ASC)
);


GO
PRINT N'Creating Table [dbo].[Match]...';


GO
CREATE TABLE [dbo].[Match] (
    [MatchID]    INT                IDENTITY (1, 1) NOT NULL,
    [MatchDate]  DATETIMEOFFSET (7) NOT NULL,
    [LocationID] VARCHAR (256)      NULL,
    [Team1ID]    INT                NOT NULL,
    [Team2ID]    INT                NOT NULL,
    [Team1Score] INT                NULL,
    [Team2Score] INT                NULL,
    PRIMARY KEY CLUSTERED ([MatchID] ASC)
);


GO
PRINT N'Creating Table [dbo].[TeamMember]...';


GO
CREATE TABLE [dbo].[TeamMember] (
    [TeamMemberId] INT           IDENTITY (1, 1) NOT NULL,
    [FirstName]    VARCHAR (256) NOT NULL,
    [LastName]     VARCHAR (256) NOT NULL,
    [DateOfBirth]  DATE          NULL,
    [Position]     VARCHAR (100) NULL,
    [TeamID]       INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([TeamMemberId] ASC)
);


GO
PRINT N'Creating Foreign Key unnamed constraint on [dbo].[Team]...';


GO
ALTER TABLE [dbo].[Team] WITH NOCHECK
    ADD FOREIGN KEY ([ManagerID]) REFERENCES [dbo].[AspNetUsers] ([Id]);


GO
PRINT N'Creating Foreign Key unnamed constraint on [dbo].[Team]...';


GO
ALTER TABLE [dbo].[Team] WITH NOCHECK
    ADD FOREIGN KEY ([HomeLocationID]) REFERENCES [dbo].[Location] ([LocationID]);


GO
PRINT N'Creating Foreign Key unnamed constraint on [dbo].[Match]...';


GO
ALTER TABLE [dbo].[Match] WITH NOCHECK
    ADD FOREIGN KEY ([Team1ID]) REFERENCES [dbo].[Team] ([TeamID]);


GO
PRINT N'Creating Foreign Key unnamed constraint on [dbo].[Match]...';


GO
ALTER TABLE [dbo].[Match] WITH NOCHECK
    ADD FOREIGN KEY ([Team2ID]) REFERENCES [dbo].[Team] ([TeamID]);


GO
PRINT N'Creating Foreign Key unnamed constraint on [dbo].[TeamMember]...';


GO
ALTER TABLE [dbo].[TeamMember] WITH NOCHECK
    ADD FOREIGN KEY ([TeamID]) REFERENCES [dbo].[Team] ([TeamID]) ON DELETE CASCADE;


GO
PRINT N'Creating Procedure [dbo].[GetTeamsByUser]...';


GO
CREATE PROCEDURE dbo.[GetTeamsByUser]
    @userName VARCHAR(256)
AS
BEGIN
    SELECT 
        [TeamID],
        [TeamName],
        [FoundedYear],
        [CoachName],
        [ManagerID],
        [HomeLocationID]

    FROM AspNetUsers U
    JOIN Team T ON U.ID = T.ManagerID

    WHERE U.UserName = @userName;
END;
GO
PRINT N'Refreshing Procedure [dbo].[GetTeamsById]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[GetTeamsById]';


GO
PRINT N'Checking existing data against newly created constraints';


GO
USE [$(DatabaseName)];


GO
CREATE TABLE [#__checkStatus] (
    id           INT            IDENTITY (1, 1) PRIMARY KEY CLUSTERED,
    [Schema]     NVARCHAR (256),
    [Table]      NVARCHAR (256),
    [Constraint] NVARCHAR (256)
);

SET NOCOUNT ON;

DECLARE tableconstraintnames CURSOR LOCAL FORWARD_ONLY
    FOR SELECT SCHEMA_NAME([schema_id]),
               OBJECT_NAME([parent_object_id]),
               [name],
               0
        FROM   [sys].[objects]
        WHERE  [parent_object_id] IN (OBJECT_ID(N'dbo.Team'), OBJECT_ID(N'dbo.Match'), OBJECT_ID(N'dbo.TeamMember'))
               AND [type] IN (N'F', N'C')
                   AND [object_id] IN (SELECT [object_id]
                                       FROM   [sys].[check_constraints]
                                       WHERE  [is_not_trusted] <> 0
                                              AND [is_disabled] = 0
                                       UNION
                                       SELECT [object_id]
                                       FROM   [sys].[foreign_keys]
                                       WHERE  [is_not_trusted] <> 0
                                              AND [is_disabled] = 0);

DECLARE @schemaname AS NVARCHAR (256);

DECLARE @tablename AS NVARCHAR (256);

DECLARE @checkname AS NVARCHAR (256);

DECLARE @is_not_trusted AS INT;

DECLARE @statement AS NVARCHAR (1024);

BEGIN TRY
    OPEN tableconstraintnames;
    FETCH tableconstraintnames INTO @schemaname, @tablename, @checkname, @is_not_trusted;
    WHILE @@fetch_status = 0
        BEGIN
            PRINT N'Checking constraint: ' + @checkname + N' [' + @schemaname + N'].[' + @tablename + N']';
            SET @statement = N'ALTER TABLE [' + @schemaname + N'].[' + @tablename + N'] WITH ' + CASE @is_not_trusted WHEN 0 THEN N'CHECK' ELSE N'NOCHECK' END + N' CHECK CONSTRAINT [' + @checkname + N']';
            BEGIN TRY
                EXECUTE [sp_executesql] @statement;
            END TRY
            BEGIN CATCH
                INSERT  [#__checkStatus] ([Schema], [Table], [Constraint])
                VALUES                  (@schemaname, @tablename, @checkname);
            END CATCH
            FETCH tableconstraintnames INTO @schemaname, @tablename, @checkname, @is_not_trusted;
        END
END TRY
BEGIN CATCH
    PRINT ERROR_MESSAGE();
END CATCH

IF CURSOR_STATUS(N'LOCAL', N'tableconstraintnames') >= 0
    CLOSE tableconstraintnames;

IF CURSOR_STATUS(N'LOCAL', N'tableconstraintnames') = -1
    DEALLOCATE tableconstraintnames;

SELECT N'Constraint verification failed:' + [Schema] + N'.' + [Table] + N',' + [Constraint]
FROM   [#__checkStatus];

IF @@ROWCOUNT > 0
    BEGIN
        DROP TABLE [#__checkStatus];
        RAISERROR (N'An error occurred while verifying constraints', 16, 127);
    END

SET NOCOUNT OFF;

DROP TABLE [#__checkStatus];


GO
PRINT N'Update complete.';


GO
