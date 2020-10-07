using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using FL.LogTrace.Contracts.Standard;
using FL.WebAPI.Core.Items.Api.Mapper.v1.Contracts;
using FL.WebAPI.Core.Items.Api.Models.v1.Request;
using FL.WebAPI.Core.Items.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace FL.WebAPI.Core.Items.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class BirthPhotoController : ControllerBase
    {
        private readonly ILogger<BirthPhotoController> logger;
        private readonly IBirdPhotosService birdPhotosService;
        private readonly IBirdPhotoMapper birdPhotoMapper;

        public BirthPhotoController(IBirdPhotosService birdPhotosService,
            IBirdPhotoMapper birdPhotoMapper,
            ILogger<BirthPhotoController> logger)
        {
            this.logger = logger;
            this.birdPhotosService = birdPhotosService ?? throw new ArgumentNullException(nameof(birdPhotosService));
            this.birdPhotoMapper = birdPhotoMapper;
        }

        [HttpPost("UploadFiles")]
        public async Task<IActionResult> Post(NewBirdPhotoRequest request)
        {

            try
            {
                if (request == null)
                    return null;

                if (request.UserId == null || request.UserId == Guid.Empty
                    || string.IsNullOrWhiteSpace(request.ImageBase64) || string.IsNullOrWhiteSpace(request.ImageName))
                    return this.BadRequest();

                var fileExtension = request.ImageName.Split('.')[1];
                var name = $"{request.UserId}.{fileExtension}";

                var bytes = Convert.FromBase64String(request.ImageBase64.Split(',')[1]);
                var contents = new StreamContent(new MemoryStream(bytes));
                var imageStream = await contents.ReadAsStreamAsync();

                var result = await this.userImageService.UploadImageAsync(imageStream, name, request.UserId);
                if (result)
                {
                    return this.Ok();
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex);
                return this.Problem();
            }

            return this.BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> AddPhoto([FromBody] BirdPhotoRequest request)
        {
            try
            {
                if (request == null)
                    return null;

                if (request.UserId == null || request.UserId == Guid.Empty
                    || string.IsNullOrWhiteSpace(request.ImageBase64) || string.IsNullOrWhiteSpace(request.ImageName))
                    return this.BadRequest();

                var fileExtension = request.ImageName.Split('.')[1];
                var name = $"{request.UserId}.{fileExtension}";

                var bytes = Convert.FromBase64String(request.ImageBase64.Split(',')[1]);
                var contents = new StreamContent(new MemoryStream(bytes));
                var imageStream = await contents.ReadAsStreamAsync();

                var birdPhoto = this.birdPhotoMapper.Convert(request);
                if (birdPhoto == null)
                    return BadRequest();
                var result = await this.birdPhotosService.AddBirdPhoto(birdPhoto);

                if (result)
                {
                    var userResponse = this.birdPhotoMapper.Convert(birdPhoto);
                    return this.CreatedAtRoute("GetPhotoById", new { id = userResponse.Id }, userResponse);
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

        [HttpPut]
        public async Task<IActionResult> EditPhoto([FromBody] BirdPhotoRequest request)
        {
            try
            {
                var birdPhoto = this.birdPhotoMapper.Convert(request);
                if (birdPhoto == null)
                    return BadRequest();

                var result = await this.birdPhotosService.UpdateBirdPhoto(birdPhoto);

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