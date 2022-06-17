using ImmigrationDTOs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEBApiGrad.HttpClients;
using IntermediateModels;


namespace WEBApiGrad.DataProcessor
{
    public class ImigrantsPerCityProcessor : ISpecificPlotProcessor
    {

        public PlotInt PrepareSpecificPlotAsync(List<ProfileInt> profileDTOs, List<CityInt> cityDTOs)
        {
            var ImmigrantsPerCity = new Dictionary<string, int>();
            var ImmigrantsList = new List<ImmigrantPerCity>();
            foreach (var line in profileDTOs.Where(p => p.CountryId != 1 && p.City is not null && p.CountryId is not null)
                .GroupBy(p => p.City.Name)
                .Select(group => new
                {
                    Name = group.Key,
                    Count = group.Count()
                })
                .OrderBy(p => p.Count))
            {
                var g = new ImmigrantPerCity() { city = line.Name, amount = line.Count };
                ImmigrantsPerCity.Add(line.Name, line.Count);
                ImmigrantsList.Add(g);

            }
            ImmigrantsList = ImmigrantsList.OrderByDescending(i=>i.amount).ToList();
            return new PlotInt() { Name = "ImmigrantsPerCity", Data = JsonConvert.SerializeObject(ImmigrantsList) };
        }
    }
    public class ImmigrantPerCity
    {
        public string city { get; set; }
        public int amount { get; set; }
    }
}

