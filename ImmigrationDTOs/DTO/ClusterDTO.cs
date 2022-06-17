using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImmigrationDTOs
{
    public class ClusterDTO
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("profileCount")]
        public int ProfileCount { get; set; }

        [JsonProperty("clusterNumber")]
        public int ClusterNumber { get; set; }
        [JsonProperty("clusterAlgorithm")]
        public string ClusterAlgorithm { get; set; }

        public List<ProfileDTO> Profiles { get; set; }
    }
}
