using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Threading;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Json;
using WEBApiGrad.DataProcessor;
using Newtonsoft.Json;
using System.Text;
using System.Web.Http;
using System.Net.Http.Headers;
using IntermediateModels;
using Converters;
using ImmigrationDTOs;

namespace WEBApiGrad.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MapController : Controller
{
    private readonly HttpClient _httpClient;
    private readonly IDataProcessor<List<CityInt>> _dataProcessor;
    public MapController(IHttpClientFactory clientFactory,
        IDataProcessor<List<CityInt>> dataProcessor)
    {
        _httpClient = clientFactory.CreateClient("microserviceClient");
        _dataProcessor = dataProcessor;

    }

    [HttpPost]
    [Route("data")]
    public async Task<IActionResult> RetrieveMapData()
    {
        try
        {
            var cityInts = await _dataProcessor.PrepareDataAsync();
            if (cityInts.Count == 0)
            {
                await RefreshMapData();

                return Ok(await _dataProcessor.PrepareDataAsync());

            }
            return Ok(cityInts.Select(p => CityConverter.ConvertToDTO(p)).ToList());
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPut]
    [Route("data")]
    public async Task<IActionResult> RefreshMapData()
    {
        try
        {
            var tokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(500000));
            // var content = JsonContent.Create(plotDTOs,List<PlotDTO>);


            var result = await _httpClient.PutAsync("api/Seed", null);
            result.EnsureSuccessStatusCode();
            return Ok((await _dataProcessor.PrepareDataAsync()).Select(p => CityConverter.ConvertToDTO(p)).ToList());
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

}

