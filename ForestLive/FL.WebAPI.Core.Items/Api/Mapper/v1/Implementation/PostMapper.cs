using FL.Pereza.Helpers.Standard.Images;
using FL.WebAPI.Core.Items.Api.Mapper.v1.Contracts;
using FL.WebAPI.Core.Items.Api.Models.v1.Request;
using FL.WebAPI.Core.Items.Api.Models.v1.Response;
using FL.WebAPI.Core.Items.Domain.Dto;
using FL.WebAPI.Core.Items.Domain.Entities;
using Microsoft.Azure.Cosmos.Spatial;
using System;
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
                    Location = new Point(double.Parse(source.Longitude), double.Parse(source.Latitude)),
                    ObservationDate = source.ObservationDate
                };

                if (!string.IsNullOrWhiteSpace(source.SpecieId)) {
                    result.SpecieId = Guid.Parse(source.SpecieId);
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
                    Latitude = source.Location.Position.Latitude.ToString(),
                    Longitude = source.Location.Position.Longitude.ToString(),
                    ObservationDate = source.ObservationDate.ToString("dd/MM/yyyy"),
                    HasVote = vote != null,
                    VoteId = vote != null ? vote.VoteId : Guid.Empty,
                };
            }
            return result;
        }

        public BirdCommentResponse Convert(BirdComment source)
        {
            var result = default(BirdCommentResponse);
            if (source != null)
            {
                result = new BirdCommentResponse()
                {
                    Id = source.Id,
                    Text = source.Text,
                    UserId = source.UserId,
                    CreationDate = source.CreationDate.ToString("dd/MM/yyyy hh:mm"),
                    PostId = source.PostId,
                    UserImage = source.UserId + ImageHelper.USER_PROFILE_IMAGE_EXTENSION
                };
            }
            return result;
        }
    }
}
