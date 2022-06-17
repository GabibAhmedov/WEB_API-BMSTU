using DataBaseAPI.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataBaseAPI.Data.Models;
using Converters;
using IntermediateModels;
using ImmigrationDTOs;

namespace DataBaseAPI.Controllers;

public class ClusterController : Controller
{

    private readonly IClusterRepository _clusterRepository;

    public ClusterController(IClusterRepository clusterRepository)
    {
        _clusterRepository = clusterRepository;
    }


    [HttpGet("clusters")]
    public async Task<IActionResult> GetClustersAsync()
    {
        IActionResult response;
        try
        {
            var clusters = await _clusterRepository.GetClustersAsync();
            //var jsonResponse = JsonConvert.SerializeObject(plots);
            var clusterDTOs = clusters.Select(c => ClusterConverter.ConvertToDTO(c));
            response = Ok(clusterDTOs);

        }
        catch (Exception ex)
        {
            response = StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
        return response;
    }


    [HttpPost("clusters")]
    public async Task<IActionResult> LoadClustersAsync([FromBody] List<ClusterDTO> content)
    {
        IActionResult response;
        //    var plotDTOs = JsonSerializer.Deserialize<List<PlotDTO>>(content);
        var clusterList = new List<Cluster>();
        try
        {
            foreach (var clusterDTO in content)
            {
                clusterList.Add(new Cluster()
                {
                    Id = clusterDTO.Id,
                    ProfileCount = clusterDTO.Profiles.Count,
                    Profiles = _clusterRepository.GetProfilesFromProfileInts(clusterDTO.Profiles
                    .Select(p => ProfileConverter.ConvertToIntermediate(p))
                    .ToList()),
                    ClusterNumber = clusterDTO.ClusterNumber,
                    ClusterAlgorithm = clusterDTO.ClusterAlgorithm 

                });
            }
            await _clusterRepository.ClearAsync();
            await _clusterRepository.InsertManyAsync(clusterList);
            response = Ok(content);

        }
        catch (Exception ex)
        {
            response = StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
        return response;
    }
}

