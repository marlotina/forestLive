using Microsoft.Azure.Cosmos.Spatial;
using Newtonsoft.Json;
using System;

namespace FL.WebAPI.Core.User.Posts.Domain.Entities
{
    public class BirdPost
    {
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }

        [JsonProperty(PropertyName = "postId")]
        public Guid PostId { get; set; }

        [JsonProperty(PropertyName = "type")]
        public string Type { get; set; }

        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }

        [JsonProperty(PropertyName = "location")]
        public Point Location { get; set; }

        [JsonProperty(PropertyName = "specieName")]
        public string SpecieName { get; set; }

        [JsonProperty(PropertyName = "specieId")]
        public Guid? SpecieId { get; set; }

        [JsonProperty(PropertyName = "imageUrl")]
        public string ImageUrl { get; set; }

        [JsonProperty(PropertyName = "altImage")]
        public string AltImage { get; set; }

        [JsonProperty(PropertyName = "labels")]
        public string[] Labels { get; set; }

        [JsonProperty(PropertyName = "creationDate")]
        public DateTime CreationDate { get; set; }

        [JsonProperty(PropertyName = "voteCount")]
        public int VoteCount { get; set; }

        [JsonProperty(PropertyName = "commentCount")]
        public int CommentCount { get; set; }

        [JsonProperty(PropertyName = "userId")]
        public string UserId { get; set; }

        [JsonProperty(PropertyName = "observationDate")]
        public DateTime? ObservationDate { get; set; }
    }
}
