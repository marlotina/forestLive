﻿using FL.Functions.Posts.Dto;
using FL.Functions.Posts.Model;
using System.Threading.Tasks;

namespace FL.Functions.Posts.Services
{
    public interface IPostCosmosService
    {
        Task AddCommentPostAsync(BirdCommentDto comment);

        Task DeleteCommentPostAsync(BirdCommentDto comment);

        Task AddVotePostAsync(VotePost vote);

        Task DeleteVotePostAsync(VotePost vote);
    }
}
