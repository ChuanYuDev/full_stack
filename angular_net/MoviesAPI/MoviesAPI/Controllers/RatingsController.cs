using CoreBusiness.DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UseCases.DataStoreInterfaces;

namespace MoviesAPI.Controllers;

[ApiController]
[Route("api/ratings")]
public class RatingsController: ControllerBase
{
    private readonly IRatingsRepository _ratingsRepository;

    public RatingsController(IRatingsRepository ratingsRepository)
    {
        _ratingsRepository = ratingsRepository;
    }

    [HttpPut]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Put([FromBody] MovieRatingCreationDto movieRatingCreationDto)
    {
        var found = await _ratingsRepository.AddOrUpdate(movieRatingCreationDto);

        if (!found)
        {
            return NotFound();
        }

        return NoContent();
    }
}