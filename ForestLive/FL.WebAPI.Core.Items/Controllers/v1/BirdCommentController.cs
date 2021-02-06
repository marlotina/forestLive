﻿using FL.LogTrace.Contracts.Standard;
using FL.WebAPI.Core.Items.Application.Services.Contracts;
using FL.WebAPI.Core.Items.Mapper.v1.Contracts;
using FL.WebAPI.Core.Items.Models.v1.Request;
using FL.WebAPI.Core.Items.Models.v1.Response;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Items.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BirdCommentController : Controller
    {
        private readonly ILogger<BirdCommentController> logger;
        private readonly IBirdCommentMapper commentMapper;
        private readonly IBirdCommentService commentService;

        public BirdCommentController(IBirdCommentMapper commentMapper,
            IBirdCommentService commentService,
            ILogger<BirdCommentController> logger)
        {
            this.commentService = commentService;
            this.commentMapper = commentMapper;
            this.logger = logger;
        }

        [HttpPost]
        [Route("AddComment", Name = "AddComment")]
        public async Task<IActionResult> AddComment([FromBody] BirdCommentRequest request) 
        {
            try
            {
                var comment = this.commentMapper.Convert(request);
                var result = await this.commentService.AddComment(comment);

                if (result != null)
                {
                    var commentResponse = this.commentMapper.Convert(result);
                    return this.Ok(commentResponse);
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

        [HttpGet]
        [Route("GetComment", Name = "GetComment")]
        public async Task<IActionResult> GetComment(Guid postId)
        {
            try
            {
                if (postId == Guid.Empty || postId == null)
                {
                    this.BadRequest();
                }

                var result = await this.commentService.GetCommentByItem(postId);
                var itemResponse = result.Select(x => this.commentMapper.Convert(x));
                    
                if (itemResponse != null)
                {
                    return this.Ok(itemResponse);
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
        public async Task<IActionResult> DeleteComment(Guid idComment, Guid postId)
        {
            try
            {
                var result = await this.commentService.DeleteComment(idComment, postId);
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