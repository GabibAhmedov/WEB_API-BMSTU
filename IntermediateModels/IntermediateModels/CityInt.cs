using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IntermediateModels;

public class CityInt
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string CountryName { get; set; }

    public double? Longitude { get; set; }

    public double? Lattitude { get; set; }

    public int Profiles { get; set; }
}

