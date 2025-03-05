CREATE PROCEDURE dbo.[GetTeamsById]
    @TeamIds dbo.[GenericIntType] READONLY
AS
BEGIN
    SELECT TeamID, TeamName

    FROM Team

    WHERE TeamID IN (SELECT ID FROM @TeamIds);
END;
