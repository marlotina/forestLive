using FL.Pereza.Helpers.Standard.Images;
using FL.WebAPI.Core.Birds.Api.Mappers.v1.Contracts;
using FL.WebAPI.Core.Birds.Api.Models.v1.Response;
using FL.WebAPI.Core.Birds.Domain.Dto;
using FL.WebAPI.Core.Birds.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FL.WebAPI.Core.Birds.Api.Mappers.v1.Implementations
{
    public class BirdSpeciePostMapper : IBirdSpeciePostMapper
    {
        public BirdPostResponse Convert(PostDto source, IEnumerable<VotePostResponse> postVotes = null)
        {
            var result = default(BirdPostResponse);
            if (source != null)
            {
                var vote = postVotes != null ? postVotes.FirstOrDefault(x => x.PostId == source.PostId) : null;
                result = new BirdPostResponse()
                {
                    PostId = source.PostId,
                    Title = source.Title,
                    Text = source.Text,
                    ImageUrl = source.ImageUrl,
                    AltImage = source.AltImage,
                    CreationDate = source.CreationDate,
                    UserId = source.UserId,
                    BirdSpecie = source.SpecieName,
                    SpecieId = source.SpecieId.Value,
                    Labels = source.Labels == null || !source.Labels.Any() ? new string[0] :  source.Labels,
                    VoteCount = source.VoteCount,
                    CommentCount = source.CommentCount,
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
                    SpecieId = source.SpecieId.Value,
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
                    PostId = source.Id,
                    Title = source.Title,
                    Text = source.Text,
                    ImageUrl = source.ImageUrl,
                    AltImage = source.AltImage,
                    UserId = source.UserId,
                    BirdSpecie = source.SpecieName,
                    SpecieId = source.SpecieId,
                    ObservationDate = source.ObservationDate.HasValue ? source.ObservationDate.Value.ToString("dd/MM/yyyy") : string.Empty
                };
            }

            return result;
        }
    }
}
