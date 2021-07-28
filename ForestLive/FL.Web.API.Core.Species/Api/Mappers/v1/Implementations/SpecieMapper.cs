using FL.Web.API.Core.Species.Api.Mappers.v1.Contracts;
using FL.Web.API.Core.Species.Api.Models.v1.Response;
using FL.Web.API.Core.Species.Domain.Dto;
using System.Web;

namespace FL.Web.API.Core.Species.Api.Mappers.v1.Implementations
{
    public class SpecieMapper : ISpecieMapper
    {
        public SpecieResponse Convert(SpecieItem source)
        {
            var result = default(SpecieResponse);
            if (source != null)
            {
                result = new SpecieResponse()
                {
                    SpecieId = source.SpecieId,
                    ScienceName = source.ScienceName.ToLower(),
                    UrlSpecie = source.UrlSpecie
                };
            }

            return result;
        }

        public SpecieInfoReponse ConvertInfo(SpecieItem source)
        {
            var result = default(SpecieInfoReponse);
            if (source != null)
            {
                result = new SpecieInfoReponse()
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
