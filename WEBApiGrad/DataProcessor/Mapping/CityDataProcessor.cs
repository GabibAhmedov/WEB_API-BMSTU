using ImmigrationDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using WEBApiGrad.HttpClients;
using IntermediateModels;

namespace WEBApiGrad.DataProcessor
{
    public class CityDataProcessor:IDataProcessor<List<CityInt>>
    {
        private readonly HttpClient _httpClient;
        private readonly ClientCategory _microserviceClientCategory = ClientCategory.MicroserviceClient;
        public CityDataProcessor(IHttpClientFactory clientFactory)
        {
            _httpClient = clientFactory.CreateClient(_microserviceClientCategory.Category);
        }
        public async Task<List<CityInt>> PrepareDataAsync()
        {
            var citiesResult = await _httpClient.GetAsync("cities");


            var cityInts = JsonConvert.DeserializeObject<List<CityInt>>(await citiesResult.Content.ReadAsStringAsync());

            cityInts = cityInts.Where(c => c.CountryName != "Россия").ToList();

            return cityInts;
        }
    }
}
