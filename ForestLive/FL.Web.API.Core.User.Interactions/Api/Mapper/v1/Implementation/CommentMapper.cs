using FL.Pereza.Helpers.Standard.Images;
using FL.Web.API.Core.User.Interactions.Domain.Entities;
using FL.Web.API.Core.User.Interactions.Mapper.v1.Contracts;
using FL.Web.API.Core.User.Interactions.Models.v1.Response;

namespace FL.Web.API.Core.User.Interactions.Mapper.v1.Implementation
{
    public class CommentMapper : ICommentMapper
    {
        public CommentResponse Convert(CommentPost source)
        {
            var result = default(CommentResponse);
            if (source != null)
            {
                result = new CommentResponse()
                {
                    Id = source.Id,
                    Text = source.Text,
                    CreationDate = source.CreationDate.ToString("dd/MM/yyyy hh:mm"),
                    PostId = source.PostId,
                    AuthorPostId = source.AuthorPostId,
                    TitlePost = source.TitlePost,
                    SpecieId = source.SpecieId
                };
            }
            return result;
        }
    }
}
