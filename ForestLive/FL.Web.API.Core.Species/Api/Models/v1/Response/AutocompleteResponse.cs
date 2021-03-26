using System;

namespace FL.Web.API.Core.Species.Api.Models.v1.Response
{
    public class AutocompleteResponse
    {
        public Guid SpecieId { get; set; }

        public string NameComplete { get; set; }

        public string NormalizeNameComplete { get; set; }
    }
}
