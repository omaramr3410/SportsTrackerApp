namespace SportsTrackerApp.Entities
{
    public class TeamMember
    {
        /// <summary>
        /// Defines unique identifier of team member
        /// </summary>
        public int TeamMemberID { get; init; }
        
        /// <summary>
        /// Defines team member's first name
        /// </summary>
        public string? FirstName { get; init; }

        /// <summary>
        /// Defines team member's last name
        /// </summary>
        public string? LastName { get; init; }

        /// <summary>
        /// Defines team member's date of birth
        /// </summary>
        public DateTime? DateOfBirth { get; init; }
        
        /// <summary>
        /// Defines team member's position
        /// </summary>
        public string? CoachName { get; init; }

        /// <summary>
        /// Defines team member's team
        /// </summary>
        public int TeamID { get; init; }

    }
}
