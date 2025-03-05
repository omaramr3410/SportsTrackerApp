using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using SportsTrackerApp.Dtos;
using SportsTrackerApp.Models;
using System.Security.Claims;

namespace SportsTrackerApp.Controllers.User;

public interface IUserController
{
    Task<UserModel?> LoginAsync([FromBody] UserLoginDto req);
}

[ApiController]
[Route(UserRoutes.baseRoute)]
public class UserController(
    IHttpContextAccessor ctx,
    ILogger<UserController> logger) : ControllerBase, IUserController
{
    private readonly IHttpContextAccessor ctx = ctx;
    private readonly ILogger<UserController> logger = logger;

    [HttpPost(UserRoutes.login)]
    public async Task<UserModel?> LoginAsync([FromBody] UserLoginDto req)
    {
        var claims = new List<Claim>
        {
            new("usr", req.UserName)
        };

        var identity = new ClaimsIdentity(claims, "cookie");
        var user = new ClaimsPrincipal(identity);

        await ctx.HttpContext.SignInAsync("cookie", user);

        return new UserModel
        {
            UserName = req.UserName,
            PasswordHash = req.Password
        };
    }
}
