using Microsoft.AspNetCore.Mvc;

namespace WebAPITest.Controllers;

[Route("api/configurations")]
public class ConfigurationsController: ControllerBase
{
    private readonly IConfiguration _configuration;

    public ConfigurationsController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpGet("get-connection-string")]
    public IActionResult GetConnectionString()
    {
        var connectionString = _configuration.GetValue<string>("myConnectionString");

        return Ok(connectionString);
    }
}