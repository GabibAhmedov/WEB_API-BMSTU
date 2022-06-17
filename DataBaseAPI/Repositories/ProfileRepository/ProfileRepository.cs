using DataBaseAPI.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DataBaseAPI.Data;
using Microsoft.EntityFrameworkCore;
using ImmigrationDTOs;
using IntermediateModels;

namespace DataBaseAPI.Repositories
{
    public class ProfileRepository : BaseRepository, IProfileRepository
    {

    
        public ProfileRepository(ImmigrationDbContext context) : base(context)
        {

        }
        public void DeleteProfile(int profileId)
        {
            throw new NotImplementedException();
        }

        public async Task InsertProfileAsync(Profile profile)
        {
            await _context.Profiles.AddAsync(profile);
        }

        public void UpdateCustomer(Profile profile)
        {
            throw new NotImplementedException();
        }

        public async void Dispose()
        {
            await _context.DisposeAsync();
        }

        public async Task<List<ProfileInt>> GetProfilesAsync()
        {
            var profileDTOs = new List<ProfileInt>();
            var profiles = await _context.Profiles
                .AsNoTracking()
                .Include(p => p.Clusters)
                .Include(p => p.City)
                .ToListAsync();
            if (profiles == null)
                throw new Exception($"could not get profiles from database");
            foreach (var profile in profiles)
            {
                profileDTOs.Add(new ProfileInt()
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
                    City = profile.City!=null?GetCityIntFromCity(profile.City):null,
                });
            }
            return profileDTOs;

        }
    }
}
