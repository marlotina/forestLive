using FL.Pereza.Helpers.Standard.Images;
using FL.WebAPI.Core.Items.Api.Mapper.v1.Contracts;
using FL.WebAPI.Core.Items.Api.Models.v1.Request;
using FL.WebAPI.Core.Items.Api.Models.v1.Response;
using FL.WebAPI.Core.Items.Domain.Dto;
using FL.WebAPI.Core.Items.Domain.Entities;
using Microsoft.Azure.Cosmos.Spatial;
using System.Collections.Generic;
using System.Linq;

namespace FL.WebAPI.Core.Items.Api.Mapper.v1.Implementation
{
    public class PostMapper : IPostMapper
    {
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

                if (source.Longitude.HasValue && source.Latitude.HasValue) {

                    result.Location = new Point(source.Longitude.Value, source.Latitude.Value);
                }
            }
            return result;
        }

        public PostResponse Convert(BirdPost source, IEnumerable<VotePostResponse> postVotes = null)
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
                    Labels = source.Labels == null || !source.Labels.Any() ? new string[0] :  source.Labels,
                    VoteCount = source.VoteCount,
                    CommentCount = source.CommentCount,
                    ObservationDate = source.ObservationDate.HasValue ? source.ObservationDate.Value.ToString("dd/MM/yyyy") : string.Empty,
                    UserPhoto = $"{source.UserId}{ImageHelper.USER_PROFILE_IMAGE_EXTENSION}"
                };

                if (vote != null)
                {
                    result.HasVote = true;
                    result.VoteId = vote.VoteId;
                }

                if (source.Location != null) {
                    result.Latitude = source.Location.Position.Latitude;
                    result.Longitude = source.Location.Position.Longitude;
                }
            }
            return result;
        }

        public PostResponse Convert(PostDto source, IEnumerable<VotePostResponse> postVotes = null)
        {
            var result = default(PostResponse);
            if (source != null)
            {
                var vote = postVotes != null ? postVotes.FirstOrDefault(x => x.PostId == source.PostId) : null;
                result = new PostResponse()
                {
                    Id = source.PostId,
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

        public PostListResponse ConvertToList(PostDto source, IEnumerable<VotePostResponse> postVotes = null)
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
                    ImageUrl = source.ImageUrl,
                    Type = "post",
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

                if (vote != null)
                {
                    result.HasVote = true;
                    result.VoteId = vote.VoteId;
                }
            }
            return result;
        }
    }
}
