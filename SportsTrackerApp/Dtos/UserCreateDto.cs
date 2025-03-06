namespace SportsTrackerApp.Dtos
{
    public class UserCreateDto
    {
        /// <summary>
        /// Defines the user's name
        /// </summary>
        public required string UserName { get; set; }

        /// <summary>
        /// Defines the user's password
        /// </summary>
        public required string Password { get; set; }
    }
}
