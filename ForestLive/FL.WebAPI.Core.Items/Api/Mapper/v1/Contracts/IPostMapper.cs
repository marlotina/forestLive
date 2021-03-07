using FL.WebAPI.Core.Items.Api.Models.v1.Request;
using FL.WebAPI.Core.Items.Api.Models.v1.Response;
using FL.WebAPI.Core.Items.Domain.Entities;

namespace FL.WebAPI.Core.Items.Api.Mapper.v1.Contracts
{
    public interface IPostMapper
    {
        BirdPost Convert(PostRequest source);

        PostResponse Convert(BirdPost source);

        BirdCommentResponse Convert(BirdComment source);
    }
}
