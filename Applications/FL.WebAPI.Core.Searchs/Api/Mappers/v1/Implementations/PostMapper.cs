using FL.Pereza.Helpers.Standard.Images;
using FL.Web.API.Core.User.Posts.Domain.Dto;
using FL.WebAPI.Core.Searchs.Api.Mappers.v1.Contracts;
using FL.WebAPI.Core.Searchs.Api.Models.v1.Response;
using FL.WebAPI.Core.Searchs.Application.Services.Contracts;
using FL.WebAPI.Core.Searchs.Domain.Dto;
using FL.WebAPI.Core.Searchs.Domain.Model;
using System.Collections.Generic;
using System.Linq;

namespace FL.WebAPI.Core.Searchs.Api.Mappers.v1.Implementations
{
    public class PostMapper : IPostMapper
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
                    SpecieUrl = source.SpecieUrl,
                    SpecieId = source.SpecieId,
                    Labels = source.Labels == null || !source.Labels.Any() ? new string[0] : source.Labels,
                    VoteCount = source.VoteCount,
                    CommentCount = source.CommentCount,
                    HasVote = false,
                    ObservationDate = source.ObservationDate.HasValue ? source.ObservationDate.Value.ToString("dd/MM/yyyy"): string.Empty,
                    UserPhoto = source.UserImage
                };

                if (vote != null)
                {
                    result.HasVote = true;
                    result.VoteId = vote.VoteId;
                }
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
                    SpecieId = source.SpecieId,
                    Labels = source.Labels == null || !source.Labels.Any() ? new string[0] : source.Labels,
                    VoteCount = source.VoteCount,
                    CommentCount = source.CommentCount,
                    HasVote = false,
                    IsPost = source.Type == "post",
                    ObservationDate = source.ObservationDate.HasValue ? source.ObservationDate.Value.ToString("dd/MM/yyyy") : string.Empty,
                    //UserPhoto = source.UserImage
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

        public PostMapResponse Convert(PointPostDto source)
        {
            var result = default(PostMapResponse);
            if (source != null)
            {
                result = new PostMapResponse()
                {
                    PostId = source.PostId,
                    UserId = source.UserId,
                    Location = new PositionResponse
                    {
                        Lat = source.Location.Position.Latitude,
                        Lng = source.Location.Position.Longitude,
                    }
                };
            }

            return result;
        }

        public ModalPostResponse ModalConvert(BirdPost source)
        {
            var result = default(ModalPostResponse);
            if (source != null)
            {
                result = new ModalPostResponse()
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
