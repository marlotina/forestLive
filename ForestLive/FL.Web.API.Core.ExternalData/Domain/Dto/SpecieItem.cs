using System;

namespace FL.Web.API.Core.ExternalData.Domain.Dto
{
    public class SpecieItem
    {
        public Guid SpecieId { get; set; }

        public string Name { get; set; }

        public string ScienceName { get; set; }

        public string UrlSpecie { get; set; }
        
    }
}
