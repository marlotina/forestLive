using FL.Pereza.Helpers.Standard.Images;
using FL.Web.API.Core.Post.Interactions.Domain.Entities;
using FL.Web.API.Core.Post.Interactions.Mapper.v1.Contracts;
using FL.Web.API.Core.Post.Interactions.Models.v1.Request;
using FL.Web.API.Core.Post.Interactions.Models.v1.Response;

namespace FL.Web.API.Core.Post.Interactions.Mapper.v1.Implementation
{
    public class CommentMapper : ICommentMapper
    {
        public BirdComment Convert(CommentRequest source)
        {
            var result = default(BirdComment);
            if (source != null)
            {
                result = new BirdComment()
                {
                    PostId = source.PostId,
                    Text = source.Text,
                    UserId = source.UserId,
                    AuthorPostUserId = source.AuthorPostUserId,
                    TitlePost = source.TitlePost,
                    SpecieId = source.SpecieId
                };
            }
            return result;
        }

        public CommentResponse Convert(BirdComment source)
        {
            var result = default(CommentResponse);
            if (source != null)
            {
                result = new CommentResponse()
                {
                    Id = source.Id,
                    Text = source.Text,
                    UserId = source.UserId,
                    CreationDate = source.CreationDate.ToString("dd/MM/yyyy hh:mm"),
                    PostId = source.PostId,
                    UserImage = source.UserId + ImageHelper.USER_PROFILE_IMAGE_EXTENSION,
                    AuthorPostUserId = source.AuthorPostUserId,
                    TitlePost = source.TitlePost,
                    SpecieId = source.SpecieId
                };
            }
            return result;
        }
    }
}
