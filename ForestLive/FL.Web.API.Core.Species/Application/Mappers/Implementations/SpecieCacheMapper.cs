using FL.Pereza.Helpers.Standard.Extensions;
using FL.Web.API.Core.Species.Application.Mappers.Contracts;
using FL.Web.API.Core.Species.Domain.Dto;

using System.Web;

namespace FL.Web.API.Core.Species.Application.Mappers.Implementations
{
    public class SpecieCacheMapper : ISpecieCacheMapper
    {

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
                    UrlSpecie = source.UrlSpecie,
                    NormalizeName = source.Name.NormalizeName(),
                    NameComplete = $"{HttpUtility.HtmlDecode(source.Name.ToLower())} ({HttpUtility.HtmlDecode(source.ScienceName.ToLower())})",
                };
            }

            return result;
        }
    }
}
