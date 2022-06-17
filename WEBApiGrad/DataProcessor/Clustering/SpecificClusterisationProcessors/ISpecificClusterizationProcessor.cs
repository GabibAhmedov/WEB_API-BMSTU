using ImmigrationDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using DBSCAN;
using System.Text;
using Accord.MachineLearning;
using WEBApiGrad.HttpClients;
using IntermediateModels;

namespace WEBApiGrad.DataProcessor.Clustering
{
    public interface ISpecificClusterizationProcessor
    {
        public List<ClusterInt> PerformClustering(List<ProfileInt> profileInts);
    }
}
