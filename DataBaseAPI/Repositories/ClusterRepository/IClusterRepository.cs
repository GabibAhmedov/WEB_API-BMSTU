using ImmigrationDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IntermediateModels;

namespace DataBaseAPI.Repositories
{
    public interface IClusterRepository:IRepository,IDisposable
    {
        Task<List<ClusterInt>> GetClustersAsync();
    }
}
