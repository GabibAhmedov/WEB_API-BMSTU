using ImmigrationDTOs;
using DataBaseAPI.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IntermediateModels;

namespace DataBaseAPI.Repositories
{
    public interface IProfileRepository : IDisposable, IRepository
    {
        Task InsertProfileAsync(Profile profile);
        void DeleteProfile(int profileId);
        void UpdateCustomer(Profile profile);
        Task<List<ProfileInt>> GetProfilesAsync();
    }
}
