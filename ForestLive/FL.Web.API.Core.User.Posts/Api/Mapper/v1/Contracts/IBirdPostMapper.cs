using FL.WebAPI.Core.User.Posts.Api.Models.v1.Response;
using FL.WebAPI.Core.User.Posts.Domain.Entities;
using System;
using System.Collections.Generic;

namespace FL.WebAPI.Core.User.Posts.Api.Mapper.v1.Contracts
{
    public interface IBirdPostMapper
    {
        BirdPostResponse Convert(BirdPost source, IEnumerable<Guid> postVotes);

        BirdPointResponse MapConvert(BirdPost source);
    }
}
