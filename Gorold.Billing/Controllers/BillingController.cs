using Gorold.Billing.Contracts;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;

namespace Gorold.Billing.Controllers;

[ApiController]
[Route("[controller]")]
public class BillingController : ControllerBase
{
    private readonly IMongoCollection<BillingInfo> dbCollection;
    private readonly FilterDefinitionBuilder<BillingInfo> filterBuilder = Builders<BillingInfo>.Filter;

    public BillingController(IMongoDatabase mongoDatabase)
    {
        dbCollection = mongoDatabase.GetCollection<BillingInfo>("BillingInfo");
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await dbCollection.Find(filterBuilder.Empty).ToListAsync());
    }
}
