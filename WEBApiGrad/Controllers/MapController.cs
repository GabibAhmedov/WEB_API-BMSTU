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
using WEBApiGrad.WebMediator;

namespace WEBApiGrad.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MapController : Controller
{
    private readonly HttpClient _httpClient;
    private readonly IDataProcessor<List<CityInt>> _dataProcessor;
    private readonly IWebMediator _webMediator;

    public MapController(IHttpClientFactory clientFactory,
        IDataProcessor<List<CityInt>> dataProcessor,
        IWebMediator webMediator)
    {
        _httpClient = clientFactory.CreateClient("microserviceClient");
        _dataProcessor = dataProcessor;
        _webMediator = webMediator;

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
                return Ok( await RefreshMapData());
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
            await _webMediator.PutProfilesAsync();
            return Ok((await _dataProcessor.PrepareDataAsync()).Select(p => CityConverter.ConvertToDTO(p)).ToList());
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

}

