using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Threading;
using ImmigrationDTOs;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Json;
using WEBApiGrad.DataProcessor;
using Newtonsoft.Json;
using System.Text;
using System.Web.Http;
using System.Net.Http.Headers;
using IntermediateModels;
using Converters;

namespace WEBApiGrad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlotController : Controller
    {

        private readonly HttpClient _httpClient;
        private readonly IDataProcessor<List<PlotInt>> _dataProcessor;
        public PlotController(IHttpClientFactory clientFactory,
            IDataProcessor<List<PlotInt>> dataProcessor)
        {
            _httpClient = clientFactory.CreateClient("microserviceClient");
            _dataProcessor = dataProcessor;

        }
        [HttpPost]
        [Route("data")]
        public async Task<IActionResult> RetrievePlotData()
        {
            try
            {
                var tokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(500000));
                var result = await _httpClient.GetAsync("plots");
                var stringResult = await result.Content.ReadAsStringAsync();
                var plotInts = JsonConvert.DeserializeObject<List<PlotInt>>(stringResult);
                IActionResult altResult;
                if (plotInts.Count == 0)
                {
                    altResult = await RefreshPlotData();
                    return altResult;
                }
                result.EnsureSuccessStatusCode();
                var desResult = JsonConvert.DeserializeObject(await result.Content.ReadAsStringAsync());
                return Ok(plotInts.Select(p => PlotConverter.ConvertToDTO(p)).ToList());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPost]
        [Route("data/cities")]
        public async Task<IActionResult> RetrieveCityData()
        {
            try
            {
                var tokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(500000));
                var result = await _httpClient.GetAsync("plots");
                var stringResult = await result.Content.ReadAsStringAsync();
                var plotInts = JsonConvert.DeserializeObject<List<PlotInt>>(stringResult);
                plotInts = plotInts.Where(p => p.Name == "ImmigrantsPerCity").ToList();
                IActionResult altResult;
                if (plotInts.Count == 0)
                {
                    altResult = await RefreshPlotData();
                    return altResult;
                }
                result.EnsureSuccessStatusCode();
                var desResult = JsonConvert.DeserializeObject(await result.Content.ReadAsStringAsync());
                return Ok(PlotConverter.ConvertToDTO(plotInts[0]));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        [HttpPost]
        [Route("data/years")]
        public async Task<IActionResult> RetrieveYearData()
        {
            try
            {
                var tokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(500000));
                var result = await _httpClient.GetAsync("plots");
                var stringResult = await result.Content.ReadAsStringAsync();
                var plotInts = JsonConvert.DeserializeObject<List<PlotInt>>(stringResult);
                plotInts = plotInts.Where(p => p.Name == "GraduationYear").ToList();
                IActionResult altResult;
                if (plotInts.Count == 0)
                {
                    altResult = await RefreshPlotData();
                    return altResult;
                }
                result.EnsureSuccessStatusCode();
                var desResult = JsonConvert.DeserializeObject(await result.Content.ReadAsStringAsync());
                return Ok(PlotConverter.ConvertToDTO(plotInts[0]));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        [Route("data")]
        public async Task<IActionResult> RefreshPlotData()
        {
            try
            {
                var plotInts = await _dataProcessor.PrepareDataAsync();
                var tokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(500000));
                var stringPlots = JsonConvert.SerializeObject(plotInts);
                var content = new StringContent(stringPlots, Encoding.UTF8, "application/json");           
                var result = await _httpClient.PostAsync("plots", content, tokenSource.Token);
                result.EnsureSuccessStatusCode();
                return Ok(plotInts.Select(p => PlotConverter.ConvertToDTO(p)).ToList());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
