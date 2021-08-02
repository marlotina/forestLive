using FL.Web.API.Core.ExternalData.Api.Models.v1.Response;
using FL.Web.API.Core.ExternalData.Domain.Dto;

namespace FL.Web.API.Core.ExternalData.Api.Mappers.v1.Contracts
{
    public interface IAutocompleteMapper
    {
        AutocompleteResponse Convert (SpeciesCacheItem source);

        AutocompleteResponse ConvertInfo(SpecieItem source);
    }
}
