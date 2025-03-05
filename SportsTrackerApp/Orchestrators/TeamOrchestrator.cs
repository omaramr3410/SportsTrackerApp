using Mapster;
using SportsTrackerApp.Models;
using SportsTrackerApp.Repository;

namespace SportsTrackerApp.Orchestrators;

public interface ITeamOrchestrator
{
    Task<IEnumerable<TeamModel>> GetTeamsAsync(int[] teamIds);
}

public class TeamOrchestrator(
    ITeamRepository teamRepository,
    ILogger<TeamRepository> logger) : ITeamOrchestrator
{
    private readonly ITeamRepository teamRepository = teamRepository;
    private readonly ILogger<TeamRepository> _logger = logger;

    public async Task<IEnumerable<TeamModel>> GetTeamsAsync(int[] teamIds)
    {
        var teams = await this.teamRepository.GetTeamsAsync(teamIds);

        return teams.Adapt<IEnumerable<TeamModel>>();
    }
}
