using FL.WebAPI.Core.Items.Api.Mapper.v1.Contracts;
using FL.WebAPI.Core.Items.Api.Models.v1.Request;
using FL.WebAPI.Core.Items.Api.Models.v1.Response;
using FL.WebAPI.Core.Items.Domain.Entities;
using Microsoft.Azure.Cosmos.Spatial;
using System.Linq;

namespace FL.WebAPI.Core.Items.Api.Mapper.v1.Implementation
{
    public class BirdPostMapper : IBirdPostMapper
    {
        public Item Convert(ItemRequest source)
        {
            var result = default(Item);
            if (source != null)
            {
                result = new Item()
                {
                    Title = source.Title,
                    Text = source.Text,
                    UserId = source.UserId,
                    SpecieName = source.SpecieName,
                    SpecieId = source.SpecieId,
                    Labels = source.Labels,
                    AltImage = source.AltImage,
                    Location = new Point(double.Parse(source.Longitude), double.Parse(source.Latitude)),
                    ObservationDate = source.ObservationDate
                };
            }
            return result;
        }

        public ItemResponse Convert(Item source)
        {
            var result = default(ItemResponse);
            if (source != null)
            {
                result = new ItemResponse()
                {
                    Id = source.Id,
                    ItemId = source.ItemId,
                    Title = source.Title,
                    Text = source.Text,
                    ImageUrl = source.ImageUrl,
                    AltImage = source.AltImage,
                    CreateDate = source.CreateDate,
                    UserId = source.UserId,
                    BirdSpecie = source.SpecieName,
                    SpecieId = source.SpecieId,
                    Labels = source.Labels == null || !source.Labels.Any() ? new string[0] :  source.Labels,
                    LikesCount = source.LikesCount,
                    CommentsCount = source.CommentsCount,
                    Latitude = source.Location.Position.Latitude.ToString(),
                    Longitude = source.Location.Position.Longitude.ToString(),
                    ObservationDate = source.ObservationDate,
                    SpecieConfirmed = source.SpecieConfirmed
                };
            }
            return result;
        }
    }
}
