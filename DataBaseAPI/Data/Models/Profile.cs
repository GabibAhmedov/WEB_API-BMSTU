using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace DataBaseAPI.Data.Models
{
	[Table("Profiles")]
	public class Profile
	{

		public Profile()
		{

		}

		[Key]
		[Required]
		public int Id { get; set; }		

		public long VKId { get; set; }

		public string Name { get; set; }

		public string Surname { get; set; }

		public int Age { get; set; }

		public string University { get; set; }

		public int? Graduation { get; set; }

		public int Sex { get; set; }

		public string Hometown { get; set; }

		public int? CountryId { get; set; }

		public string Country { get; set; }

		[ForeignKey("City")]
		public int? CityId { get; set; }
		public City City { get; set; }

		public List<Cluster> Clusters { get; set; } = new List<Cluster>();


	}
}
