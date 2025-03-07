namespace SportsTrackerApp.Entities
{
    public class Match
    {
        /// <summary>
        /// Defines the unique identifier of a match
        /// </summary>
        public int MatchID { get; init; }
        
        /// <summary>
        /// Defines the match date and time
        /// </summary>
        public DateTimeOffset MatchDate { get; init; }

        /// <summary>
        /// Defines the match location
        /// </summary>
        public string? LocationID { get; init; }

        /// <summary>
        /// Defines the home team 
        /// </summary>
        public int TeamHomeID{ get; init; }

        /// <summary>
        /// Defines the away team
        /// </summary>
        public int TeamAwayID { get; init; }
        
        /// <summary>
        /// Defines the home team's score
        /// </summary>
        public int TeamHomeScore { get; init; }

        /// <summary>
        /// Defines the away team's score
        /// </summary>
        public int TeamAwayScore { get; init; }
    }
}
