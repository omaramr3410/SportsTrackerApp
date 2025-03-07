CREATE PROCEDURE dbo.[GetTeamsById]
    @teamIds dbo.[GenericIntType] READONLY
AS
BEGIN
    SELECT TeamID, TeamName

    FROM Team

    WHERE TeamID IN (SELECT ID FROM @teamIds);
END;
