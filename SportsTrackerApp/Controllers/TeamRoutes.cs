namespace SportsTrackerApp.Controllers
{
    public class TeamRoutes
    {
        /// <summary>
        /// Base Route
        /// </summary>
        public const string baseRoute = "/team";

        /// <summary>
        /// Route to retrieve teams by query string params, allows anonymous access
        /// </summary>
        public const string getTeamsById = $"/teams";

        /// <summary>
        /// Route to retrieve teams for a user
        /// </summary>
        public const string getTeamsByUser = $"/user/teams";
    }
}