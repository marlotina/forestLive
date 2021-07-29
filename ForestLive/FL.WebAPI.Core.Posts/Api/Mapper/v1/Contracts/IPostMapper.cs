using FL.WebAPI.Core.Posts.Api.Models.v1.Request;
using FL.WebAPI.Core.Posts.Api.Models.v1.Response;
using FL.WebAPI.Core.Posts.Domain.Dto;
using FL.WebAPI.Core.Posts.Domain.Entities;
using System;
using System.Collections.Generic;

namespace FL.WebAPI.Core.Posts.Api.Mapper.v1.Contracts
{
    public interface IPostMapper
    {
        BirdPost Convert(PostRequest source);

        PostResponse Convert(BirdPost source, IEnumerable<VotePostResponse> postVotes = null);
    }
}
