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
    public class GraduationYearProcessor : ISpecificPlotProcessor
    {
        public PlotInt PrepareSpecificPlotAsync(List<ProfileInt> profileDTOs, List<CityInt> cityDTOs)
        {
            var GraduationYear = new Dictionary<int, int>();
            var gradYearList = new List<GraduationYear>();

            foreach (var line in profileDTOs.Where(p => p.CountryId != 1 && p.City is not null && p.CountryId is not null && p.Graduation is not null)
                .GroupBy(p => p.Graduation)
                .Select(group => new
                {
                    Name = group.Key,
                    Count = group.Count()
                })
                .OrderBy(p => p.Count))
            {
                var g = new GraduationYear() { year = (int)line.Name, amount = line.Count };
                GraduationYear.Add((int)line.Name, line.Count);
                gradYearList.Add(g);      
            }
            gradYearList = gradYearList.OrderBy(g => g.year).ToList();
            return new PlotInt() { Name = "GraduationYear", Data = JsonConvert.SerializeObject(gradYearList)};
        }
    }

    public class GraduationYear
    {
        public int year { get; set; }
        public int amount { get; set; }
    }
}
