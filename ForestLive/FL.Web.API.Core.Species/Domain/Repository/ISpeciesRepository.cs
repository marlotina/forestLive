using FL.Web.API.Core.Species.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.Web.API.Core.Species.Domain.Repository
{
    public interface ISpeciesRepository
    {
        Task<List<SpecieItem>> GetSpeciesByLanguage(Guid languageId);

        Task<List<SpecieItem>> GetAllSpecies();
    }
}
