using DataBaseAPI.Data;
using System.Collections.Generic;
using System.Threading.Tasks;
using ImmigrationDTOs;
using Microsoft.EntityFrameworkCore;
using IntermediateModels;

namespace DataBaseAPI.Repositories
{
    public class PlotRepository:BaseRepository, IPlotRepository
    {

        public PlotRepository(ImmigrationDbContext context) :base(context)
        {
               
        }

        public async void Dispose()
        {
            await _context.DisposeAsync();
        }

        public async Task<List<PlotInt>> GetPlotsAsync()
        {
            var plotList = new List<PlotInt>();
            var plots = await _context.Plots
                .AsNoTracking()
                .ToListAsync();
            if (plots == null)
                return null;
            foreach (var plot in plots)
            {
                plotList.Add(new PlotInt()
                {
                    Id = plot.Id,
                    Name = plot.Name,
                    Data = plot.Data
                });
            }

            return plotList;
        }

        public async override Task ClearAsync() 
        {
            await _context.Database.ExecuteSqlRawAsync("TRUNCATE TABLE \"GraduatesImmigration\".public.\"Plots\" RESTART IDENTITY CASCADE");
        }
    }
}
