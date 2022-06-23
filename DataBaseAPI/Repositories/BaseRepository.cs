using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DataBaseAPI.Data;
using ImmigrationDTOs;
using DataBaseAPI.Data.Models;
using IntermediateModels;

namespace DataBaseAPI.Repositories
{
    public class BaseRepository : IRepository
    {
        protected readonly ImmigrationDbContext _context;

        public BaseRepository(ImmigrationDbContext context)
        {
            _context = context;
        }

        protected CityInt GetCityIntFromCity(City city)
        {
            return new CityInt()
            {
                Id = city.Id,
                Name = city.Name,
                CountryName = city.CountryName,
                Longitude = city.Longitude,
                Lattitude = city.Lattitude,
            };
        }
        protected async Task<ClusterInt> GetClusterIntFromCluster(Cluster cluster)
        {
            return new ClusterInt()
            {
                Id = cluster.Id,
                ProfileCount=cluster.ProfileCount,
                Profiles = await GetProfilesIntFromEntityWithProlieCollection(cluster)
            };
        }

        public virtual async Task InsertManyAsync<T>(ICollection<T> elements) where T : class
        {
            await _context.AddRangeAsync(elements);
            await _context.SaveChangesAsync();
        }

        protected void Save()
        {
            _context.SaveChanges();
        }

        public async virtual Task ClearAsync()
        {
           await _context.Database.ExecuteSqlRawAsync("TRUNCATE TABLE \"GraduatesImmigration\".public.\"Profiles\" RESTART IDENTITY CASCADE");
           await _context.Database.ExecuteSqlRawAsync("TRUNCATE TABLE \"GraduatesImmigration\".public.\"Cities\" RESTART IDENTITY CASCADE");
        }
        protected async Task<List<ProfileInt>> GetProfilesIntFromEntityWithProlieCollection(EntityWithProfileCollection entityWithProfileCollection)
        {
            List<Profile> profiles;
            if (entityWithProfileCollection is City)
            {
                profiles = await _context.Profiles.Where(p => p.CityId == entityWithProfileCollection.Id).Include(p => p.City).ToListAsync();
            }
            else
            {
                profiles = await _context.Profiles.Where(p =>p.Clusters.FirstOrDefault(c=>c.Id == entityWithProfileCollection.Id) != null)
                    .Include(p=>p.City).ToListAsync();
            }


            return profiles
                .OrderBy(c => c.Id)
                .Select(profile => new ProfileInt()
                {
                    Id = profile.Id,
                    Name = profile.Name,
                    Surname = profile.Surname,
                    Age = profile.Age,
                    University = profile.University,
                    Graduation = profile.Graduation,
                    Sex = profile.Sex,
                    Hometown = profile.Hometown,
                    CountryId = profile.CountryId,
                    Country = profile.Country,
                    CityId = profile.CityId,
                    City = profile.City is not null ? GetCityIntFromCity(profile.City) : null,
                })
                .ToList();

        }

        public List<Profile> GetProfilesFromProfileInts(List<ProfileInt> profileInts)
        {

            return profileInts
                .OrderBy(c => c.Id)
                .Select(profile => new Profile()
                {
                    Id = profile.Id,
                    Name = profile.Name,
                    Surname = profile.Surname,
                    Age = profile.Age,
                    University = profile.University,
                    Graduation = profile.Graduation,
                    Sex = profile.Sex,
                    Hometown = profile.Hometown,
                    CountryId = profile.CountryId,
                    Country = profile.Country,
                    CityId = profile.CityId,
                })
                .ToList();
        }
    }
}