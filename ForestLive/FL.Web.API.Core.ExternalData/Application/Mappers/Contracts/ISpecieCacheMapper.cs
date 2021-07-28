using FL.Web.API.Core.ExternalData.Domain.Dto;

namespace FL.Web.API.Core.ExternalData.Application.Mappers.Contracts
{
    public interface ISpecieCacheMapper
    {
        SpeciesCacheItem Convert(SpecieItem source);
    }
}
