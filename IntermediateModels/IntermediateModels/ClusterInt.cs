using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntermediateModels;

public class ClusterInt
{
    public int Id { get; set; }

    public int ProfileCount { get; set; }

    public int ClusterNumber { get; set; }

    public string ClusterAlgorithm { get; set; }

    public List<ProfileInt> Profiles { get; set; }
}

