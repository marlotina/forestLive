using System;

namespace FL.WebAPI.Core.Birds.Api.Models.v1.Response
{
    public class AutocompleteResponse
    {
        public Guid SpecieId { get; set; }

        public string NameComplete { get; set; }
    }
}
