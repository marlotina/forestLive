using FL.WebAPI.Core.Searchs.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Searchs.Domain.Repository
{
    public interface ISpecieRestRepository
    {
        Task<IEnumerable<SpecieInfoResponse>> GetAllSpecies();

        Task<IEnumerable<SpecieResponse>> GetAllSpeciesByLanguageId(Guid languageId);
    }
}
