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
    private readonly IRatingsSqlRepository _ratingsSqlRepository;

    public RatingsController(IRatingsSqlRepository ratingsSqlRepository)
    {
        _ratingsSqlRepository = ratingsSqlRepository;
    }

    [HttpPut]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<IActionResult> Put([FromBody] MovieRatingCreationDto movieRatingCreationDto)
    {
        var found = await _ratingsSqlRepository.AddOrUpdate(movieRatingCreationDto);

        if (!found)
        {
            return NotFound();
        }

        return NoContent();
    }
}