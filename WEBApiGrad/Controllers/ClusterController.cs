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
using WEBApiGrad.WebMediator;

namespace WEBApiGrad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClustersController : Controller
    {

        private readonly HttpClient _httpClient;
        private readonly IDataProcessor<List<ClusterInt>> _dataProcessor;
        private readonly IWebMediator _webMediator;
        public ClustersController(IHttpClientFactory clientFactory,
            IDataProcessor<List<ClusterInt>> dataProcessor,
            IWebMediator webMediator)
        {
            _httpClient = clientFactory.CreateClient("microserviceClient");
            _dataProcessor = dataProcessor;
            _webMediator = webMediator;

        }

        [HttpPost]
        [Route("data")]
        public async Task<IActionResult> RetrieveClusterData()
        {
            try
            {
                var clusterInts = await _webMediator.GetClustersAsync();
                if (clusterInts.Count == 0 || clusterInts[0].ProfileCount == 0)
                {
                    return Ok( await RefreshClusterData());
                }

                return Ok(clusterInts.Select(p => ClusterConverter.ConvertToDTO(p)).ToList());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpPut]
        [Route("data")]
        public async Task<IActionResult> RefreshClusterData()
        {
            try
            {
                var clusterInts = await _dataProcessor.PrepareDataAsync();
                return Ok(await _webMediator.PostClustersAsync(clusterInts));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
