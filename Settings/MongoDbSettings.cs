using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Gorold.Billing.Settings
{
    public class MongoDbSettings
    {
        public string Host { get; init; }

        public int Port { get; init; }

        public string ConnectionString => $"mongodb://{Host}:{Port}";
    }
}