using DataBaseAPI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Model;
using VkNet.Model.RequestParams;
using Microsoft.EntityFrameworkCore;
using DataBaseAPI.Repositories;
using DataBaseAPI.Data.Models;
using City = DataBaseAPI.Data.Models.City;
using Yandex.Geocoder;
using DataBaseAPI.Options;
using Microsoft.Extensions.Options;
using VkNet.Abstractions;

namespace DataBaseAPI.Importers
{
    public class VkImporter : IDataImporter<Profile>
    {

        private readonly IVkApi _source;
        private readonly IEnumerable<IVkImportAlternatives<Profile>> _importAlternatives;
        private readonly IProfileRepository _repository;
        public YandexGeocoderOptions Options { get; set; }

        public VkImporter(IProfileRepository repository,
            IOptions<YandexGeocoderOptions> options,
            IVkApi source,
            IEnumerable<IVkImportAlternatives<Profile>> importAlternatives)
        {
            _repository = repository;
            Options = options.Value;
            _source = source;
            _importAlternatives = importAlternatives;
        }

        public async Task<int> ImportDataAsync()
        {
            await _repository.ClearAsync();
            var profiles = new List<Profile>();
            foreach (var alt in _importAlternatives)
            {
                profiles = profiles.Concat(await alt.ImportDataAlternativelyAsync()).ToList();
            }

            var uniqueProfiles = profiles.GroupBy(p => p.VKId)
                .Select(p => p.FirstOrDefault()).ToList();

            var uniqueCities = uniqueProfiles.Select(p => p.City)
                .Where(c => c is not null)
                .GroupBy(c => c.Id)
                .Select(x => x.FirstOrDefault()).ToList();

            await ImportCoordinatesAsync(uniqueCities);
            uniqueProfiles.ForEach(p => p.City = null);

            await _repository.InsertManyAsync(uniqueCities);
            await _repository.InsertManyAsync(uniqueProfiles);

            return uniqueProfiles.Count;
        }

        public async Task ImportCoordinatesAsync(List<City> cities)
        {
            //cities.Remove(cities.FirstOrDefault(c => c.Name == "Kourou"));
            //cities.Remove(cities.FirstOrDefault(c => c.Name == "Локоть"));

            try
            {
                for (int i = 0; i < cities.Count; i++)
                {
                    if (cities[i].Name != "Kourou" && cities[i].Name != "Локоть")
                    {
                        var geocoderRu = new YandexGeocoder
                        {
                            SearchQuery = cities[i].Name,
                            Apikey = Options.ApiSecret,
                            Results = 1,
                            LanguageCode = LanguageCode.ru_RU
                        };


                        var response = await geocoderRu.GetResultsAsync();
                        if (response.Count >= 1)
                        {
                            cities[i].Lattitude = response.First().Point.Latitude;
                            cities[i].Longitude = response.First().Point.Longitude;
                            cities[i].CountryName = response.First().CountryName;

                        }

                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
    }
}
