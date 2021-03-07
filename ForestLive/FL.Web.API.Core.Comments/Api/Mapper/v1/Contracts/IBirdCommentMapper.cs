using FL.Web.API.Core.Comments.Domain.Entities;
using FL.Web.API.Core.Comments.Models.v1.Request;
using FL.Web.API.Core.Comments.Models.v1.Response;

namespace FL.Web.API.Core.Comments.Mapper.v1.Contracts
{
    public interface IBirdCommentMapper
    {
        BirdComment Convert(BirdCommentRequest source);

        BirdCommentResponse Convert(BirdComment source);
    }
}
