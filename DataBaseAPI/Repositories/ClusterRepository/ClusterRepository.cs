using DataBaseAPI.Data;
using DataBaseAPI.Data.Models;
using ImmigrationDTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IntermediateModels;

namespace DataBaseAPI.Repositories
{
    public class ClusterRepository : BaseRepository, IClusterRepository
    {

        public ClusterRepository(ImmigrationDbContext context) : base(context)
        {

        }

        public async void Dispose()
        {
            await _context.DisposeAsync();
        }

        public async Task<List<ClusterInt>> GetClustersAsync()
        {
            var clusterList = new List<ClusterInt>();
            var clusters = await _context.Clusters
                .AsNoTracking()
                .Include(c => c.Profiles)
                .ToListAsync();
            if (clusters is null)
                throw new Exception($"could not get profiles from database");
            foreach (var cluster in clusters)
            {
                clusterList.Add(new ClusterInt()
                {
                    Id = cluster.Id,
                    ProfileCount = cluster.Profiles.Count,
                    Profiles = await GetProfilesIntFromEntityWithProlieCollection(cluster),
                    ClusterAlgorithm = cluster.ClusterAlgorithm,
                    ClusterNumber = cluster.ClusterNumber
                });
            }
            return clusterList;
        }

        public async override Task ClearAsync()
        {
            await _context.Profiles.ForEachAsync(p => 
            { 
                p.Clusters = null;
            }
            );
            _context.Clusters.RemoveRange(_context.Clusters);
            await _context.SaveChangesAsync();
        }

        public override async Task InsertManyAsync<T>(ICollection<T> elements) where T : class
        {
            var listElements = (List<Cluster>)elements;

            var listProfiles = new List<ICollection<Profile>>();
            foreach (var cluster in listElements)
            {
                listProfiles.Add(cluster.Profiles);
                cluster.Profiles = null;
            }

            //await _context.AddRangeAsync(listElements);
            //await _context.AddRangeAsync(listElements[1]);
            //await _context.SaveChangesAsync();

            var listElements2 = new List<Cluster>((List<Cluster>)elements);

            for (var i = 0;i< listElements2.Count;i++)
            { 
                foreach(var profile in listProfiles[i])
                {
                    var p = (await _context.Profiles.FirstOrDefaultAsync(p => p.Id == profile.Id));
                    if (p.Clusters is null)
                    {
                        p.Clusters = new List<Cluster>();
                        p.Clusters.Add(listElements2[i]);
                    }
                    else
                    {
                        p.Clusters.Add(listElements2[i]);
                    }
                  
                }
                
            }

            await _context.SaveChangesAsync();
        }
    }
}
