using DataBaseAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Converters;

namespace DataBaseAPI.Controllers
{
    public class CityController : Controller
    {
        private readonly ICityRepository _cityRepository;

        public CityController(ICityRepository cityRepository)
        {
            _cityRepository = cityRepository;
        }

        [HttpGet("cities")]
        public async Task<IActionResult> GetCities()
        {
            IActionResult response;
            try
            {
                var cities = await _cityRepository.GetCitiesAsync();
                var cityDTOs= cities.Select(p => CityConverter.ConvertToDTO(p)).ToList();
                response = Ok(cityDTOs);
            }
            catch (Exception ex)
            {
                response = StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            return response;
        }
    }
}
