using FL.Web.API.Core.ExternalData.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.Web.API.Core.ExternalData.Application.Services.Contracts
{
    public interface ICountriesService
    {
        Task<IEnumerable<CountryItem>> GetCountryByLanguage(Guid languageId);
    }
}
