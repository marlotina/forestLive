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
        public PostListResponse Convert(PostDto source, IEnumerable<VotePostResponse> postVotes)
        {
            var result = default(PostListResponse);
            if (source != null)
            {
                result = new PostListResponse()
                {
                    PostId = source.PostId,
                    Title = source.Title,
                    Text = source.Text,
                    ImageUrl = source.ImageUrl,
                    AltImage = source.AltImage,
                    Type = source.Type,
                    CreationDate = source.CreationDate,
                    UserId = source.UserId,
                    BirdSpecie = source.SpecieName,
                    SpecieId = source.SpecieId,
                    Labels = source.Labels == null || !source.Labels.Any() ? new string[0] : source.Labels,
                    VoteCount = source.VoteCount,
                    CommentCount = source.CommentCount,
                    UserPhoto = $"{source.UserId}{ImageHelper.USER_PROFILE_IMAGE_EXTENSION}"
                };

                if (postVotes != null)
                {
                    var vote = postVotes.FirstOrDefault(x => x.PostId == source.PostId);
                    if (vote != null)
                    {
                        result.HasVote = true;
                        result.VoteId = vote.VoteId;
                    }
                }
            }

            return result;
        }

        public BirdMapResponse MapConvert(PointPostDto source)
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
