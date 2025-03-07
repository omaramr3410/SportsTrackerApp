using Dapper;
using SportsTrackerApp.Entities;
using SportsTrackerApp.Context;
using SportsTrackerApp.Helpers;

namespace SportsTrackerApp.Repository
{
    public interface ITeamRepository
    {
        /// <summary>
        /// Retrieve a list of team entities by team ids
        /// </summary>
        /// <param name="teamIds">Collection of team ids</param>
        /// <returns>Collection of team entities connected to ids</returns>
        Task<IEnumerable<TeamEntity>> GetTeamsByIdAsync(int[] teamIds);

        /// <summary>
        /// Retrieve list of team entities by user name
        /// </summary>
        /// <param name="userName">User security context's name</param>
        /// <returns>Colleciton of team entities connected to username</returns>
        Task<IEnumerable<TeamEntity>> GetTeamsByUserAsync(string userName);
    }

    public class TeamRepository(
        IDapperContext context,
        ILogger<ITeamRepository> logger) : ITeamRepository
    {
        private readonly IDapperContext context = context;
        private readonly ILogger<ITeamRepository> logger = logger;

        /// <inheritdoc/>
        public async Task<IEnumerable<TeamEntity>> GetTeamsByIdAsync(int[] teamIds)
        {
            try
            {
                var query = "EXECUTE dbo.[GetTeamsById] @teamIds";
                using var connection = context.CreateConnection();

                var teams = await connection.QueryAsync<TeamEntity>(query, new
                {
                    teamIds = teamIds.AsTVP()
                });

                return teams;
            }
            catch
            {
                this.logger.LogError("Error occured retrieving teams by ids", [new {
                    method = nameof(GetTeamsByIdAsync),
                    teamIds
                }]);

                throw;
            }
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<TeamEntity>> GetTeamsByUserAsync(string userName)
        {
            try
            {
                var query = "EXECUTE dbo.[GetTeamsByUser] @userName";
                using var connection = context.CreateConnection();

                var teams = await connection.QueryAsync<TeamEntity>(query, new
                {
                    userName
                });

                return teams;
            }
            catch
            {
                this.logger.LogError("Error occured retrieving teams by user name", [new
                {
                    method = nameof(GetTeamsByIdAsync),
                    userName
                }]);

                throw;
            }
        }
    }
}
