using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SportsTrackerApp.Dtos;
using SportsTrackerApp.Models;
using System.Security.Claims;

namespace SportsTrackerApp.Controllers.User;

public interface IUserController
{
    /// <summary>
    /// Route to attempt a log in
    /// </summary>
    /// <param name="req">Login request body</param>
    /// <returns>User</returns>
    Task<UserModel?> LoginAsync([FromBody] UserLoginDto req);
    
    /// <summary>
    /// Route to attempt a user log out
    /// </summary>
    /// <returns>Action result during this operation</returns>
    Task<IActionResult> LogoutAsync();
}

[ApiController]
[Route(UserRoutes.baseRoute)]
public class UserController(
    ILogger<IUserController> logger) : ControllerBase, IUserController
{
    private readonly ILogger<IUserController> logger = logger;

    [HttpPost(UserRoutes.login)]
    //<inheritdoc/>
    public async Task<UserModel?> LoginAsync([FromBody] UserLoginDto req)
    {
        var claims = new List<Claim>
        {
            new("usr", req.UserName)
        };

        var identity = new ClaimsIdentity(claims, "cookie");
        var user = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync("cookie", user);

        return new UserModel
        {
            UserName = req.UserName,
            PasswordHash = req.Password
        };
    }

    [HttpGet(UserRoutes.logout)]
    //<inheritdoc/>
    [Authorize]
    public async Task<IActionResult> LogoutAsync()
    {
        try
        {
            await HttpContext.SignOutAsync();

            return Ok();
        }
        catch
        {
            this.logger.LogError("Error logging out user", new {
                method = nameof(LogoutAsync),
                HttpContext
            });

            return BadRequest();
        }
    }
}
