using System;

namespace FL.WebAPI.Core.Birds.Domain.Model
{
    public class SpecieItem
    {
        public Guid SpecieId { get; set; }

        public string Name { get; set; }

        public string ScienceName { get; set; }
    }
}
