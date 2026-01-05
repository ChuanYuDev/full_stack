using Microsoft.AspNetCore.Mvc;

namespace WebAPITest.Controllers;

[Route("api/configurations")]
public class ConfigurationsController: ControllerBase
{
    private readonly IConfiguration configuration;

    public ConfigurationsController(IConfiguration configuration)
    {
        this.configuration = configuration;
    }

    [HttpGet("get-connection-string")]
    public IActionResult GetConnectionString()
    {
        var connectionString = configuration.GetValue<string>("myConnectionString");

        return Ok(connectionString);
    }
}