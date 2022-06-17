using DataBaseAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using ImmigrationDTOs;
using DataBaseAPI.Data.Models;
using Newtonsoft.Json;
using Converters;

namespace DataBaseAPI.Controllers
{
    public class PlotController : Controller
    {
        private readonly IPlotRepository _plotRepository;

        public PlotController(IPlotRepository plotRepository)
        {
            _plotRepository = plotRepository;
        }

        [HttpGet("plots")]
        public async Task<IActionResult> GetPlotsAsync()
        {
            IActionResult response;
            try
            {
                var plots = await _plotRepository.GetPlotsAsync();
                //var jsonResponse = JsonConvert.SerializeObject(plots);
                var plotDTOs = plots.Select(p => PlotConverter.ConvertToDTO(p)).ToList();
                response = Ok(plotDTOs);

            }
            catch (Exception ex)
            {
                response = StatusCode(StatusCodes.Status500InternalServerError,ex.Message);
            }
            return response;
        }


        [HttpPost("plots")]
        public async Task<IActionResult> LoadPlotsAsync([FromBody] List<PlotDTO> content)
        {
            IActionResult response;
        //    var plotDTOs = JsonSerializer.Deserialize<List<PlotDTO>>(content);
            var plotList = new List<Plot>();
            foreach(var plotDTO in content)
            {
                plotList.Add(new Plot()
                {
                    Id = plotDTO.Id,
                    Name = plotDTO.Name,
                    Data = plotDTO.Data
                });
            }
            try
            {
                await _plotRepository.ClearAsync();
                await _plotRepository.InsertManyAsync(plotList);
                response = Ok();

            }
            catch (Exception ex)
            {
                response = StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            return response;
        }
    }
}
