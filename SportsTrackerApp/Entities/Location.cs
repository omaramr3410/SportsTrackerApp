namespace SportsTrackerApp.Entities
{
    public class Location
    {
        /// <summary>
        /// Defines the unique identifier of location
        /// </summary>
        public int LocationID { get; init; }

        /// <summary>
        /// Defines the location's name
        /// </summary>
        public required string LocationName { get; init; }

        /// <summary>
        /// Defines the location city
        /// </summary>
        public string? City { get; init; }

        /// <summary>
        /// Defines the location country
        /// </summary>
        public string? Country { get; init; }
    }
}
