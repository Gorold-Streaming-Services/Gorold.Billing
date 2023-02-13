using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gorold.Billing.Contracts
{
    public record BillingInfo(
            Guid Id,
            double AmountPaid,
            DateTimeOffset PurchaseDate
    );
}