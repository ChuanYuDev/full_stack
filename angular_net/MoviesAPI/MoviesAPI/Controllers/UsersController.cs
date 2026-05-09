using CoreBusiness.DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using MoviesAPI.Utilities;
using UseCases.DataStoreInterfaces;

namespace MoviesAPI.Controllers;

[ApiController]
[Route("api/users")]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "isadmin")]
public class UsersController: CustomBaseController
{
    private readonly IUsersRepository _usersRepository;
    private const string CacheTag = "users";

    public UsersController(IUsersRepository usersRepository, IOutputCacheStore outputCacheStore
    ): base(outputCacheStore)
    {
        _usersRepository = usersRepository;
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<ActionResult<AuthenticationResponseDto?>> Register([FromBody] UserCredentialsDto userCredentialsDto)
    {
        var result = await _usersRepository.Register(userCredentialsDto);
        var identityResult = result.identityResult;

        if (identityResult.Succeeded)
        {
            await OutputCacheStore.EvictByTagAsync(CacheTag, default);
            return result.authenticationResponseDto;
        }
        
        return BadRequest(identityResult.Errors);
    }
    
    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult<AuthenticationResponseDto?>> Login([FromBody] UserCredentialsDto userCredentialsDto)
    {
        var result = await _usersRepository.Login(userCredentialsDto);
        var loginResult = result.loginResult;

        if (!loginResult)
        {
            return BadRequest(_usersRepository.BuildIncorrectLoginErrorMessage());
        }

        return result.authenticationResponseDto;
    }

    [HttpGet("users-list")]
    [OutputCache(Tags = [CacheTag])]
    public async Task<List<UserDto>> Get([FromQuery] PaginationDto paginationDto)
    {
        var count = await _usersRepository.Count();
        HttpContext.InsertPaginationParametersInHeader(count);
        return await _usersRepository.Get(paginationDto);
    }
    
    [HttpPost("make-admin")]
    public async Task<IActionResult> MakeAdmin(EditClaimDto editClaimDto)
    {
        var found = await _usersRepository.MakeAdmin(editClaimDto);

        if (!found)
        {
            return NotFound();
        }

        return NoContent();
    }

    [HttpPost("remove-admin")]
    public async Task<IActionResult> RemoveAdmin(EditClaimDto editClaimDto)
    {
        var found = await _usersRepository.RemoveAdmin(editClaimDto);

        if (!found)
        {
            return NotFound();
        }

        return NoContent();
    }

}