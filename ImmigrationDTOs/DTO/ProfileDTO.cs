using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ImmigrationDTOs
{
    public class ProfileDTO
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("surname")]
        public string Surname { get; set; }

        [JsonProperty("age")]
        public int Age { get; set; }

        [JsonProperty("university")]
        public string University { get; set; }

        [JsonProperty("graduation")]
        public int? Graduation { get; set; }

        [JsonProperty("sex")]
        public int Sex { get; set; }

        [JsonProperty("hometown")]
        public string Hometown { get; set; }

        [JsonProperty("countryId")]
        public int? CountryId { get; set; }

        [JsonProperty("counry")]
        public string Country { get; set; }

        [JsonProperty("cityId")]
        public int? CityId { get; set; }

        [JsonProperty("city")]
        public CityDTO City { get; set; }

        [JsonProperty("clusterId")]
        public int? ClusterId { get; set; }

        [JsonProperty("cluster")]
        public List<ClusterDTO> Clusters { get; set; }

    }
}
