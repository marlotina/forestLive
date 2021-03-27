using FL.WebAPI.Core.Items.Api.Models.v1.Request;
using FL.WebAPI.Core.Items.Api.Models.v1.Response;
using FL.WebAPI.Core.Items.Domain.Dto;
using FL.WebAPI.Core.Items.Domain.Entities;
using System;
using System.Collections.Generic;

namespace FL.WebAPI.Core.Items.Api.Mapper.v1.Contracts
{
    public interface IPostMapper
    {
        BirdPost Convert(PostRequest source);

        PostListResponse ConvertToList(PostDto source, IEnumerable<VotePostResponse> postVotes = null);

        PostResponse Convert(BirdPost source, IEnumerable<VotePostResponse> postVotes = null);

        PostResponse Convert(PostDto source, IEnumerable<VotePostResponse> postVotes = null);
    }
}
