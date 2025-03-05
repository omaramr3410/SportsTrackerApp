using Mapster;
using Microsoft.AspNetCore.Mvc;
using SportsTrackerApp.Dto;
using SportsTrackerApp.Orchestrators;

namespace SportsTrackerApp.Controllers;

public interface ITeamController
{
    Task<IEnumerable<TeamDto>> GetTeamsAsync([FromQuery] int[] teamIds);
}

[ApiController]
[Route(TeamRoutes.baseRoute)]
public class TeamController(
    ITeamOrchestrator teamOrchestrator,
    ILogger<TeamController> logger) : ControllerBase, ITeamController
{
    private readonly ITeamOrchestrator teamOrchestrator = teamOrchestrator;
    private readonly ILogger<TeamController> _logger = logger;

    [HttpGet(TeamRoutes.getTeams)]
    public async Task<IEnumerable<TeamDto>> GetTeamsAsync([FromQuery] int[] teamIds)
    {
        var teams = await this.teamOrchestrator.GetTeamsAsync(teamIds);

        return teams.Adapt<IEnumerable<TeamDto>>();
    }
}
