using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DataBaseAPI.Data;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using DataBaseAPI.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Security;
using System.Net.Http;
using System.Net.Http.Headers;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Model;
using VkNet.Model.RequestParams;
using System.Collections.Generic;
using DataBaseAPI.Repositories;
using Microsoft.Extensions.Options;
using DataBaseAPI.Options;
using DataBaseAPI.Importers;
using VkNet.Abstractions;
using Microsoft.AspNetCore.Http;

namespace DataBaseAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeedController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        private readonly IEnumerable<IDataImporter<Profile>> _dataImporters;
        public VkApiOptions Options { get; set; }
        public SeedController(
            IProfileRepository repository,
            IWebHostEnvironment env,
            IEnumerable<IDataImporter<Profile>> dataImporters,
            IOptions<VkApiOptions> options)
        {
            _env = env;
            _dataImporters = dataImporters;
            Options = options.Value;
        }

        [HttpPut]
        public async Task<ActionResult> Import()
        {
            try
            {
                if (!_env.IsDevelopment())
                    throw new SecurityException("Not allowed");
                var VKapi = new VkApi();

                VKapi.Authorize(new ApiAuthParams
                {
                    AccessToken = Options.ApiSecret
                }); ;

                int count = 0;

                foreach (var importer in _dataImporters)
                {
                    count += await importer.ImportDataAsync();
                }
                return Ok(new JsonResult(new
                {
                    Profiles = count,
                }));
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
