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
    public class PlotController : Controller
    {
        private readonly IWebMediator _webMediator;
        private readonly IDataProcessor<List<PlotInt>> _dataProcessor;
        public PlotController(
            IDataProcessor<List<PlotInt>> dataProcessor,
            IWebMediator webMediator)
        {
            _dataProcessor = dataProcessor;
            _webMediator = webMediator;
        }
        [HttpPost]
        [Route("data")]
        public async Task<IActionResult> RetrievePlotData()
        {
            try
            {
                var plotInts = await _webMediator.GetPlotsAsync();
                if (plotInts.Count == 0)
                {
                    return Ok(await RefreshPlotData());
                }
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
                var plotInts = await _webMediator.GetPlotsAsync();
                plotInts = plotInts.Where(p => p.Name == "ImmigrantsPerCity").ToList();
                if (plotInts.Count == 0)
                {
                    return Ok(await RefreshPlotData());
                }
                return Ok(plotInts.Select(p => PlotConverter.ConvertToDTO(p)).ToList());
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
                var plotInts = await _webMediator.GetPlotsAsync();
                plotInts = plotInts.Where(p => p.Name == "GraduationYear").ToList();
                if (plotInts.Count == 0)
                {
                    return Ok(await RefreshPlotData());
                }
                return Ok(plotInts.Select(p => PlotConverter.ConvertToDTO(p)).ToList());
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
                return Ok(await _webMediator.PostPlotsAsync(plotInts));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
