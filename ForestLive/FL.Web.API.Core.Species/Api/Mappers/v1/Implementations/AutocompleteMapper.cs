using FL.Pereza.Helpers.Standard.Extensions;
using FL.Web.API.Core.Species.Api.Mappers.v1.Contracts;
using FL.Web.API.Core.Species.Api.Models.v1.Response;
using FL.Web.API.Core.Species.Domain.Model;
using System.Web;

namespace FL.Web.API.Core.Species.Api.Mappers.v1.Implementations
{
    public class AutocompleteMapper : IAutocompleteMapper
    {
        public AutocompleteResponse Convert(SpecieItem source)
        {
            var result = default(AutocompleteResponse);
            if (source != null)
            {
                result = new AutocompleteResponse()
                {
                    SpecieId = source.SpecieId,
                    NameComplete = $"{HttpUtility.HtmlDecode(source.Name.ToLower())} ({HttpUtility.HtmlDecode(source.ScienceName.ToLower())})",
                };


                result.NormalizeNameComplete = result.NameComplete.NormalizeName();
            }

            return result;
        }
    }
}
