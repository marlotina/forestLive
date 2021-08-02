using FL.Web.API.Core.ExternalData.Api.Mappers.v1.Contracts;
using FL.Web.API.Core.ExternalData.Api.Models.v1.Response;
using FL.Web.API.Core.ExternalData.Domain.Dto;
using System.Web;

namespace FL.Web.API.Core.ExternalData.Api.Mappers.v1.Implementations
{
    public class AutocompleteMapper : IAutocompleteMapper
    {
        public AutocompleteResponse Convert(SpeciesCacheItem source)
        {
            var result = default(AutocompleteResponse);
            if (source != null)
            {
                result = new AutocompleteResponse()
                {
                    SpecieId = source.SpecieId,
                    ScienceName = source.ScienceName,
                    Name = source.Name,
                    UrlSpecie = source.UrlSpecie,
                    NameComplete = $"{HttpUtility.HtmlDecode(source.Name.ToLower())} ({HttpUtility.HtmlDecode(source.ScienceName.ToLower())})",
                };
            }

            return result;
        }

        public AutocompleteResponse ConvertInfo(SpecieItem source)
        {
            var result = default(AutocompleteResponse);
            if (source != null)
            {
                result = new AutocompleteResponse()
                {
                    SpecieId = source.SpecieId,
                    ScienceName = source.ScienceName,
                    Name = source.Name,
                    UrlSpecie = source.UrlSpecie,
                    NameComplete = $"{HttpUtility.HtmlDecode(source.Name.ToLower())} ({HttpUtility.HtmlDecode(source.ScienceName.ToLower())})",
                };
            }

            return result;
        }
    }
}
