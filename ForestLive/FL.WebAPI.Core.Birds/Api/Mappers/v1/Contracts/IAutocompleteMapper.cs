using FL.WebAPI.Core.Birds.Api.Models.v1.Response;
using FL.WebAPI.Core.Birds.Domain.Model;

namespace FL.WebAPI.Core.Birds.Api.Mappers.v1.Contracts
{
    public interface IAutocompleteMapper
    {
        AutocompleteResponse Convert (SpecieItem source);
    }
}
