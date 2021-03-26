using FL.WebAPI.Core.Items.Domain.Dto;
using FL.WebAPI.Core.Items.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Items.Application.Services.Contracts
{
    public interface IPostService
    {
        Task<BirdPost> AddBirdPost(BirdPost birdPost, byte[] imageBytes, string imageName, bool isPost);

        Task<bool> DeleteBirdPost(Guid birdPostId, string userId);

        Task<BirdPost> GetBirdPost(Guid birdPostId);

        Task<List<BirdComment>> GetCommentByPost(Guid postId);

        Task<IEnumerable<VotePostResponse>> GetVoteByUserId(IEnumerable<Guid> listPost, string webUserId);

        Task<List<PostDto>> GetPosts(int orderBy);
    }
}
