using FL.Web.API.Core.Post.Interactions.Api.Mapper.v1.Contracts;
using FL.Web.API.Core.Post.Interactions.Api.Models.v1.Request;
using FL.Web.API.Core.Post.Interactions.Api.Models.v1.Response;
using FL.Web.API.Core.Post.Interactions.Domain.Dto;
using FL.Web.API.Core.Post.Interactions.Domain.Entities;

namespace FL.Web.API.Core.Post.Interactions.Api.Mapper.v1.Implementation
{
    public class VoteMapper : IVoteMapper
    {
        public VoteCommentPostDto Convert(VoteCommentRequest source)
        {
            var result = default(VoteCommentPostDto);
            if (source != null)
            {
                result = new VoteCommentPostDto()
                {
                    Text = source.Text,
                    UserId = source.UserId,
                    PostId = source.PostId,
                    AuthorPostId = source.AuthorPostId,
                    CommentId = source.CommentId
                };
            }
            return result;
        }

        public VotePostDto Convert(VoteRequest source)
        {
            var result = default(VotePostDto);
            if (source != null)
            {
                result = new VotePostDto()
                {
                    TitlePost = source.TitlePost,
                    UserId = source.UserId,
                    PostId = source.PostId,
                    AuthorPostId = source.AuthorPostId,
                    SpecieId = source.SpecieId,

                };
            }
            return result;
        }

        public VoteResponse Convert(VotePost source)
        {
            var result = default(VoteResponse);
            if (source != null)
            {
                result = new VoteResponse()
                {
                    UserId = source.UserId,
                    PostId = source.PostId,
                    CreationDate = source.CreationDate.ToString("ddMMyyyhhmm"),
                    Id = source.Id,
                    AuthorPostId = source.AuthorPostId,
                    SpecieId = source.SpecieId
                };
            }
            return result;
        }

        public VoteCommentPostDto Convert(VoteCommentPost source)
        {
            var result = default(VoteCommentPostDto);
            if (source != null)
            {
                result = new VoteCommentPostDto()
                {
                    UserId = source.UserId,
                    PostId = source.PostId,
                    CommentId = source.CommentId,
                    Id = source.Id
                };
            }
            return result;
        }

        public Models.v1.Response.VotePostResponse ConvertUserVote(VotePost source)
        {
            var result = default(Models.v1.Response.VotePostResponse);
            if (source != null)
            {
                result = new Models.v1.Response.VotePostResponse()
                {
                    VoteId = source.Id,
                    PostId = source.PostId,
                };
            }
            return result;
        }
    }
}
