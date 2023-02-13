using Gorold.Billing.Contracts;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace Gorold.Billing.Controllers;

[ApiController]
[Route("[controller]")]
public class BillingController : ControllerBase
{
    private readonly IMongoCollection<BillingInfo> dbCollection;

    public BillingController(IMongoDatabase mongoDatabase)
    {
        dbCollection = mongoDatabase.GetCollection<BillingInfo>("BillingInfo");
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok("Dummy value");
    }
}
