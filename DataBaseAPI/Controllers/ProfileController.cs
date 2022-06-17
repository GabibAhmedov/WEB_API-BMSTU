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
    public class ProfileController : Controller
    {
        private readonly IProfileRepository _profileRepository;

        public ProfileController(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        [HttpGet("profiles")]
        public async Task<IActionResult> GetProfiles()
        {
            IActionResult response;
            try
            {
                var profiles = await _profileRepository.GetProfilesAsync();
                var profileDTOs = profiles.Select(p => ProfileConverter.ConvertToDTO(p)).ToList();
                response = Ok(profileDTOs);
            }
            catch (Exception ex)
            {
                response = StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
            return response;
        }
    }
}
