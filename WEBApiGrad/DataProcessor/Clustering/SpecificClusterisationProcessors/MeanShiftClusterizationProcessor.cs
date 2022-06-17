using System.Collections.Generic;
using System.Linq;
using Accord.MachineLearning;
using WEBApiGrad.HttpClients;
using IntermediateModels;
using Accord.Statistics.Distributions.DensityKernels;

namespace WEBApiGrad.DataProcessor.Clustering;

public class MeanShiftClusterizationProcessor : ISpecificClusterizationProcessor
{

    public List<ClusterInt> PerformClustering(List<ProfileInt> profileInts)
    {
        Accord.Math.Random.Generator.Seed = 0;

        var meanShift = new MeanShift()
        {
            Kernel = new UniformKernel(),
            Bandwidth = 12.0
        };

        var observations = new double[profileInts.Count][];
        int count = 0;
        foreach (var profileDTO in profileInts)
        {
            var obs = new double[] { (double)profileDTO.Age, (double)profileDTO.City.Lattitude, (double)profileDTO.City.Longitude, (double)profileDTO.Sex };
            observations[count] = obs;
            observations.Append(obs);
            count++;
        }
            
        var clusters = meanShift.Learn(observations);
        int[] labels = clusters.Decide(observations);
        var clusterInts = new List<ClusterInt>();

        var nClusters = clusters.Count;

        for (var i = 0; i < nClusters; i++)
        {
            var clusterDTO = new ClusterInt();
            clusterDTO.ClusterNumber = i;
            clusterDTO.ClusterAlgorithm = "MeanShift";
            clusterDTO.Profiles = new List<ProfileInt>();
            clusterInts.Add(clusterDTO);

        }

        for (var j = 0; j < labels.Length; j++)
        {
            var c = new ClusterInt()
            {
                ClusterAlgorithm = clusterInts[labels[j]].ClusterAlgorithm,
                ClusterNumber = clusterInts[labels[j]].ClusterNumber
            };
            profileInts[j].Clusters = new List<ClusterInt>() { c };
            profileInts[j].Clusters.Add(c);
        }

        for (var i = 0; i < nClusters; i++)
        {
            clusterInts[i].Profiles.AddRange(profileInts.Where(p => p.Clusters.FirstOrDefault(c => c.ClusterNumber == clusterInts[i].ClusterNumber
                && c.ClusterAlgorithm == clusterInts[i].ClusterAlgorithm) != null));
        }

        return clusterInts;
    }
}
