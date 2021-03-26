using FL.Pereza.Helpers.Standard.Images;
using FL.Web.API.Core.Comments.Domain.Entities;
using FL.Web.API.Core.Comments.Mapper.v1.Contracts;
using FL.Web.API.Core.Comments.Models.v1.Request;
using FL.Web.API.Core.Comments.Models.v1.Response;

namespace FL.Web.API.Core.Comments.Mapper.v1.Implementation
{
    public class BirdCommentMapper : IBirdCommentMapper
    {
        public BirdComment Convert(BirdCommentRequest source)
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
