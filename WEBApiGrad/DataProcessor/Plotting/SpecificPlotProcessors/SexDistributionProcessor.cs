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
    public class SexDistributionProcessor : ISpecificPlotProcessor
    {
        public PlotInt PrepareSpecificPlotAsync(List<ProfileInt> profileDTOs, List<CityInt> cityDTOs)
        {
            var SexDestribution = new Dictionary<string, int>();

            foreach (var line in profileDTOs.Where(p => p.CountryId != 1 
            && p.City is not null 
            && p.CountryId is not null 
            && (p.Sex == 1 || p.Sex == 2 ))
            .GroupBy(p => p.Sex)
            .Select(group => new
            {
                Name = group.Key,
                Count = group.Count()
            })
            .OrderBy(p => p.Count))
            {
                SexDestribution.Add(line.Name == 1 ? "Female" : "Male", line.Count);
            }

            return new PlotInt() { Name = "SexDestribution", Data = JsonConvert.SerializeObject(SexDestribution) };
        }

    }
}
