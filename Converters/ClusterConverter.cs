using System;
using IntermediateModels;
using ImmigrationDTOs;
using System.Linq;

namespace Converters;

public class ClusterConverter
{
    public static ClusterDTO ConvertToDTO(ClusterInt cluster)
    {
        return new ClusterDTO()
        {
            Id = cluster.Id,
            ProfileCount = cluster.ProfileCount,
            ClusterAlgorithm = cluster.ClusterAlgorithm,
            ClusterNumber = cluster.ClusterNumber,
            Profiles = cluster.Profiles.Select(p => ProfileConverter.ConvertToDTO(p)).ToList()
        };
    }

    public static ClusterInt ConvertToIntermediate(ClusterDTO DTO)
    {
        return new ClusterInt()
        {
            Id = DTO.Id,
            ProfileCount = DTO.ProfileCount,
            ClusterAlgorithm = DTO.ClusterAlgorithm,
            ClusterNumber = DTO.ClusterNumber,
            Profiles = DTO.Profiles.Select(p => ProfileConverter.ConvertToIntermediate(p)).ToList()
        };
    }
}

