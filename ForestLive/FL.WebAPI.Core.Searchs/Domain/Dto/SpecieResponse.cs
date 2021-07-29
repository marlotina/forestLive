using System;

namespace FL.WebAPI.Core.Searchs.Domain.Dto
{
    public class SpecieResponse
    {
        public Guid SpecieId { get; set; }

        public string Name { get; set; }

        public string ScienceName { get; set; }

        public string NameComplete { get; set; }

        public string UrlSpecie { get; set; }
    }
}
