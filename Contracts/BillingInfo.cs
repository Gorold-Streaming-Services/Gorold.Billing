using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gorold.Billing.Contracts
{
    public class BillingInfo
    {
        public Guid UserId { get; set; }
        public double AmountPaid { get; set; }
        public DateTimeOffset PurchaseDate { get; set; }
    }
}