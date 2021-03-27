using FL.WebAPI.Core.Birds.Api.Mappers.v1.Contracts;
using FL.WebAPI.Core.Birds.Api.Models.v1.Request;
using FL.WebAPI.Core.Birds.Application.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Birds.Controllers.v1
{
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PostSpecieController : Controller
    {
        private readonly IBirdSpeciesService iBirdSpeciesService;
        private readonly IBirdSpeciePostMapper iBirdSpeciePostMapper;
        public PostSpecieController(
            IBirdSpeciesService iBirdSpeciesService,
            IBirdSpeciePostMapper iBirdSpeciePostMapper)
        {
            this.iBirdSpeciesService = iBirdSpeciesService;
            this.iBirdSpeciePostMapper = iBirdSpeciePostMapper;
        }

        [HttpPost]
        [Route("AddPost", Name = "AddPost")]
        public async Task<IActionResult> AddPost([FromBody] PostRequest request)
        {
            try
            {
                if (request == null)
                    return null;

                if (string.IsNullOrWhiteSpace(request.UserId)
                    || string.IsNullOrWhiteSpace(request.ImageData))
                    return this.BadRequest();

                var post = this.iBirdSpeciePostMapper.Convert(request);
                var bytes = Convert.FromBase64String(request.ImageData.Split(',')[1]);


                var result = await this.iBirdSpeciesService.AddBirdPost(post, bytes, request.ImageName, request.isPost);

                if (result != null)
                {
                    var postResponse = this.iBirdSpeciePostMapper.Convert(result);
                    return this.CreatedAtRoute("GetPost", new { id = postResponse.PostId }, postResponse);
                }
                else
                    return this.BadRequest();
            }
            catch (Exception ex)
            {
                //this.logger.LogError(ex);
                return this.Problem();
            }
        }

        [HttpGet, Route("GetPost", Name = "GetPost")]
        public async Task<IActionResult> GetPost(Guid postId, Guid specieId)
        {

            return this.NoContent();
        }

        [HttpDelete, Route("DeletePost", Name = "DeletePost")]
        public async Task<IActionResult> DeletePost(Guid postId, Guid specieId)
        {

            return this.NoContent();
        }

    }
}
