using System;

namespace FL.Web.API.Core.Species.Api.Models.v1.Response
{
    public class SpecieInfoReponse
    {
        public Guid SpecieId { get; set; }

        public string Name { get; set; }

        public string ScienceName { get; set; }

        public string NameComplete { get; set; }

        public string UrlSpecie { get; set; }
    }
}
