using Microsoft.Azure.Cosmos.Spatial;
using Newtonsoft.Json;
using System;

namespace FL.WebAPI.Core.Items.Domain.Entities
{
    public class Item
    {
        [JsonProperty(PropertyName = "id")]
        public Guid Id { get; set; }

        [JsonProperty(PropertyName = "itemId")]
        public string ItemId { get; set; }

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
        public Guid SpecieId { get; set; }

        [JsonProperty(PropertyName = "imageUrl")]
        public string ImageUrl { get; set; }

        [JsonProperty(PropertyName = "altImage")]
        public string AltImage { get; set; }

        [JsonProperty(PropertyName = "labels")]
        public string[] Labels { get; set; }

        [JsonProperty(PropertyName = "createDate")]
        public DateTime CreateDate { get; set; }

        [JsonProperty(PropertyName = "likesCount")]
        public int LikesCount { get; set; }

        [JsonProperty(PropertyName = "commentsCount")]
        public int CommentsCount { get; set; }

        [JsonProperty(PropertyName = "userName")]
        public string UserName { get; set; }

        [JsonProperty(PropertyName = "userId")]
        public Guid UserId { get; set; }

        [JsonProperty(PropertyName = "observationDate")]
        public DateTime ObservationDate { get; set; }

        [JsonProperty(PropertyName = "specieConfirmed")]
        public bool SpecieConfirmed { get; set; }
    }
}
