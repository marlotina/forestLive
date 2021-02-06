using System;
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
    public class ItemController : ControllerBase
    {
        private readonly ILogger<ItemController> logger;
        private readonly IItemService itemService;
        private readonly IBirdPostMapper birdItemMapper;

        public ItemController(IItemService itemService,
            IBirdPostMapper birdItemMapper,
            ILogger<ItemController> logger)
        {
            this.logger = logger;
            this.itemService = itemService ?? throw new ArgumentNullException(nameof(itemService));
            this.birdItemMapper = birdItemMapper ?? throw new ArgumentNullException(nameof(birdItemMapper));
        }

        [HttpPost]
        [Route("AddItem", Name = "AddItem")]
        public async Task<IActionResult> AddItem([FromBody] ItemRequest request)
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
                    return this.CreatedAtRoute("GetItem", new { id = postResponse.Id }, postResponse);
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
        [Route("DeleteItem", Name = "DeleteItem")]
        public async Task<IActionResult> DeleteItem(Guid itemId)
        {
            try
            {
                if (itemId == Guid.Empty || itemId == null) {
                    this.BadRequest();
                }
                var userId = new Guid();
                var result = await this.itemService.DeleteBirdItem(itemId, userId);

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
        [Route("GetItem", Name = "GetItem")]
        public async Task<IActionResult> GetItem(Guid itemId)
        {
            try
            {
                if (itemId == Guid.Empty || itemId == null)
                {
                    this.BadRequest();
                }

                var result = await this.itemService.GetBirdItem(itemId);

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