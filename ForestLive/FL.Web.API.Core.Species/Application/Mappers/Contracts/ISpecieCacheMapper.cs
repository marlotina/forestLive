using FL.Web.API.Core.Species.Domain.Dto;

namespace FL.Web.API.Core.Species.Application.Mappers.Contracts
{
    public interface ISpecieCacheMapper
    {
        SpeciesCacheItem Convert(SpecieItem source);
    }
}
