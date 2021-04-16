using FL.WebAPI.Core.Users.Domain.Entities;
using FL.WebAPI.Core.Users.Mappers.v1.Contracts;
using FL.WebAPI.Core.Users.Models.v1.Request;
using FL.WebAPI.Core.Users.Models.v1.Response;

namespace FL.WebAPI.Core.Users.Mappers.v1.Implementation
{
    public class UserMapper : IUserMapper
    {
        public Domain.Entities.User Convert(UserRequest source)
        {
            var result = default(Domain.Entities.User);
            if (source != null)
            {
                result = new Domain.Entities.User()
                {
                    Id = source.Id,
                    Email = source.Email,
                    UserName = source.UserName,
                };
            }
            return result;
        }

        public UserResponse Convert(UserInfo source)
        {
            var result = default(UserResponse);
            if (source != null)
            {
                result = new UserResponse()
                {
                    Id = source.Id,
                    Name = source.Name,
                    Surname = source.Surname,
                    UrlWebSite = source.UrlWebSite,
                    IsCompany = source.IsCompany,
                    RegistrationDate = source.RegistrationDate,
                    LastModification = source.LastModification,
                    LanguageId = source.LanguageId,
                    Description = source.Description,
                    Photo = source.Photo,
                    Location = source.Location,
                    UserName = source.UserId,
                    FacebookUrl = source.FacebookUrl,
                    InstagramUrl = source.InstagramUrl,
                    TwitterUrl = source.TwitterUrl,
                    LinkedlinUrl = source.LinkedlinUrl
                };
            }
            return result;
        }

        public UserInfoResponse ConvertUserInfo(Domain.Entities.User source)
        {
            var result = default(UserInfoResponse);
            if (source != null)
            {
                result = new UserInfoResponse()
                {
                    Id = source.Id,
                    RegistrationDate = source.RegistrationDate,
                    UserName = source.UserName
                };
            }

            return result;
        }
    }
}
