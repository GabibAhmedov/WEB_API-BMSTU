using DataBaseAPI.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VkNet.Abstractions;
using VkNet.Enums.Filters;
using VkNet.Model.RequestParams;

namespace DataBaseAPI.Importers;

public class VkImportSearch : IVkImportAlternatives<Profile>
{

    private readonly ushort _startingAge = 22;
    private readonly ushort _finalAge = 45;
    private readonly IVkApi _source;

    public VkImportSearch(IVkApi source)
    {
        _source = source;
    }

    public async Task<List<Profile>> ImportDataAlternativelyAsync()
    {
        var profiles = new List<Profile>();
        var cities = new List<City>();
        int pCount = 0;
        int cCount = 0;
        for (ushort i = _startingAge; i < _finalAge; i++)
        {
            var users = await _source.Users.SearchAsync(new UserSearchParams
            {
                Fields = ProfileFields.All,
                Count = 1000,
                Offset = 0,
                AgeFrom = i,
                AgeTo = i,
                University = 250
            });

            for (int j = 1; j < users.Count; j++)
            {
                var profile = new Profile();
                var city = new City();

                profile.Name = users[j].FirstName;
                profile.Surname = users[j].LastName;
                profile.Sex = (int)users[j].Sex;
                profile.Age = i;
                profile.VKId = users[j].Id;

                if (users[j].City is not null && users[j].City.Title is not "" && users[j].City is not null)
                {
                    profile.City = city;
                    profile.City.Id = (int)users[j].City.Id;
                    profile.City.Name = users[j].City.Title;
                    profile.CityId = (int)users[j].City.Id;
                    cCount++;
                }

                if (users[j].Country is not null)
                {
                    profile.Country = users[j].Country.Title;
                    profile.CountryId = (int?)users[j].Country.Id;
                }
                if (users[j].HomeTown is not null)
                {
                    profile.Hometown = users[j].HomeTown;
                }

                if (users[j].Education is not null)
                {
                    profile.Graduation = users[j].Education.Graduation;
                }
                profiles.Add(profile);
                pCount++;
            }

        }
        return profiles;
    }
}

