using ImmigrationDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using WEBApiGrad.HttpClients;
using IntermediateModels;


namespace WEBApiGrad.WebMediator;
public interface IWebMediator
{

    public Task<List<ProfileInt>> GetProfilesAsync();

    public Task<List<CityInt>> GetCitiesAsync();

    public Task<List<ClusterInt>> GetClustersAsync();

    public Task<List<ClusterDTO>> PostClustersAsync(List<ClusterInt> clusterInts);

    public Task<string> PutProfilesAsync();
    public Task<List<PlotDTO>> PostPlotsAsync(List<PlotInt> PlotInt);

    public Task<List<PlotInt>> GetPlotsAsync();

}

