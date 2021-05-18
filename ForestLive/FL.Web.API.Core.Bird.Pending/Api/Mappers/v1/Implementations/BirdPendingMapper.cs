using FL.Pereza.Helpers.Standard.Images;
using FL.Web.API.Core.Bird.Pending.Api.Mappers.v1.Contracts;
using FL.Web.API.Core.Bird.Pending.Api.Models.v1.Response;
using FL.Web.API.Core.Bird.Pending.Domain.Dto;
using System.Collections.Generic;
using System.Linq;

namespace FL.Web.API.Core.Bird.Pending.Api.Mappers.v1.Implementations
{
    public class BirdPendingMapper : IBirdPendingMapper
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
                    ImageUrl = source.ImageUrl,
                    Type = "pending",
                    AltImage = source.AltImage,
                    CreationDate = source.CreationDate,
                    UserId = source.UserId,
                    BirdSpecie = source.SpecieName,
                    SpecieId = source.SpecieId.Value,
                    Labels = source.Labels == null || !source.Labels.Any() ? new string[0] :  source.Labels,
                    VoteCount = source.VoteCount,
                    CommentCount = source.CommentCount,
                    UserPhoto = $"{source.UserId}{ImageHelper.USER_PROFILE_IMAGE_EXTENSION}",
                    ObservationDate = source.ObservationDate.HasValue ? source.ObservationDate.Value.ToString("dd/MM/yyyy") : string.Empty,
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
