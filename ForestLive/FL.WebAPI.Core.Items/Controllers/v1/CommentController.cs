﻿using FL.LogTrace.Contracts.Standard;
using FL.WebAPI.Core.Items.Application.Services.Contracts;
using FL.WebAPI.Core.Items.Mapper.v1.Contracts;
using FL.WebAPI.Core.Items.Models.v1.Request;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Items.Controllers.v1
{
    public class CommentController : Controller
    {
        private readonly ILogger<CommentController> logger;
        private readonly ICommentMapper commentMapper;
        private readonly ICommentService commentService;

        public CommentController(ICommentMapper commentMapper,
            ICommentService commentService,
            ILogger<CommentController> logger)
        {
            this.commentService = commentService;
            this.commentMapper = commentMapper;
            this.logger = logger;
        }

        [HttpPost]
        [Route("AddComment", Name = "AddComment")]
        public async Task<IActionResult> AddComment(CommentRequest request) 
        {
            try
            {
                var comment = this.commentMapper.Convert(request);
                var result = await this.commentService.AddComment(comment);

                if (result != null)
                {
                    var commentResponse = this.commentMapper.Convert(result);
                    return this.CreatedAtRoute("GetCommentById", new { id = commentResponse.Id }, commentResponse);
                }
                else
                    return this.BadRequest();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex);
                return this.Problem();
            }
        }

        [HttpDelete]
        [Route("DeleteComment", Name = "DeleteComment")]
        public async Task<IActionResult> DeleteComment(Guid idComment, Guid itemId)
        {
            try
            {
                var result = await this.commentService.DeleteComment(idComment, itemId);
                if (result)
                    return this.Ok();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex);
                return this.Problem();
            }

            return this.BadRequest();
        }
    }
}
