using System;

namespace FL.Web.API.Core.ExternalData.Api.Models.v1.Response
{
    public class AutocompleteResponse
    {
        public Guid SpecieId { get; set; }

        public string Name { get; set; }

        public string ScienceName { get; set; }

        public string NameComplete { get; set; }

        public string UrlSpecie { get; set; }
    }
}
