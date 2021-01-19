using FL.WebAPI.Core.Items.Domain.Entities;
using FL.WebAPI.Core.Items.Mapper.v1.Contracts;
using FL.WebAPI.Core.Items.Models.v1.Request;
using FL.WebAPI.Core.Items.Models.v1.Response;

namespace FL.WebAPI.Core.Items.Mapper.v1.Implementation
{
    public class CommentMapper : ICommentMapper
    {
        public Comment Convert(CommentRequest source)
        {
            var result = default(Comment);
            if (source != null)
            {
                result = new Comment()
                {
                    Text = source.Text,
                    UserId = source.UserId,
                    UserName = source.UserName
                };
            }
            return result;
        }

        public CommentResponse Convert(Comment source)
        {
            var result = default(CommentResponse);
            if (source != null)
            {
                result = new CommentResponse()
                {
                    Id = source.Id,
                    Text = source.Text,
                    UserId = source.UserId,
                    UserName = source.UserName,
                    LikesCount = source.LikesCount,
                    CreateDate = source.CreateDate
                };
            }
            return result;
        }
    }
}
