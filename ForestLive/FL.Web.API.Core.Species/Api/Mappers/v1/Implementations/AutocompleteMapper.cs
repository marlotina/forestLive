using FL.Pereza.Helpers.Standard.Extensions;
using FL.Web.API.Core.Species.Api.Mappers.v1.Contracts;
using FL.Web.API.Core.Species.Api.Models.v1.Response;
using FL.Web.API.Core.Species.Domain.Dto;
using FL.Web.API.Core.Species.Domain.Model;
using System.Web;

namespace FL.Web.API.Core.Species.Api.Mappers.v1.Implementations
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
                    NameComplete = $"{HttpUtility.HtmlDecode(source.Name.ToLower())} ({HttpUtility.HtmlDecode(source.ScienceName.ToLower())})",
                };
            }

            return result;
        }

        public SpeciesCacheItem Convert(SpecieItem source)
        {
            var result = default(SpeciesCacheItem);
            if (source != null)
            {
                result = new SpeciesCacheItem()
                {
                    SpecieId = source.SpecieId,
                    ScienceName = source.ScienceName.ToLower(),
                    NormalizeScienceName = source.ScienceName.NormalizeName(),
                    Name = source.Name,
                    NormalizeName = source.Name.NormalizeName(),
                    NameComplete = $"{HttpUtility.HtmlDecode(source.Name.ToLower())} ({HttpUtility.HtmlDecode(source.ScienceName.ToLower())})",
                };
            }

            return result;
        }
    }
}
