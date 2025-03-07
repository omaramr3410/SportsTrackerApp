namespace SportsTrackerApp.Entities
{
    public class Team
    {
        /// <summary>
        /// Defines unique identifier of team
        /// </summary>
        public int TeamID { get; init; }
        
        /// <summary>
        /// Defines team name
        /// </summary>
        public string? TeamName { get; init; }

        /// <summary>
        /// Defines team's founding year
        /// </summary>
        public string? FoundedYear { get; init; }
        
        /// <summary>
        /// Defines team's coach name
        /// </summary>
        public string? CoachName { get; init; }

        /// <summary>
        /// Defines the team manager id, linked to the AspNetCore Identity User ID
        /// </summary>
        public required string ManagerID { get; init; }

        /// <summary>
        /// Defines home location
        /// </summary>
        public int HomeLocationID { get; init; }

    }
}
