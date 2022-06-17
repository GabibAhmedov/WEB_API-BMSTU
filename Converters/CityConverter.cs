using System;
using IntermediateModels;
using ImmigrationDTOs;

namespace Converters;

    public class CityConverter
{
    public static CityDTO ConvertToDTO(CityInt city)
    {

        return new CityDTO()
        {
            Id = city.Id,
            Name = city.Name,
            CountryName = city.CountryName,
            Longitude = city.Longitude,
            Lattitude = city.Lattitude,
            Profiles = city.Profiles
        };

    }

    public static CityInt ConvertToIntermediate(CityDTO city)
    {
        return new CityInt()
        {
            Id = city.Id,
            Name = city.Name,
            CountryName = city.CountryName,
            Longitude = city.Longitude,
            Lattitude = city.Lattitude,
            Profiles = city.Profiles
        };
    }
}

