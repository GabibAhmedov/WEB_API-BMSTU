using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Text.Json;
using IntermediateModels;
using WEBApiGrad.WebMediator;

namespace WEBApiGrad.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProfilesController : Controller
    {
        private readonly IWebMediator _webMediator;

        public ProfilesController(IHttpClientFactory clientFactory,
            IWebMediator webMediator)
        {
            _webMediator = webMediator;
        }
        [HttpPut]
        [Route("data")]
        public async Task<IActionResult> RefreshProfileDataAsycn()
        {
            try
            {
                //var tokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(500000));
                //var result = await _httpClient.PutAsync("api/Seed",null, tokenSource.Token);
                //result.EnsureSuccessStatusCode();
                return Ok(await _webMediator.PutProfilesAsync());
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
