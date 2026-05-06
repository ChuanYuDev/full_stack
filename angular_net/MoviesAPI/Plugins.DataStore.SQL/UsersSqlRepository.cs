using AutoMapper;
using CoreBusiness.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using UseCases.DataStoreInterfaces;

namespace Plugins.DataStore.SQL;

public class UsersSqlRepository: BaseSqlRepository, IUsersRepository
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly UserManager<IdentityUser> _userManager;

    public UsersSqlRepository(IHttpContextAccessor httpContextAccessor, UserManager<IdentityUser> userManager, ApplicationDbContext context, IMapper mapper) : base(context, mapper)
    {
        _httpContextAccessor = httpContextAccessor;
        _userManager = userManager;
    }

    public async Task<int> Count()
    {
        return await Count<IdentityUser>();
    }

    public async Task<List<UserDto>> Get(PaginationDto paginationDto)
    {
        return await Get<IdentityUser, UserDto>(orderBy: user => user.Email, paginationDto: paginationDto);
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