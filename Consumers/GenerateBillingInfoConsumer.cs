using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Gorold.Billing.Contracts;
using MassTransit;
using MongoDB.Driver;

namespace Gorold.Billing.Consumers
{
    public class GenerateBillingInfoConsumer : IConsumer<BillingInfo>
    {
        private readonly IMongoCollection<BillingInfo> dbCollection;

        public GenerateBillingInfoConsumer(IMongoDatabase mongoDatabase)
        {
            dbCollection = mongoDatabase.GetCollection<BillingInfo>("BillingInfo");
        }

        public async Task Consume(ConsumeContext<BillingInfo> context)
        {
            var message = context.Message;
            //do some logging of the stuff received
            Debug.WriteLine($"Billing received for {message?.UserId} in the amount of {message?.AmountPaid} at {message?.PurchaseDate}'");

            //write in databasez
            await dbCollection.InsertOneAsync(message);
        }
    }
}