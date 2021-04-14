using FL.Web.API.Core.Post.Interactions.Domain.Dto;
using FL.Web.API.Core.Post.Interactions.Domain.Entities;
using FL.Web.API.Core.Post.Interactions.Models.v1.Request;
using FL.Web.API.Core.Post.Interactions.Models.v1.Response;
using System.Collections.Generic;

namespace FL.Web.API.Core.Post.Interactions.Mapper.v1.Contracts
{
    public interface ICommentMapper
    {
        CommentDto Convert(CommentRequest source);

        CommentResponse Convert(BirdComment source);

        IEnumerable<CommentResponse> Convert(IEnumerable<BirdComment> source);
    }
}
