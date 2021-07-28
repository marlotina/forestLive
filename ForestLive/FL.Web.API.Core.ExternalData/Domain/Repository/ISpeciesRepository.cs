using FL.Web.API.Core.ExternalData.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.Web.API.Core.ExternalData.Domain.Repository
{
    public interface ISpeciesRepository
    {
        Task<List<SpecieItem>> GetSpeciesByLanguage(Guid languageId);

        Task<List<SpecieItem>> GetAllSpecies();
    }
}
