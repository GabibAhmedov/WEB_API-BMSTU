using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImmigrationDTOs
{
    public class PlotDTO
    {

        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("Name")]
        public string Name { get; set; }
        [JsonProperty("Data")]

        public string Data { get; set; }

    }
}
