using Mapster;
using SportsTrackerApp.Models;
using SportsTrackerApp.Repository;
using System.Security.Claims;

namespace SportsTrackerApp.Orchestrators;

public interface ITeamOrchestrator
{
    /// <summary>
    /// Method to retrieve teams by ids
    /// </summary>
    /// <param name="teamIds">List of team ids</param>
    /// <returns>Collection of team models</returns>
    Task<IEnumerable<TeamModel>> GetTeamsByIdAsync(int[] teamIds);

    /// <summary>
    /// Method to retrieve teams by user name
    /// </summary>
    /// <param name="user">Claims Principal security context's user name</param>
    /// <returns>Collection of teams</returns>
    Task<IEnumerable<TeamModel>> GetTeamsByUserAsync(string userName);
}

public class TeamOrchestrator(
    ITeamRepository teamRepository,
    ILogger<ITeamRepository> logger) : ITeamOrchestrator
{
    private readonly ITeamRepository teamRepository = teamRepository;
    private readonly ILogger<ITeamRepository> _logger = logger;

    //<inheritdoc/>
    public async Task<IEnumerable<TeamModel>> GetTeamsByIdAsync(int[] teamIds)
    {
        var teams = await this.teamRepository.GetTeamsByIdAsync(teamIds);

        return teams.Adapt<IEnumerable<TeamModel>>();
    }
    
    //<inheritdoc/>
    public async Task<IEnumerable<TeamModel>> GetTeamsByUserAsync(string userName)
    {
        var teams = await this.teamRepository.GetTeamsByUserAsync(userName);

        return teams.Adapt<IEnumerable<TeamModel>>();
    }
}
