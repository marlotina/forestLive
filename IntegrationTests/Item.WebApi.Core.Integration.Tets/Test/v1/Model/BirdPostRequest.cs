using System;
using System.Collections.Generic;
using System.Text;

namespace Item.WebApi.Core.Integration.Tets.Test.v1.Model
{
    public class BirdPostRequest
    {
        //public Guid Id { get; set; }
        public string ImageData { get; set; }

        //public string ImageName { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public Guid UserId { get; set; }

        public string UserName { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public string SpecieName { get; set; }

        public Guid SpecieId { get; set; }

        public string[] Labels { get; set; }

        //public string Type { get; set; }
    }
}
