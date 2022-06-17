using ImmigrationDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IntermediateModels;

namespace DataBaseAPI.Repositories
{
    public interface ICityRepository : IRepository, IDisposable
    {

        public Task<List<CityInt>> GetCitiesAsync();

    }
}
