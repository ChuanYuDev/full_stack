using Microsoft.AspNetCore.Identity;
using UseCases.UsersServiceInterfaces;

namespace MoviesAPI.Services;

public class UsersService: IUsersService
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly UserManager<IdentityUser> _userManager;

    public UsersService(IHttpContextAccessor httpContextAccessor, UserManager<IdentityUser> userManager)
    {
        _httpContextAccessor = httpContextAccessor;
        _userManager = userManager;
    }

    public async Task<string> GetUserId()
    {
        var httpContext = _httpContextAccessor.HttpContext;

        if (httpContext is null)
        {
            throw new InvalidOperationException("HttpContext is null");
        }

        var claim = httpContext.User.Claims.FirstOrDefault(c => c.Type == "email");

        if (claim is null)
        {
            throw new InvalidOperationException("Claim is null");
        }

        var user = await _userManager.FindByEmailAsync(claim.Value);

        if (user is null)
        {
            throw new InvalidOperationException("User is not found");
        }

        return user.Id;
    }
}