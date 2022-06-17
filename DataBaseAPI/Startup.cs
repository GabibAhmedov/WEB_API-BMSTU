using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Text.Json;
using DataBaseAPI.Data;
using Npgsql;
using DataBaseAPI.Repositories;
using DataBaseAPI.Importers;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Model;
using VkNet.Model.RequestParams;
using DataBaseAPI.Options;
using VkNet.Abstractions;
using DataBaseAPI.Data.Models;

namespace DataBaseAPI
{
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
            services.AddDbContext<ImmigrationDbContext>(options => 
            {
                options.UseNpgsql( Configuration.GetConnectionString("DefaultConnection"));
                options.EnableSensitiveDataLogging();
            });

            services.AddTransient<IProfileRepository, ProfileRepository>();
            services.AddTransient<ICityRepository, CityRepository>();
            services.AddTransient<IPlotRepository, PlotRepository>();
            services.AddTransient<IClusterRepository, ClusterRepository>();
            services.Configure<VkApiOptions>(options =>
            {
                options.ApiSecret = Configuration["ExternalProviders:VkApi:ApiKey"];
            });

            services.Configure<YandexGeocoderOptions>(options =>
            {
                options.ApiSecret = Configuration["ExternalProviders:YandexGeocoderOptions:ApiKey"];
            });

            services.AddTransient<IVkApi>(sp=>
            {
                var VkApi = new VkApi();
                VkApi.Authorize(new ApiAuthParams
                {
                    AccessToken = Configuration["ExternalProviders:VkApi:ApiKey"]
                });

                return VkApi;
            }
               
            );

            services.AddTransient<IDataImporter<Profile>, VkImporter>();
            services.AddTransient<IVkImportAlternatives<Profile>, VkImportSearch>();
            services.AddTransient<IVkImportAlternatives<Profile>, VkImportGroupMembers>();
            services.AddControllers();
            services.AddSwaggerGen();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "DataBaseAPI v1"));
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
}
