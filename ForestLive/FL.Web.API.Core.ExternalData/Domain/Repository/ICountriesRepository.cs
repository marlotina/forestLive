using FL.Web.API.Core.ExternalData.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.Web.API.Core.ExternalData.Domain.Repository
{
    public interface ICountriesRepository
    {
        Task<List<CountryItem>> GetCountryByLanguage(Guid languageId);
    }
}
