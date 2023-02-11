using Microsoft.AspNetCore.Mvc;

namespace Gorold.Billing.Controllers;

[ApiController]
[Route("[controller]")]
public class BillingController : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok("Dummy value");
    }
}
