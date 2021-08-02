using FL.WebAPI.Core.Searchs.Domain.Dto;
using System;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Searchs.Application.Services.Contracts
{
    public interface ISpecieInfoService
    {
        Task<SpecieResponse> GetSpecieById(Guid specieId, Guid languageId);
    }
}
