using FL.Pereza.Helpers.Standard.Images;
using FL.WebAPI.Core.Birds.Api.Mappers.v1.Contracts;
using FL.WebAPI.Core.Birds.Api.Models.v1.Request;
using FL.WebAPI.Core.Birds.Api.Models.v1.Response;
using FL.WebAPI.Core.Birds.Domain.Dto;
using FL.WebAPI.Core.Birds.Domain.Model;
using Microsoft.Azure.Cosmos.Spatial;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FL.WebAPI.Core.Birds.Api.Mappers.v1.Implementations
{
    public class BirdSpeciePostMapper : IBirdSpeciePostMapper
    {
        public PostListResponse Convert(PostDto source, IEnumerable<VotePostResponse> postVotes = null)
        {
            var result = default(PostListResponse);
            if (source != null)
            {
                var vote = postVotes != null ? postVotes.FirstOrDefault(x => x.PostId == source.PostId) : null;
                result = new PostListResponse()
                {
                    PostId = source.PostId,
                    Title = source.Title,
                    Text = source.Text,
                    Type = "bird",
                    ImageUrl = source.ImageUrl,
                    AltImage = source.AltImage,
                    CreationDate = source.CreationDate,
                    UserId = source.UserId,
                    BirdSpecie = source.SpecieName,
                    SpecieId = source.SpecieId.Value,
                    Labels = source.Labels == null || !source.Labels.Any() ? new string[0] :  source.Labels,
                    VoteCount = source.VoteCount,
                    CommentCount = source.CommentCount,
                    HasVote = false,
                    ObservationDate = source.ObservationDate.HasValue ? source.ObservationDate.Value.ToString("dd/MM/yyyy"): string.Empty,
                    UserPhoto = $"{source.UserId}{ImageHelper.USER_PROFILE_IMAGE_EXTENSION}"
                };

                if (vote != null)
                {
                    result.HasVote = true;
                    result.VoteId = vote.VoteId;
                }
            }

            return result;
        }

        public BirdPost Convert(PostRequest source)
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
                    ObservationDate = source.ObservationDate,
                    SpecieId = source.SpecieId
                };

                if (source.Longitude.HasValue && source.Latitude.HasValue)
                {
                    result.Location = new Point(source.Longitude.Value, source.Latitude.Value);
                }
            }
            return result;
        }

        public PostListResponse Convert(BirdPost source)
        {
            var result = default(PostListResponse);
            if (source != null)
            {
                result = new PostListResponse()
                {
                    PostId = source.PostId,
                    Title = source.Title,
                    Text = source.Text,
                    Type = "bird",
                    ImageUrl = source.ImageUrl,
                    AltImage = source.AltImage,
                    CreationDate = source.CreationDate,
                    UserId = source.UserId,
                    BirdSpecie = source.SpecieName,
                    SpecieId = source.SpecieId,
                    Labels = source.Labels == null || !source.Labels.Any() ? new string[0] : source.Labels,
                    VoteCount = source.VoteCount,
                    CommentCount = source.CommentCount,
                    UserPhoto = $"{source.UserId}{ImageHelper.USER_PROFILE_IMAGE_EXTENSION}"
                };
            }

            return result;
        }

        public PostResponse ConvertPost(BirdPost source, IEnumerable<VotePostResponse> postVotes = null)
        {
            var result = default(PostResponse);
            if (source != null)
            {
                var vote = postVotes != null ? postVotes.FirstOrDefault(x => x.PostId == source.PostId) : null;
                result = new PostResponse()
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
                    Labels = source.Labels == null || !source.Labels.Any() ? new string[0] : source.Labels,
                    VoteCount = source.VoteCount,
                    CommentCount = source.CommentCount,
                    HasVote = false,
                    ObservationDate = source.ObservationDate.HasValue ? source.ObservationDate.Value.ToString("dd/MM/yyyy") : string.Empty,
                    UserPhoto = $"{source.UserId}{ImageHelper.USER_PROFILE_IMAGE_EXTENSION}"
                };

                if (vote != null)
                {
                    result.HasVote = true;
                    result.VoteId = vote.VoteId;
                }

                if (source.Location != null)
                {
                    result.Latitude = source.Location.Position.Latitude;
                    result.Longitude = source.Location.Position.Longitude;
                }
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
                    SpecieId = source.SpecieId,
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
                    Type = source.Type,
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
