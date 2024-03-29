﻿using FL.Pereza.Helpers.Standard.Language;
using FL.WebAPI.Core.Account.Mappers.v1.Contracts;
using FL.WebAPI.Core.Account.Models.v1.Request;
using FL.WebAPI.Core.Account.Models.v1.Response;

namespace FL.WebAPI.Core.Account.Mappers.v1.Implementation
{
    public class RegisterMapper : IRegisterMapper
    {
        public Domain.Entities.User Convert(RegisterRequest source)
        {
            var result = default(Domain.Entities.User);
            if (source != null)
            {
                result = new Domain.Entities.User()
                {
                    Email = source.Email,
                    UserName = source.UserName,
                    LanguageId = LanguageHelper.GetLanguageByCode(source.LanguageCode)
                };
            }
            return result;
        }

        public RegisterResponse Convert(Domain.Entities.User source)
        {
            var result = default(RegisterResponse);
            if (source != null)
            {
                result = new RegisterResponse
                {
                    Email = source.Email,
                    UserName = source.UserName,
                    Id = source.Id
                };
            }
            return result;
        }
    }
}
