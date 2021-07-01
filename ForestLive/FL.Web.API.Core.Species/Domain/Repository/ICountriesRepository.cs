using FL.Web.API.Core.Species.Domain.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.Web.API.Core.Species.Domain.Repository
{
    public interface ICountriesRepository
    {
        Task<List<CountryItem>> GetCountryByLanguage(Guid languageId);
    }
}
