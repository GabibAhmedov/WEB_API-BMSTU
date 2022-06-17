using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DataBaseAPI.Data.Models
{
    public class EntityWithProfileCollection
    {
        [Key]
        [Required]
        public int Id { get; set; }

        public List<Profile> Profiles { get; set; } = new List<Profile>();
    }
}
