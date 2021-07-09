using FL.WebAPI.Core.Birds.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Birds.Domain.Repository
{
    public interface ISpecieRestRepository
    {
        Task<IEnumerable<SpecieResponse>> GetAllSpecies();

        Task<IEnumerable<SpecieResponse>> GetAllSpeciesByLanguageId(Guid languageId);
    }
}
