using FL.Infrastructure.Standard.Contracts;
using FL.LogTrace.Contracts.Standard;
using FL.Pereza.Helpers.Standard.Enums;
using FL.WebAPI.Core.Items.Application.Exceptions;
using FL.WebAPI.Core.Items.Configuration.Contracts;
using FL.WebAPI.Core.Items.Domain.Entities;
using FL.WebAPI.Core.Items.Domain.Enum;
using FL.WebAPI.Core.Items.Domain.Repositories;
using FL.WebAPI.Core.Items.Infrastructure.ServiceBus.Contracts;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Items.Application.Services.Implementations
{
    public class ManagePostService
    {
        private readonly IPostConfiguration iPostConfiguration;
        private readonly IBlobContainerRepository iBlobContainerRepository;
        private readonly IPostRepository iPostRepository;
        private readonly ILogger<PostService> iLogger;
        private readonly IServiceBusPostTopicSender<BirdPost> iServiceBusCreatedPostTopic;
        private readonly IServiceBusLabelTopicSender<List<UserLabel>> iServiceBusLabelTopicSender;

        public ManagePostService(IPostConfiguration iPostConfiguration,
            IBlobContainerRepository iBlobContainerRepository,
            IPostRepository iPostRepository,
            IServiceBusPostTopicSender<BirdPost> iServiceBusCreatedPostTopic,
            IServiceBusLabelTopicSender<List<UserLabel>> iServiceBusLabelTopicSender,
            ILogger<PostService> iLogger)
        {
            this.iBlobContainerRepository = iBlobContainerRepository;
            this.iPostConfiguration = iPostConfiguration;
            this.iPostRepository = iPostRepository;
            this.iServiceBusCreatedPostTopic = iServiceBusCreatedPostTopic;
            this.iServiceBusLabelTopicSender = iServiceBusLabelTopicSender;
            this.iLogger = iLogger;
        }

        public async Task<BirdPost> AddBirdPost(BirdPost birdPost, byte[] imageBytes, string imageName, bool isPost)
        {
            try
            {
                var folder = birdPost.UserId + "/" + DateTime.Now.ToString("ddMMyyyhhmm");
                var result = await this.SavePhoto(imageBytes, imageName, folder);

                if (result)
                {
                    var postId = Guid.NewGuid();
                    birdPost.PostId = postId;
                    birdPost.Id = postId;
                    birdPost.Type = ItemHelper.POST_TYPE;
                    birdPost.VoteCount = 0;
                    birdPost.CommentCount = 0;
                    birdPost.CreationDate = DateTime.UtcNow;
                    birdPost.ImageUrl = folder + "/" + imageName;
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

                    if (birdPost.Labels != null && birdPost.Labels.Any())
                    {
                        var dtoLabels = this.GetListLabel(birdPost.Labels.ToList(), birdPost.UserId);
                        await this.iServiceBusLabelTopicSender.SendMessage(dtoLabels, TopicHelper.LABEL_USER_LABEL_CREATED);
                    }

                    var post = await this.iPostRepository.CreatePostAsync(birdPost);
                    await this.iServiceBusCreatedPostTopic.SendMessage(birdPost, TopicHelper.LABEL_POST_CREATED);

                    return post;
                }
            }
            catch (Exception ex)
            {
                this.iLogger.LogError(ex, "AddBirdItem");
            }

            return null;
        }

        public async Task<bool> DeleteBirdPost(Guid birdPostId, string userId)
        {
            try
            {
                var post = await this.iPostRepository.GetPostAsync(birdPostId);
                if (userId == post.UserId)
                {
                    var image = post.ImageUrl;
                    var partitionKey = post.PostId.ToString();
                    var id = post.Id;
                    var userPartitionKey = post.UserId;
                    var result = await this.iBlobContainerRepository.DeleteFileToStorage(image, this.iPostConfiguration.BirdPhotoContainer);

                    if (result)
                    {
                        await this.iPostRepository.DeletePostAsync(id, partitionKey);
                        await this.iServiceBusCreatedPostTopic.SendMessage(post, TopicHelper.LABEL_POST_DELETED);
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

        public async Task<BirdPost> GetBirdPost(Guid birdPostId)
        {
            try
            {
                return await this.iPostRepository.GetPostAsync(birdPostId);
            }
            catch (Exception ex)
            {
                this.iLogger.LogError(ex, "GetBirdItem");
            }

            return null;
        }


        private async Task<bool> SavePhoto(byte[] imageBytes, string imageName, string folder)
        {
            var contents = new StreamContent(new MemoryStream(imageBytes));
            var imageStream = await contents.ReadAsStreamAsync();

            var stream = new MemoryStream();
            Image image = Image.FromStream(imageStream);
            Image thumb = image.GetThumbnailImage(image.Width, image.Height, () => false, IntPtr.Zero);
            thumb.Save(stream, ImageFormat.Jpeg);
            stream.Position = 0;

            return await this.iBlobContainerRepository.UploadFileToStorage(stream, imageName, this.iPostConfiguration.BirdPhotoContainer, folder);
        }

        private List<UserLabel> GetListLabel(List<string> labels, string userId)
        {
            var listLabel = new List<UserLabel>();

            foreach (var label in labels)
            {
                listLabel.Add(new UserLabel()
                {
                    Type = "label",
                    UserId = userId,
                    Id = label,
                    PostCount = 1,
                    CreationDate = DateTime.UtcNow

                });
            }

            return listLabel;
        }
    }
}
