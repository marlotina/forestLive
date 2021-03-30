using FL.Web.API.Core.User.Interactions.Domain.Entities;
using FL.Web.API.Core.User.Interactions.Models.v1.Response;

namespace FL.Web.API.Core.User.Interactions.Mapper.v1.Contracts
{
    public interface ICommentMapper
    {
        CommentResponse Convert(CommentPost source);
    }
}
