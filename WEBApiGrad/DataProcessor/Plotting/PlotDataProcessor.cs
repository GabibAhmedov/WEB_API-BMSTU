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
    public class PlotDataProcessor : IDataProcessor<List<PlotInt>>
    {
        private readonly IEnumerable<ISpecificPlotProcessor> _specificPlotProcessors;
        private readonly IWebMediator _webMediator;
        public PlotDataProcessor(IEnumerable<ISpecificPlotProcessor> specificPlotProcessors,
            IWebMediator webMediator)
        {
            _specificPlotProcessors = specificPlotProcessors;
            _webMediator = webMediator;
        }

        public async Task<List<PlotInt>> PrepareDataAsync()
        {
            var profileInts = await _webMediator.GetProfilesAsync();
            var cityInts = await _webMediator.GetCitiesAsync();
            var plotInts = new List<PlotInt>();

            var count = 0;
            foreach(var plotProcessor in _specificPlotProcessors)
            {
                
                var plotDTO = plotProcessor.PrepareSpecificPlotAsync(profileInts, cityInts);
                plotDTO.Id = count+1;
                plotInts.Add(plotDTO);
                count++;
            }

            return plotInts;
        }

    }
}
