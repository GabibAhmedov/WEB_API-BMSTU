using System;
using IntermediateModels;
using ImmigrationDTOs;
using System.Linq;

namespace Converters
{
    public class ProfileConverter
    {
        public static ProfileDTO ConvertToDTO(ProfileInt profile)
        {

            return new ProfileDTO() 
            {
                Id = profile.Id,
                Name = profile.Name,
                Surname = profile.Surname,
                Age = profile.Age,
                University = profile.University,
                Graduation = profile.Graduation,
                Sex = profile.Sex,
                Hometown = profile.Hometown,
                CountryId = profile.CountryId,
                Country = profile.Country,
                CityId = profile.CityId,
                City = profile.City != null ? CityConverter.ConvertToDTO(profile.City) : null,
                //Clusters = profile.Clusters.Select(p=>ClusterConverter.ConvertToDTO(p)).ToList()
            };

        }


        public static ProfileInt ConvertToIntermediate(ProfileDTO profile)
        {
            return new ProfileInt()
            {
                Id = profile.Id,
                Name = profile.Name,
                Surname = profile.Surname,
                Age = profile.Age,
                University = profile.University,
                Graduation = profile.Graduation,
                Sex = profile.Sex,
                Hometown = profile.Hometown,
                CountryId = profile.CountryId,
                Country = profile.Country,
                CityId = profile.CityId,
                City = profile.City != null ? CityConverter.ConvertToIntermediate(profile.City) : null,
                //Clusters = profile.Clusters.Select(p => ClusterConverter.ConvertToIntermediate(p)).ToList(),
            };
        }
    }
}
