using System;

namespace FL.Web.API.Core.Species.Domain.Dto
{
    public class SpeciesCacheItem
    {
        public Guid SpecieId { get; set; }

        public string Name { get; set; }

        public string NormalizeName { get; set; }

        public string ScienceName { get; set; }

        public string NormalizeScienceName { get; set; }

        public string NameComplete { get; set; }

        public string UrlSpecie { get; set; }
    }
}
