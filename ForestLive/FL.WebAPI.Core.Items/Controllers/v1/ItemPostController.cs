using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using FL.LogTrace.Contracts.Standard;
using FL.WebAPI.Core.Items.Api.Mapper.v1.Contracts;
using FL.WebAPI.Core.Items.Api.Models.v1.Request;
using FL.WebAPI.Core.Items.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FL.WebAPI.Core.Items.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ItemPostController : ControllerBase
    {
        private readonly ILogger<ItemPostController> logger;
        private readonly IItemService birdPhotosService;
        private readonly IBirdPostMapper birdPhotoMapper;

        public ItemPostController(IItemService birdPhotosService,
            IBirdPostMapper birdPhotoMapper,
            ILogger<ItemPostController> logger)
        {
            this.logger = logger;
            this.birdPhotosService = birdPhotosService ?? throw new ArgumentNullException(nameof(birdPhotosService));
            this.birdPhotoMapper = birdPhotoMapper;
        }

        [HttpPost]
        [Route("AddPost", Name = "AddPost")]
        public async Task<IActionResult> AddPost([FromBody] BirdPostRequest request)
        {
            try
            {
                if (request == null)
                    return null;

                if (request.UserId == null || request.UserId == Guid.Empty
                    || string.IsNullOrWhiteSpace(request.ImageData))
                    return this.BadRequest();

                var post = this.birdPhotoMapper.Convert(request);

                var bytes = Convert.FromBase64String(request.ImageData);
                var contents = new StreamContent(new MemoryStream(bytes));
                var imageStream = await contents.ReadAsStreamAsync();

                var result = await this.birdPhotosService.AddBirdPost(post, imageStream);

                if (result != null)
                {
                    var postResponse = this.birdPhotoMapper.Convert(result);
                    return this.CreatedAtRoute("GetPostById", new { id = postResponse.Id }, postResponse);
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
        public async Task<IActionResult> DeletePost([FromBody] BirdPostRequest request)
        {
            try
            {
                var result = false;

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
    }
}