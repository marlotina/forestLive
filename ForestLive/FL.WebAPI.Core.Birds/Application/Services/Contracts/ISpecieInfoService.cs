using FL.WebAPI.Core.Birds.Domain.Dto;
using System;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Birds.Application.Services.Contracts
{
    public interface ISpecieInfoService
    {
        Task<SpecieResponse> GetSpecieById(Guid specieId, string languageId);
    }
}
