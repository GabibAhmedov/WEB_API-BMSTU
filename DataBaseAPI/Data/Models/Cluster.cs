using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DataBaseAPI.Data.Models
{
    public class Cluster: EntityWithProfileCollection
    {
        public int ProfileCount { get; set; }

        public int ClusterNumber { get; set; }

        public string ClusterAlgorithm { get; set; }
    }

}
