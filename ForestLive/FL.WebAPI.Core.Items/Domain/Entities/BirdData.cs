using Microsoft.Azure.Cosmos.Spatial;
using System;

namespace FL.WebAPI.Core.Items.Domain.Entities
{
    public class BirdData
    {
        public Guid Id { get; set; }

        public string Label { get; set; }

        public string Type { get; set; }

        public Point Location { get; set; }

        public BirdDataProperty Properties { get; set; }
    }
}
