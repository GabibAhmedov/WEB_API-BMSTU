using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WEBApiGrad.HttpClients
{
    public class ClientCategory
    {
        private ClientCategory(string value) { Category = value; }

        public string Category { get; private set; }

        public static ClientCategory MicroserviceClient => new("microserviceClient");
        public static ClientCategory ClusterisationClient => new("clusterizationClient");

    }
}
