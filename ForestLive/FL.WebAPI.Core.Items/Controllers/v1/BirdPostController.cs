﻿using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using FL.LogTrace.Contracts.Standard;
using FL.WebAPI.Core.Items.Api.Mapper.v1.Contracts;
using FL.WebAPI.Core.Items.Api.Models.v1.Request;
using FL.WebAPI.Core.Items.Application.Services.Contracts;
using FL.WebAPI.Core.Items.Models.v1.Request;
using Microsoft.AspNetCore.Mvc;

namespace FL.WebAPI.Core.Items.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BirdPostController : ControllerBase
    {
        private readonly ILogger<BirdPostController> logger;
        private readonly IBirdPostService itemService;
        private readonly IBirdPostMapper birdItemMapper;

        public BirdPostController(IBirdPostService itemService,
            IBirdPostMapper birdItemMapper,
            ILogger<BirdPostController> logger)
        {
            this.logger = logger;
            this.itemService = itemService ?? throw new ArgumentNullException(nameof(itemService));
            this.birdItemMapper = birdItemMapper ?? throw new ArgumentNullException(nameof(birdItemMapper));
        }

        [HttpPost]
        [Route("AddPost", Name = "AddPost")]
        public async Task<IActionResult> AddPost([FromBody] BirdPostRequest request)
        {
            try
            {
                if (request == null)
                    return null;

                if (string.IsNullOrWhiteSpace(request.UserId)
                    || string.IsNullOrWhiteSpace(request.ImageData))
                    return this.BadRequest();

                var post = this.birdItemMapper.Convert(request);

                var bytes = Convert.FromBase64String(request.ImageData.Split(',')[1]);
                var contents = new StreamContent(new MemoryStream(bytes));
                var imageStream = await contents.ReadAsStreamAsync();

                var result = await this.itemService.AddBirdItem(post, imageStream, request.ImageName);

                if (result != null)
                {
                    var postResponse = this.birdItemMapper.Convert(result);
                    return this.CreatedAtRoute("GetPost", new { id = postResponse.Id }, postResponse);
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
        [Route("DeletePost", Name = "DeletePost")]
        public async Task<IActionResult> DeletePost(Guid postId)
        {
            try
            {
                if (postId == Guid.Empty || postId == null) {
                    this.BadRequest();
                }
                var userId = new Guid();
                var result = await this.itemService.DeleteBirdItem(postId, userId);

                if (result)
                {
                    return this.Ok();
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
        [Route("GetPost", Name = "GetPost")]
        public async Task<IActionResult> GetPost(Guid postId)
        {
            try
            {
                if (postId == Guid.Empty || postId == null)
                {
                    this.BadRequest();
                }

                var result = await this.itemService.GetBirdItem(postId);

                var itemResponse = this.birdItemMapper.Convert(result);
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
    }
}