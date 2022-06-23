using ImmigrationDTOs;
using System.Collections.Generic;
using System.Net.Http;
using Converters;
using IntermediateModels;
using Newtonsoft.Json;
using WEBApiGrad.HttpClients;
using System.Threading.Tasks;
using System.Linq;
using System.Threading;
using System.Text;
using System;

namespace WEBApiGrad.WebMediator;

public class WebMediator : IWebMediator
{
    private readonly HttpClient _httpClient;
    public WebMediator(IHttpClientFactory httpClient)
    {
        _httpClient=httpClient.CreateClient(ClientCategory.MicroserviceClient.Category);
    }


    public async Task<List<CityInt>> GetCitiesAsync()
    {
        var citiesResult = await _httpClient.GetAsync("cities");
        var cityInts = JsonConvert.DeserializeObject<List<CityInt>>(await citiesResult.Content.ReadAsStringAsync());
        citiesResult.EnsureSuccessStatusCode();
        return cityInts;
    }

    public async Task<List<ProfileInt>> GetProfilesAsync()
    {
        var profilesResult = await _httpClient.GetAsync("profiles");
        profilesResult.EnsureSuccessStatusCode();
        var profileInts = JsonConvert.DeserializeObject<List<ProfileInt>>(await profilesResult.Content.ReadAsStringAsync());
        return profileInts;
    }

    public async Task<List<ClusterInt>> GetClustersAsync()
    {
        var result = await _httpClient.GetAsync("clusters");
        var stringResult = await result.Content.ReadAsStringAsync();
        var clusterInts = JsonConvert.DeserializeObject<List<ClusterInt>>(stringResult);
        result.EnsureSuccessStatusCode();
        return clusterInts;
    }

    public async Task<List<PlotDTO>> PostPlotsAsync(List<PlotInt> plotInts)
    {
        var tokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(500000));
        var stringPlots = JsonConvert.SerializeObject(plotInts);
        var content = new StringContent(stringPlots, Encoding.UTF8, "application/json");
        var result = await _httpClient.PostAsync("plots", content, tokenSource.Token);
        result.EnsureSuccessStatusCode();
        var plotDTOs = plotInts.Select(p => PlotConverter.ConvertToDTO(p)).ToList();
        return plotDTOs;
    }

    public async Task<List<ClusterDTO>> PostClustersAsync(List<ClusterInt> clusterInts)
    {
        var tokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(500000));
        var clusterDTOs = clusterInts.Select(c => ClusterConverter.ConvertToDTO(c)).ToList();
        var stringPlots = JsonConvert.SerializeObject(clusterInts);
        var content = new StringContent(stringPlots, Encoding.UTF8, "application/json");
        var result = await _httpClient.PostAsync("clusters", content, tokenSource.Token);
        result.EnsureSuccessStatusCode();
        return clusterDTOs;
    }

    public async Task<string> PutProfilesAsync()
    {
        var tokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(500000));
        var result = await _httpClient.PutAsync("api/Seed", null, tokenSource.Token);
        result.EnsureSuccessStatusCode();
        var stringResult = await result.Content.ReadAsStringAsync();
        return stringResult;
    }

    public async Task<List<PlotInt>> GetPlotsAsync()
    {
        var tokenSource = new CancellationTokenSource(TimeSpan.FromSeconds(500000));
        var result = await _httpClient.GetAsync("plots");
        var stringResult = await result.Content.ReadAsStringAsync();
        var plotInts = JsonConvert.DeserializeObject<List<PlotInt>>(stringResult);
        result.EnsureSuccessStatusCode();
        return plotInts;
    }
}
