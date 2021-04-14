using Newtonsoft.Json;
using System;

namespace FL.WebAPI.Core.Birds.Domain.Dto
{
    public class PostHomeDto
    {
        [JsonProperty(PropertyName = "postId")]
        public Guid PostId { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "specieName")]
        public string SpecieName { get; set; }

        [JsonProperty(PropertyName = "specieId")]
        public Guid? SpecieId { get; set; }

        [JsonProperty(PropertyName = "imageUrl")]
        public string ImageUrl { get; set; }

        [JsonProperty(PropertyName = "altImage")]
        public string AltImage { get; set; }

        [JsonProperty(PropertyName = "userId")]
        public string UserId { get; set; }

        [JsonProperty(PropertyName = "observationDate")]
        public DateTime? ObservationDate { get; set; }
    }
}
