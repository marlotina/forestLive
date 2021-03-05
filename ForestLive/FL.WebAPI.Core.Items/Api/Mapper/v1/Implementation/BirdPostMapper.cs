using FL.WebAPI.Core.Items.Api.Mapper.v1.Contracts;
using FL.WebAPI.Core.Items.Api.Models.v1.Request;
using FL.WebAPI.Core.Items.Api.Models.v1.Response;
using FL.WebAPI.Core.Items.Domain.Entities;
using Microsoft.Azure.Cosmos.Spatial;
using System;
using System.Linq;

namespace FL.WebAPI.Core.Items.Api.Mapper.v1.Implementation
{
    public class BirdPostMapper : IBirdPostMapper
    {
        public BirdPost Convert(BirdPostRequest source)
        {
            var result = default(BirdPost);
            if (source != null)
            {
                result = new BirdPost()
                {
                    Title = source.Title,
                    Text = source.Text,
                    UserId = source.UserId,
                    SpecieName = source.SpecieName,
                    Labels = source.Labels,
                    AltImage = source.AltImage,
                    Location = new Point(double.Parse(source.Longitude), double.Parse(source.Latitude)),
                    ObservationDate = source.ObservationDate
                };

                if (!string.IsNullOrWhiteSpace(source.SpecieId)) {
                    result.SpecieId = Guid.Parse(source.SpecieId);
                }
            }
            return result;
        }

        public BirdPostResponse Convert(BirdPost source)
        {
            var result = default(BirdPostResponse);
            if (source != null)
            {
                result = new BirdPostResponse()
                {
                    Id = source.Id,
                    PostId = source.PostId,
                    Title = source.Title,
                    Text = source.Text,
                    ImageUrl = source.ImageUrl,
                    AltImage = source.AltImage,
                    CreateDate = source.CreateDate,
                    UserId = source.UserId,
                    BirdSpecie = source.SpecieName,
                    SpecieId = source.SpecieId,
                    Labels = source.Labels == null || !source.Labels.Any() ? new string[0] :  source.Labels,
                    VoteCount = source.VoteCount,
                    CommentCount = source.CommentCount,
                    Latitude = source.Location.Position.Latitude.ToString(),
                    Longitude = source.Location.Position.Longitude.ToString(),
                    ObservationDate = source.ObservationDate.ToString("dd/MM/yyyy")
                };
            }
            return result;
        }
    }
}
