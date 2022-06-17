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
using WEBApiGrad.WebMediator;

namespace WEBApiGrad.DataProcessor.Clustering
{
    public class ClusterDataProcessor : IDataProcessor<List<ClusterInt>>
    {
        private readonly IEnumerable<ISpecificClusterizationProcessor> _specificPlotProcessors;
        private readonly IWebMediator _webMediator;

        public ClusterDataProcessor(IEnumerable<ISpecificClusterizationProcessor> specificPlotProcessors,
            IWebMediator webMediator)
        {
            _specificPlotProcessors = specificPlotProcessors;
            _webMediator = webMediator;
        }

        public async Task<List<ClusterInt>> PrepareDataAsync()
        {
            var profileInts = await _webMediator.GetProfilesAsync();
            profileInts = profileInts.Where(p => p.City is not null && p.City.CountryName != "Россия" && p.City.CountryName is not null).ToList();
            var clusterInts = new List<ClusterInt>();
            foreach(var processor in _specificPlotProcessors)
            {
                clusterInts = clusterInts.Concat(processor.PerformClustering(profileInts)).ToList();
            }
            return clusterInts;
        }
    }
}
