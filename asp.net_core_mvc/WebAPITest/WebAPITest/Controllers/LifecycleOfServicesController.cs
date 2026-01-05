using Microsoft.AspNetCore.Mvc;

namespace WebAPITest.Controllers;

[Route("api/lifecycle-of-services")]
public class LifecycleOfServicesController: ControllerBase
{
    private readonly TransientService _transient1;
    private readonly TransientService _transient2;
    private readonly ScopedService _scoped1;
    private readonly ScopedService _scoped2;
    private readonly SingletonService _singleton;

    public LifecycleOfServicesController(TransientService transient1, TransientService transient2, ScopedService scoped1, ScopedService scoped2, SingletonService singleton)
    {
        _transient1 = transient1;
        _transient2 = transient2;
        _scoped1 = scoped1;
        _scoped2 = scoped2;
        _singleton = singleton;
    }

    [HttpGet]
    public IActionResult GetLifecycleServices()
    {
        return Ok(new
        {
            transients = new
            {
                transient1 = _transient1.GetId(),
                transient2 = _transient2.GetId(),
            },
            scopeds = new
            {
                scoped1 = _scoped1.GetId(),
                scoped2 = _scoped2.GetId(),
            },
            singleton = new
            {
                singleton = _singleton.GetId(),
            },
        });
    }
}