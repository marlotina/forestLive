﻿using FL.WebAPI.Core.Users.Api.Mappers.v1.Contracts;
using FL.WebAPI.Core.Users.Api.Models.v1.Response;
using FL.WebAPI.Core.Users.Domain.Entities;
using FL.WebAPI.Core.Users.Models.v1.Response;

namespace FL.WebAPI.Core.Users.Api.Mappers.v1.Implementation
{
    public class UserPageMapper : IUserPageMapper
    {
        public UserPageResponse Convert(UserInfo source)
        {
            var result = default(UserPageResponse);
            if (source != null)
            {
                result = new UserPageResponse()
                {
                    Id = source.Id,
                    Name = source.Name,
                    Surname = source.Surname,
                    UrlWebSite = source.UrlWebSite,
                    IsCompany = source.IsCompany,
                    Description = source.Description,
                    Photo = source.Photo,
                    Location = source.Location,
                    UserSystemId = source.UserSystemId,
                    FacebookUrl = source.FacebookUrl,
                    InstagramUrl = source.InstagramUrl,
                    TwitterUrl = source.TwitterUrl,
                    LinkedlinUrl = source.LinkedlinUrl
                };
            }
            return result;
        }

        public UserListSiteResponse ConvertList(UserInfo source)
        {
            var result = default(UserListSiteResponse);
            if (source != null)
            {
                result = new UserListSiteResponse()
                {
                    Id = source.Id,
                    UrlWebSite = source.UrlWebSite,
                    IsCompany = source.IsCompany,
                    RegistrationDate = source.RegistrationDate.ToString("dd/MM/yyyy"),
                    LanguageId = source.LanguageId,
                    Description = source.Description,
                    Photo = source.Photo,
                    Location = source.Location,
                    UserName = source.Id,
                    FacebookUrl = source.FacebookUrl,
                    InstagramUrl = source.InstagramUrl,
                    TwitterUrl = source.TwitterUrl,
                    LinkedlinUrl = source.LinkedlinUrl,
                    FollowerCount = source.FollowerCount,
                    HasFollow = false
                };
            }

            return result;
        }

        public UserMapResponse ConvertUserMap(UserInfo source)
        {
            var result = default(UserMapResponse);
            if (source != null)
            {
                result = new UserMapResponse()
                {
                    Latitude = source.Latitude,
                    Longitude = source.Longitude
                };
            }

            return result;
        }
    }
}
