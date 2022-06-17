using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntermediateModels;

public class ProfileInt
{
    public int Id { get; set; }
    public string Name { get; set; }

    public string Surname { get; set; }

    public int Age { get; set; }

    public string University { get; set; }

    public int? Graduation { get; set; }

    public int Sex { get; set; }

    public string Hometown { get; set; }

    public int? CountryId { get; set; }

    public string Country { get; set; }

    public int? CityId { get; set; }

    public CityInt City { get; set; }

    public List<ClusterInt> Clusters { get; set; } 

}

