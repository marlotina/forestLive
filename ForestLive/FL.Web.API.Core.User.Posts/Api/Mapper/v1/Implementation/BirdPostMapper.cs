using FL.Pereza.Helpers.Standard.Images;
using FL.Web.API.Core.User.Posts.Api.Models.v1.Response;
using FL.Web.API.Core.User.Posts.Domain.Dto;
using FL.WebAPI.Core.User.Posts.Api.Mapper.v1.Contracts;
using FL.WebAPI.Core.User.Posts.Api.Models.v1.Response;
using FL.WebAPI.Core.User.Posts.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FL.WebAPI.Core.User.Posts.Api.Mapper.v1.Implementation
{
    public class BirdPostMapper : IBirdPostMapper
    {
        public BirdPostResponse Convert(BirdPost source, IEnumerable<VotePostResponse> postVotes)
        {
            var result = default(BirdPostResponse);
            if (source != null)
            {
                var vote = postVotes.FirstOrDefault(x => x.PostId == source.PostId);
                result = new BirdPostResponse()
                {
                    Id = source.Id,
                    PostId = source.PostId,
                    Title = source.Title,
                    Text = source.Text,
                    ImageUrl = source.ImageUrl,
                    AltImage = source.AltImage,
                    CreationDate = source.CreationDate,
                    UserId = source.UserId,
                    BirdSpecie = source.SpecieName,
                    SpecieId = source.SpecieId,
                    Labels = source.Labels == null || !source.Labels.Any() ? new string[0] :  source.Labels,
                    VoteCount = source.VoteCount,
                    CommentCount = source.CommentCount,
                    Latitude = source.Location.Position.Latitude,
                    Longitude = source.Location.Position.Longitude,
                    ObservationDate = source.ObservationDate.ToString("dd/MM/yyyy"),
                    HasVote = vote != null,
                    VoteId = vote != null ? vote.VoteId : Guid.Empty,
                    UserPhoto = $"{source.UserId}{ImageHelper.USER_PROFILE_IMAGE_EXTENSION}"
                };
            }

            return result;
        }

        public BirdMapResponse MapConvert(BirdPost source)
        {
            var result = default(BirdMapResponse);
            if (source != null)
            {
                result = new BirdMapResponse()
                {
                    PostId = source.PostId,
                    Location = new PositionResponse
                        {
                            Lat = source.Location.Position.Latitude,
                            Lng = source.Location.Position.Longitude,
                        }
                };
            }

            return result;
        }

        public ModalBirdPostResponse ModalConvert(BirdPost source)
        {
            var result = default(ModalBirdPostResponse);
            if (source != null)
            {
                result = new ModalBirdPostResponse()
                {
                    Id = source.Id,
                    Title = source.Title,
                    Text = source.Text,
                    ImageUrl = source.ImageUrl,
                    AltImage = source.AltImage,
                    UserId = source.UserId,
                    BirdSpecie = source.SpecieName,
                    SpecieId = source.SpecieId,
                    ObservationDate = source.ObservationDate.ToString("dd/MM/yyyy")
                };
            }

            return result;
        }
    }
}
