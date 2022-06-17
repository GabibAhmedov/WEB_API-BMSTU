using ImmigrationDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using WEBApiGrad.HttpClients;
using IntermediateModels;
using System.Threading;
using Converters;
using System.Text;

namespace WEBApiGrad.WebIntermediary
{
    public class ClusterWebMediator
    {
        private readonly HttpClient _httpClient;
        private readonly ClientCategory _microserviceClientCategory = ClientCategory.MicroserviceClient;

        public ClusterWebMediator(IHttpClientFactory clientFactory)
        {
            _httpClient = clientFactory.CreateClient(_microserviceClientCategory.Category);
        }

        public async Task<List<ClusterInt>> GetClustersAsync()
        {
            var result = await _httpClient.GetAsync("clusters");
            var stringResult = await result.Content.ReadAsStringAsync();
            var clusterInts = JsonConvert.DeserializeObject<List<ClusterInt>>(stringResult);
            result.EnsureSuccessStatusCode();
            return clusterInts;
        }

        public async Task<List<ClusterDTO>> PostClustersAsync(List<ClusterInt> clusterInts)
        {
            var tokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(500000));
            var clusterDTOs = clusterInts.Select(c => ClusterConverter.ConvertToDTO(c)).ToList();
            var stringPlots = JsonConvert.SerializeObject(clusterInts);
            var content = new StringContent(stringPlots, Encoding.UTF8, "application/json");
            var result = await _httpClient.PostAsync("clusters", content, tokenSource.Token);
            result.EnsureSuccessStatusCode();
            return clusterDTOs;
        }
    }
}
