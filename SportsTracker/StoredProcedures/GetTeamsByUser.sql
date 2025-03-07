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
