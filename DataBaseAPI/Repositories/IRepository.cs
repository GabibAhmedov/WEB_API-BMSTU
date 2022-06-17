using DataBaseAPI.Data.Models;
using ImmigrationDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IntermediateModels;

namespace DataBaseAPI.Repositories
{
    public interface IRepository
    {

        Task InsertManyAsync<T>(ICollection<T> elements) where T : class;

        public Task ClearAsync();

        public List<Profile> GetProfilesFromProfileInts(List<ProfileInt> profileInts);

    }
}
