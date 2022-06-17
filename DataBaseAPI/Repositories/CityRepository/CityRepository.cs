using DataBaseAPI.Data;
using ImmigrationDTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IntermediateModels;

namespace DataBaseAPI.Repositories
{
    public class CityRepository:BaseRepository,ICityRepository
    {

        public CityRepository(ImmigrationDbContext context) : base(context)
        {

        }

        public async void Dispose()
        {
            await _context.DisposeAsync();
        }

        public async Task<List<CityInt>> GetCitiesAsync()
        {
            var cityList = new List<CityInt>();
            var cities = await _context.Cities
                .AsNoTracking()
                .Include(c=>c.Profiles)
                .ToListAsync();
            if (cities is null)
                throw new Exception($"could not get profiles from database");
            foreach (var city in cities)
            {
                cityList.Add(new CityInt()
                {
                    Id = city.Id,
                    Name = city.Name,
                    CountryName = city.CountryName,
                    Longitude = city.Longitude,
                    Lattitude = city.Lattitude,
                    Profiles = (await GetProfilesIntFromEntityWithProlieCollection(city)).Count
                }) ; 
            }
            return cityList;
        }
    }
}
