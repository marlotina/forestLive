﻿using FL.Functions.SpeciePost.Dto;
using System.Threading.Tasks;

namespace FL.Functions.SpeciePost.Services
{
    public interface ISpeciePostCosmosService
    {
        Task AddCommentCountAsync(CommentBaseDto comment);

        Task DeleteCommentCountAsync(CommentBaseDto comment);

        Task AddVotePostCountAsync(VotePostBaseDto vote);

        Task DeleteVotePostCountAsync(VotePostBaseDto vote);
    }
}
