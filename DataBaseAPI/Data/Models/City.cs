using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DataBaseAPI.Data.Models
{
    public class City:EntityWithProfileCollection
    {



        public string Name { get; set; }

        public string CountryName { get; set; }

        public double? Longitude { get; set; }

        public double? Lattitude { get; set; }

    }
}
