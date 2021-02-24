using FL.WebAPI.Core.Birds.Api.Mappers.v1.Contracts;
using FL.WebAPI.Core.Birds.Api.Models.v1.Response;
using FL.WebAPI.Core.Birds.Domain.Model;
using System.Web;

namespace FL.WebAPI.Core.Birds.Api.Mappers.v1.Implementations
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
                    NameComplete = $"{HttpUtility.HtmlDecode(source.Name)} ({HttpUtility.HtmlDecode(source.ScienceName)})",
                };
            }

            return result;
        }
    }
}
