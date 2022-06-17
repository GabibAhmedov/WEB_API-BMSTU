using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using Newtonsoft.Json;

namespace DataBaseAPI.Data.Models
{
    public class Plot
    {

        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        public string Name { get;set; }

        [Column(TypeName = "jsonb")]
        public string Data { get; set; }

    }
}
