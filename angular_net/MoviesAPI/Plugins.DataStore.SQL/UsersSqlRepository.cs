using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using CoreBusiness.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using UseCases.DataStoreInterfaces;

namespace Plugins.DataStore.SQL;

public class UsersSqlRepository: BaseSqlRepository, IUsersRepository
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly IConfiguration _configuration;

    public UsersSqlRepository(
        IHttpContextAccessor httpContextAccessor,
        UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager,
        IConfiguration configuration, 
        ApplicationDbContext context,
        IMapper mapper
    ) : base(context, mapper)
    {
        _httpContextAccessor = httpContextAccessor;
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
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
    
    public async Task<(IdentityResult identityResult, AuthenticationResponseDto? authenticationResponseDto)> Register(UserCredentialsDto userCredentialsDto)
    {
        var user = new IdentityUser
        {
            UserName = userCredentialsDto.Email,
            Email = userCredentialsDto.Email
        };

        var result = await _userManager.CreateAsync(user, userCredentialsDto.Password);

        if (!result.Succeeded)
        {
            return (result, null);
        }
        
        var authenticationResponseDto = await BuildToken(user);
        return (result, authenticationResponseDto);

    }

    public async Task<(bool loginResult, AuthenticationResponseDto? authenticationResponseDto)> Login(UserCredentialsDto userCredentialsDto)
    {
        var user = await _userManager.FindByEmailAsync(userCredentialsDto.Email);

        if (user is null)
        {
            return (false, null);
        }
        
        var result = await _signInManager.CheckPasswordSignInAsync(user, userCredentialsDto.Password, lockoutOnFailure: false);

        if (!result.Succeeded)
        {
            return (false, null);
        }
        
        var authenticationResponseDto = await BuildToken(user);
        return (true, authenticationResponseDto);
    }

    public IEnumerable<IdentityError> BuildIncorrectLoginErrorMessage()
    {
        var identityError = new IdentityError { Description = "Incorrect login" };

        var errors = new List<IdentityError> { identityError };

        return errors;
    }
    
    public async Task<bool> MakeAdmin(EditClaimDto editClaimDto)
    {
        var user = await _userManager.FindByEmailAsync(editClaimDto.Email);

        if (user is null)
        {
            return false;
        }

        await _userManager.AddClaimAsync(user, new Claim("isadmin", "true"));

        return true;
    }

    public async Task<bool> RemoveAdmin(EditClaimDto editClaimDto)
    {
    
        var user = await _userManager.FindByEmailAsync(editClaimDto.Email);

        if (user is null)
        {
            return false;
        }

        await _userManager.RemoveClaimAsync(user, new Claim("isadmin", "true"));

        return true;
    }

    public async Task<int> Count()
    {
        return await Count<IdentityUser>();
    }

    public async Task<List<UserDto>> Get(PaginationDto paginationDto)
    {
        return await Get<IdentityUser, UserDto>(orderBy: user => user.Email, paginationDto: paginationDto);
    }

    private async Task<AuthenticationResponseDto> BuildToken(IdentityUser user)
    {
        var claims = new List<Claim>
        {
            new Claim("email", user.Email ?? ""),
            new Claim("whatever I want", "any value")
        };

        var claimsDb = await _userManager.GetClaimsAsync(user);
        
        claims.AddRange(claimsDb);

        var expiration = DateTime.UtcNow.AddYears(1);
        
        var jwtKey = _configuration.GetValue<string>("JwtKey") ?? throw new InvalidOperationException("JWT key not found");

        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey));

        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
        
        var securityToken = new JwtSecurityToken(issuer: null, audience: null, claims: claims, expires: expiration, signingCredentials: credentials);

        var token = new JwtSecurityTokenHandler().WriteToken(securityToken);

        return new AuthenticationResponseDto
        {
            Token = token,
            Expiration = expiration
        };
    }
}