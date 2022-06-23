using ImmigrationDTOs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WEBApiGrad.DataProcessor;
using IntermediateModels;
using WEBApiGrad.DataProcessor.Clustering;
using WEBApiGrad.WebMediator;

namespace WEBApiGrad;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddMvc();
        services.AddControllers();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "WEBApiGrad", Version = "v1" });
        });
        services.AddHttpClient("microserviceClient", c =>
        {
            c.BaseAddress = new System.Uri("https://localhost:5001");
            c.Timeout = TimeSpan.FromSeconds(500000);
        });

        services.AddHttpClient("clusterizationClient", c =>
        {
            c.BaseAddress = new System.Uri("http://127.0.0.1:8000");
            c.Timeout = TimeSpan.FromSeconds(500000);
        });

        services.AddTransient<IWebMediator, WebMediator.WebMediator>();

        services.AddTransient<IDataProcessor<List<CityInt>>, CityDataProcessor>();
        services.AddTransient<IDataProcessor<List<PlotInt>>, PlotDataProcessor>();
        services.AddTransient<IDataProcessor<List<ClusterInt>>, ClusterDataProcessor>();

        services.AddTransient<ISpecificPlotProcessor, GraduationYearProcessor>();
        services.AddTransient<ISpecificPlotProcessor, ImigrantsPerCityProcessor>();
        services.AddTransient<ISpecificPlotProcessor, SexDistributionProcessor>();

        services.AddTransient<ISpecificClusterizationProcessor, KMeansClusterizationProcessor>();
        services.AddTransient<ISpecificClusterizationProcessor, GMMClusterizationProcessor>();
        services.AddTransient<ISpecificClusterizationProcessor, MeanShiftClusterizationProcessor>();

    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WEBApiGrad v1"));
        }

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}

