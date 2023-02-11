using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gorold.Billing.Contracts;
using MassTransit;

namespace Gorold.Billing.Consumers
{
    public class GenerateBillingInfoConsumer : IConsumer<BillingInfo>
    {
        public Task Consume(ConsumeContext<BillingInfo> context)
        {
            var message = context.Message;
            //do some logging of the stuff received
            Console.WriteLine($"Billing received for {message?.UserId} in the amount of {message?.AmountPaid} at {message?.PurchaseDate}'");

            //write in database



            return Task.CompletedTask;
        }
    }
}