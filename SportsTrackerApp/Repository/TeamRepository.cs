using Dapper;
using System.Data;
using SportsTrackerApp.Entities;
using SportsTrackerApp.Context;
using SportsTrackerApp.Constants;

namespace SportsTrackerApp.Repository
{
    public interface ITeamRepository
    {
        /// <summary>
        /// Retrieve a list of team entities by team ids
        /// </summary>
        /// <param name="teamIds">Collection of team ids</param>
        /// <returns>Collection of team entities connected to ids</returns>
        Task<IEnumerable<TeamEntity>> GetTeamsAsync(int[] teamIds);
    }

    public class TeamRepository(
        IDapperContext context,
        ILogger<TeamRepository> logger) : ITeamRepository
    {
        private readonly IDapperContext context = context;

        /// <inheritdoc/>
        public async Task<IEnumerable<TeamEntity>> GetTeamsAsync(int[] teamIds)
        {
            var query = "EXECUTE dbo.[GetTeamsById] @TeamIds";
            using var connection = context.CreateConnection();

            var teamIdTable = new DataTable();
            teamIdTable.Columns.Add("ID", typeof(int));

            foreach (var id in teamIds) teamIdTable.Rows.Add(id);

            var parameters = new DynamicParameters();
            parameters.Add("@TeamIds", teamIdTable.AsTableValuedParameter(DatabaseTypes.tpGenericIntType));


            var teams = await connection.QueryAsync<TeamEntity>(query, parameters);

            return teams;
        }
    }
}
