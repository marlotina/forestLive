using FL.Web.API.Core.Post.Interactions.Domain.Dto;
using FL.Web.API.Core.Post.Interactions.Models.v1.Response;
using System.Collections.Generic;

namespace FL.Web.API.Core.Post.Interactions.Application.Mapper.Contracts
{
    public interface IPostDataMapper
    {
        IEnumerable<CommentResponse> ConvertAll(IEnumerable<PostDetails> source, IEnumerable<PostDetails> postVotes = null);
    }
}
