using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using CoreBusiness.DTOs;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace MoviesAPI.Controllers;

[ApiController]
[Route("api/users")]
public class UsersController: ControllerBase
{
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SignInManager<IdentityUser> _signInManager;
    private readonly IConfiguration _configuration;

    public UsersController(
        UserManager<IdentityUser> userManager,
        SignInManager<IdentityUser> signInManager,
        IConfiguration configuration
    )
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _configuration = configuration;
    }
    
    [HttpPost("register")]
    public async Task<ActionResult<AuthenticationResponseDto>> Register(UserCredentialsDto userCredentialsDto)
    {
        var user = new IdentityUser
        {
            UserName = userCredentialsDto.Email,
            Email = userCredentialsDto.Email
        };

        var result = await _userManager.CreateAsync(user, userCredentialsDto.Password);

        if (result.Succeeded)
        {
            return await BuildToken(user);
        }
        else
        {
            return BadRequest(result.Errors);
        }
    }
    
    [HttpPost("login")]
    public async Task<ActionResult<AuthenticationResponseDto>> Login(UserCredentialsDto userCredentialsDto)
    {
        var user = await _userManager.FindByEmailAsync(userCredentialsDto.Email);

        if (user is null)
        {
            return BadRequest(BuildIncorrectLoginErrorMessage());
        }
        
        var result = await _signInManager.CheckPasswordSignInAsync(user, userCredentialsDto.Password, lockoutOnFailure: false);

        if (result.Succeeded)
        {
            return await BuildToken(user);
        }
        else
        {
            return BadRequest(BuildIncorrectLoginErrorMessage());
        }
    }

    private async Task<AuthenticationResponseDto> BuildToken(IdentityUser user)
    {
        var claims = new List<Claim>
        {
            new Claim("Email", user.Email ?? ""),
            new Claim("Whatever I want", "Any value")
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

    private IEnumerable<IdentityError> BuildIncorrectLoginErrorMessage()
    {
        var identityError = new IdentityError { Description = "Incorrect login" };

        var errors = new List<IdentityError> { identityError };

        return errors;
    }
}