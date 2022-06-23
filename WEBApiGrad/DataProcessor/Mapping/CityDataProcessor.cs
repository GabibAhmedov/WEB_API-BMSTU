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
using WEBApiGrad.WebMediator;

namespace WEBApiGrad.DataProcessor
{
    public class CityDataProcessor:IDataProcessor<List<CityInt>>
    {
        private readonly ClientCategory _microserviceClientCategory = ClientCategory.MicroserviceClient;
        private readonly IWebMediator _webMediator;
        public CityDataProcessor(
            IWebMediator webMediator)
        {
            _webMediator = webMediator;
        }
        public async Task<List<CityInt>> PrepareDataAsync()
        {
            var cityInts = await _webMediator.GetCitiesAsync();

            cityInts = cityInts.Where(c => c.CountryName != "Россия").ToList();

            return cityInts;
        }
    }
}
