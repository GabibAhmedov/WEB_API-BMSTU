using ImmigrationDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IntermediateModels;

namespace DataBaseAPI.Repositories
{
    public interface IPlotRepository:IRepository,IDisposable
    {

        Task<List<PlotInt>> GetPlotsAsync();
    }
}
