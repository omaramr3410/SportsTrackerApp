using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportsTrackerApp.Dtos;
using SportsTrackerApp.Orchestrators;
using System.CodeDom;
using System.Security.Claims;

namespace SportsTrackerApp.Controllers;

public interface ITeamController
{
    /// <summary>
    /// Endpoint to retrieve teams in bulk
    /// </summary>
    /// <param name="teamIds">List of team ids</param>
    /// <returns>Collection of Teams</returns>
    Task<IEnumerable<TeamDto>> GetTeamsByIdAsync([FromQuery] int[] teamIds);

    /// <summary>
    /// Endpoint to retrieve a user's list of teams
    /// </summary>
    /// <returns>Collection of teams</returns>
    Task<IEnumerable<TeamDto>> GetTeamsByUserAsync();
}

[Authorize]
[ApiController]
[Route(TeamRoutes.baseRoute)]
public class TeamController(
    ITeamOrchestrator teamOrchestrator,
    ILogger<TeamController> logger) : ControllerBase, ITeamController
{
    private readonly ITeamOrchestrator teamOrchestrator = teamOrchestrator;
    private readonly ILogger<TeamController> _logger = logger;

    [AllowAnonymous]
    [HttpGet(TeamRoutes.getTeamsById)]
    //<inheritdoc/>
    public async Task<IEnumerable<TeamDto>> GetTeamsByIdAsync([FromQuery] int[] teamIds)
    {
        var teams = await this.teamOrchestrator.GetTeamsByIdAsync(teamIds);

        return teams.Adapt<IEnumerable<TeamDto>>();
    }

    [HttpGet(TeamRoutes.getTeamsByUser)]
    //<inheritdoc/>
    public async Task<IEnumerable<TeamDto>> GetTeamsByUserAsync()
    {
        var userName = HttpContext.User?.Identity?.Name ?? throw new Exception("Security Context can not provider user name");

        var teams = await this.teamOrchestrator.GetTeamsByUserAsync(userName);

        return teams.Adapt<IEnumerable<TeamDto>>();
    }
}
