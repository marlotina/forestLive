﻿using FL.LogTrace.Contracts.Standard;
using FL.Web.API.Core.User.Interactions.Application.Services.Contracts;
using FL.Web.API.Core.User.Interactions.Domain.Entities;
using FL.Web.API.Core.User.Interactions.Domain.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.Web.API.Core.User.Interactions.Application.Services.Implementations
{
    public class CommentService : ICommentService
    {
        private readonly ICommentRepository commentRepository;
        private readonly ILogger<CommentService> logger;

        public CommentService(
            ICommentRepository commentRepository,
            ILogger<CommentService> logger)
        {
            this.commentRepository = commentRepository;
            this.logger = logger;
        }

        public async Task<List<CommentPost>> GetCommentByUserId(string userId)
        {
            return await this.commentRepository.GetCommentsByUserIdAsync(userId);
        }
    }
}
