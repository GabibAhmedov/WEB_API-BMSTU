using DataBaseAPI.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VkNet.Abstractions;
using VkNet.Enums.Filters;
using VkNet.Model.RequestParams;

namespace DataBaseAPI.Importers;

public class VkImportGroupMembers : IVkImportAlternatives<Profile>
{
    private readonly ushort _startingAge = 22;
    private readonly ushort _finalAge = 45;
    private readonly List<string> _groupId = new List<string>() { "40427933", "59336460", "113929722" };
    private readonly IVkApi _source;

    public VkImportGroupMembers(IVkApi source)
    {
        _source = source;
    }


    public async Task<List<Profile>> ImportDataAlternativelyAsync()
    {
        var profiles = new List<Profile>();
        var cities = new List<City>();
        int pCount = 0;
        int cCount = 0;
        var offset = 0;
        var listUsers = new List<VkNet.Model.User>();
        foreach (var groupid in _groupId)
        {
            do
            {
                var users = await _source.Groups.GetMembersAsync(new GroupsGetMembersParams
                {
                    GroupId = groupid,
                    Fields = UsersFields.All,
                    Count = 1000,
                    Offset = offset,

                });
                int age = 0;
                listUsers = users.Where(u => u.BirthDate is not null
                && u.BirthDate != ""
                && u.BirthdayVisibility == VkNet.Enums.BirthdayVisibility.Full
                && (age = (DateTime.Now.Year - DateTime.ParseExact(u.BirthDate, "d.m.yyyy", System.Globalization.CultureInfo.InvariantCulture).Year)) > _startingAge 
                && age < _finalAge)
                .ToList();

                for (int j = 1; j < listUsers.Count; j++)
                {
                    var profile = new Profile();
                    var city = new City();

                    profile.Name = listUsers[j].FirstName;
                    profile.Surname = listUsers[j].LastName;
                    profile.Sex = (int)listUsers[j].Sex;
                    profile.Age = DateTime.Now.Year - DateTime.ParseExact(listUsers[j].BirthDate, "d.m.yyyy", System.Globalization.CultureInfo.InvariantCulture).Year;
                    profile.VKId = listUsers[j].Id;

                    if (listUsers[j].City is not null && listUsers[j].City.Title is not "" && listUsers[j].City is not null)
                    {
                        profile.City = city;
                        profile.City.Id = (int)listUsers[j].City.Id;
                        profile.City.Name = listUsers[j].City.Title;
                        profile.CityId = (int)listUsers[j].City.Id;
                        cCount++;
                    }

                    if (listUsers[j].Country is not null)
                    {
                        profile.Country = listUsers[j].Country.Title;
                        profile.CountryId = (int?)listUsers[j].Country.Id;
                    }
                    if (listUsers[j].HomeTown is not null)
                    {
                        profile.Hometown = listUsers[j].HomeTown;
                    }

                    if (listUsers[j].Education is not null)
                    {
                        profile.Graduation = listUsers[j].Education.Graduation;
                    }
                    profiles.Add(profile);
                    pCount++;
                }
                offset += 1000;

            } while (listUsers.Count > 0);
            offset = 0;
            pCount = 0;
            cCount = 0;

        }

        return profiles;
    }

}


