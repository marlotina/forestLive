using FL.Infrastructure.Standard.Contracts;
using FL.LogTrace.Contracts.Standard;
using FL.Pereza.Helpers.Standard.Enums;
using FL.ServiceBus.Standard.Contracts;
using FL.WebAPI.Core.Posts.Api.Models.v1.Request;
using FL.WebAPI.Core.Posts.Application.Exceptions;
using FL.WebAPI.Core.Posts.Application.Services.Contracts;
using FL.WebAPI.Core.Posts.Configuration.Contracts;
using FL.WebAPI.Core.Posts.Domain.Entities;
using FL.WebAPI.Core.Posts.Domain.Enum;
using FL.WebAPI.Core.Posts.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Posts.Application.Services.Implementations
{
    public class ManagePostService : IManagePostService
    {
        private readonly IPostConfiguration iPostConfiguration;
        private readonly IBlobContainerRepository iBlobContainerRepository;
        private readonly ILogger<ManagePostService> iLogger;
        private readonly IServiceBusTopicSender<IEnumerable<UserLabel>> iServiceBusTopicSender;
        private readonly ISpeciesRepository iSpeciesRepository;
        private readonly IUserPostRepository iUserPostRepository;
        public ManagePostService(
            ISpeciesRepository iSpeciesRepository,
            IPostConfiguration iPostConfiguration,
            IBlobContainerRepository iBlobContainerRepository,
            IUserPostRepository iUserPostRepository,
            IServiceBusTopicSender<IEnumerable<UserLabel>> iServiceBusTopicSender,
            ILogger<ManagePostService> iLogger)
        {
            this.iBlobContainerRepository = iBlobContainerRepository;
            this.iPostConfiguration = iPostConfiguration;
            this.iServiceBusTopicSender = iServiceBusTopicSender;
            this.iSpeciesRepository = iSpeciesRepository;
            this.iUserPostRepository = iUserPostRepository;
            this.iLogger = iLogger;
        }

        public async Task<BirdPost> AddPost(BirdPost birdPost, string imageBytes, string imageName, bool isPost)
        {
            try
            {
                var result = true;
                var folder = birdPost.UserId + "/" + DateTime.Now.ToString("ddMMyyyhhmm");
                if (!string.IsNullOrEmpty(imageBytes))
                {
                    result = await this.SavePhoto(imageBytes, imageName, folder);
                    birdPost.ImageUrl = folder + "/" + imageName;
                }
                else {
                    birdPost.ImageUrl = string.Empty;
                }

                if (result)
                {
                    var postId = Guid.NewGuid();
                    birdPost.PostId = postId;
                    birdPost.Type = isPost ? ItemHelper.POST_TYPE : ItemHelper.BIRD_TYPE;
                    birdPost.Id = postId;
                    birdPost.VoteCount = 0;
                    birdPost.CommentCount = 0;
                    birdPost.CreationDate = DateTime.UtcNow;
                    birdPost.VoteCount = 0;

                    if (isPost)
                    {
                        birdPost.SpecieId = null;
                        birdPost.SpecieName = string.Empty;
                    }
                    else
                    {
                        if (birdPost.SpecieId == null || birdPost.SpecieId == Guid.Empty)
                        {
                            birdPost.SpecieId = Guid.Parse(StatusSpecie.NoSpecieId);
                            birdPost.SpecieName = string.Empty;
                        }
                    }

                    await this.iUserPostRepository.CreatePostAsync(birdPost);

                    if (birdPost.SpecieId.HasValue)
                    {
                        await this.iSpeciesRepository.CreatePostAsync(birdPost);
                    }

                    if (birdPost.Labels != null && birdPost.Labels.Any())
                    {
                        var dtoLabels = birdPost.Labels.Select(x => GetListLabel(x, birdPost.UserId));
                        await this.iServiceBusTopicSender.SendMessage(dtoLabels, TopicHelper.LABEL_USER_LABEL_CREATED);
                    }

                    return birdPost;
                }
            }
            catch (Exception ex)
            {
                this.iLogger.LogError(ex, "AddBirdItem");
            }

            return null;
        }

        public async Task<bool> DeletePost(Guid postId, string userId)
        {
            try
            {
                var post = await this.iUserPostRepository.GetPostAsync(postId, userId);
                if (userId == post.UserId)
                {
                    var result = await this.iBlobContainerRepository.DeleteFileToStorage(post.ImageUrl, this.iPostConfiguration.BirdPhotoContainer);

                    if (result)
                    {
                        var specieId = post.SpecieId;
                        var type = post.Type;
                        var postLabels = post?.Labels != null ? post.Labels.ToList() : null;

                        if (type == "bird") {
                            result = await this.iSpeciesRepository.DeletePostAsync(post.PostId, specieId.Value);
                        }
                        result = await this.iUserPostRepository.DeletePostAsync(post.PostId, userId);

                        if (result)
                        {
                            var removeLabelDto = postLabels.Select(x => GetListLabel(x, userId));
                            await this.iServiceBusTopicSender.SendMessage(removeLabelDto, TopicHelper.LABEL_POST_DELETED);
                        }
                    }

                    return true;
                }
                else
                {
                    throw new UnauthorizedRemove();
                }
            }
            catch (Exception ex)
            {
                this.iLogger.LogError(ex, "DeleteBirdItem");
            }

            return false;
        }


        private async Task<bool> SavePhoto(string imageData, string imageName, string folder)
        {

            var imageBytes = Convert.FromBase64String(imageData.Split(',')[1]);

            var contents = new StreamContent(new MemoryStream(imageBytes));
            var imageStream = await contents.ReadAsStreamAsync();

            var stream = new MemoryStream();
            Image image = Image.FromStream(imageStream);
            Image thumb = image.GetThumbnailImage(image.Width, image.Height, () => false, IntPtr.Zero);
            thumb.Save(stream, ImageFormat.Jpeg);
            stream.Position = 0;

            return await this.iBlobContainerRepository.UploadFileToStorage(stream, imageName, this.iPostConfiguration.BirdPhotoContainer, folder);
        }

        private static UserLabel GetListLabel(string label, string userId)
        {
            return new UserLabel()
            {
                Type = "label",
                UserId = userId,
                Id = label,
                PostCount = 1,
                CreationDate = DateTime.UtcNow
            };
        }

        public async Task<bool> UpdateSpeciePost(UpdateSpecieRequest request, string userId)
        {
            var post = await this.iSpeciesRepository.GetPostsAsync(request.PostId, request.OldSpecieId);
            if (userId == post.UserId)
            {
                post.SpecieId = request.SpecieId;
                post.SpecieName = request.SpecieName;
                //OJO review return values
                var response1 = await this.iUserPostRepository.UpdatePostAsync(post, userId);

                var response = await this.iSpeciesRepository.DeletePostAsync(request.PostId, request.OldSpecieId);
                if (response)
                {
                    if (await this.iSpeciesRepository.CreatePostAsync(post))
                    {
                        return true;
                    }
                }
            }
            else
            {
                throw new UnauthorizedRemove();
            }

            return false;
        }
    }
}
