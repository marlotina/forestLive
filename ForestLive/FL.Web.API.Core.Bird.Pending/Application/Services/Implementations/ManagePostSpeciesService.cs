using FL.Infrastructure.Standard.Contracts;
using FL.Pereza.Helpers.Standard.Enums;
using FL.Web.API.Core.Bird.Pending.Application.Exceptions;
using FL.Web.API.Core.Bird.Pending.Application.Services.Contracts;
using FL.Web.API.Core.Bird.Pending.Configuration.Contracts;
using FL.Web.API.Core.Bird.Pending.Domain.Dto;
using FL.Web.API.Core.Bird.Pending.Domain.Enum;
using FL.Web.API.Core.Bird.Pending.Domain.Model;
using FL.Web.API.Core.Bird.Pending.Domain.Repository;
using FL.Web.API.Core.Bird.Pending.Infrastructure.ServiceBus.Contracts;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace FL.Web.API.Core.Bird.Pending.Application.Services.Implementations
{
    public class ManagePostSpeciesService : IManagePostSpeciesService
    {
        private readonly IBirdPendingRepository iBirdSpeciesRepository;
        private readonly IBlobContainerRepository iBlobContainerRepository;
        private readonly IBirdPendingConfiguration iBirdsConfiguration;
        private readonly IServiceBusPostTopicSender<BirdPost> iServiceBusCreatedPostTopic;
        private readonly IServiceBusLabelTopicSender<List<UserLabel>> iServiceBusLabelTopicSender;

        public ManagePostSpeciesService(
            IBlobContainerRepository iBlobContainerRepository,
            IBirdPendingConfiguration iBirdsConfiguration,
            IBirdPendingRepository iBirdSpeciesRepository,
            IServiceBusPostTopicSender<BirdPost> iServiceBusCreatedPostTopic,
            IServiceBusLabelTopicSender<List<UserLabel>> iServiceBusLabelTopicSender)
        {
            this.iBirdsConfiguration = iBirdsConfiguration;
            this.iBirdSpeciesRepository = iBirdSpeciesRepository;
            this.iBlobContainerRepository = iBlobContainerRepository;
            this.iServiceBusCreatedPostTopic = iServiceBusCreatedPostTopic;
            this.iServiceBusLabelTopicSender = iServiceBusLabelTopicSender;
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
                    birdPost.Type = ItemHelper.BIRD_TYPE;
                    birdPost.VoteCount = 0;
                    birdPost.CommentCount = 0;
                    birdPost.CreationDate = DateTime.UtcNow;
                    birdPost.ImageUrl = folder + "/" + imageName;
                    birdPost.VoteCount = 0;
                    
                    if (birdPost.SpecieId == null || birdPost.SpecieId == Guid.Empty)
                    {
                        birdPost.SpecieId = Guid.Parse(StatusSpecie.NoSpecieId);
                        birdPost.SpecieName = string.Empty;
                    }
                    

                    if (birdPost.Labels != null && birdPost.Labels.Any())
                    {
                        var dtoLabels = this.GetListLabel(birdPost.Labels.ToList(), birdPost.UserId);
                        await this.iServiceBusLabelTopicSender.SendMessage(dtoLabels, TopicHelper.LABEL_USER_LABEL_CREATED);
                    }

                    var post = await this.iBirdSpeciesRepository.CreatePostAsync(birdPost);
                    await this.iServiceBusCreatedPostTopic.SendMessage(birdPost, TopicHelper.LABEL_POST_CREATED);

                    return post;
                }
            }
            catch (Exception ex)
            {
                //this.logger.LogError(ex, "AddBirdItem");
            }

            return null;
        }

        public async Task<bool> DeleteBirdPost(Guid postId, string userId)
        {
            try
            {
                var post = await this.iBirdSpeciesRepository.GetPostsAsync(postId);
                if (userId == post.UserId)
                {
                    var image = post.ImageUrl;
                    var partitionKey = post.PostId.ToString();
                    var id = post.Id;
                    var userPartitionKey = post.UserId;
                    var result = await this.iBlobContainerRepository.DeleteFileToStorage(image, this.iBirdsConfiguration.BirdPhotoContainer);

                    if (result)
                    {
                        await this.iBirdSpeciesRepository.DeletePostAsync(postId);
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
                //this.iLogger.LogError(ex, "DeleteBirdItem");
            }

            return false;
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

        private async Task<bool> SavePhoto(byte[] imageBytes, string imageName, string folder)
        {
            var contents = new StreamContent(new MemoryStream(imageBytes));
            var imageStream = await contents.ReadAsStreamAsync();

            var stream = new MemoryStream();
            Image image = Image.FromStream(imageStream);
            Image thumb = image.GetThumbnailImage(image.Width, image.Height, () => false, IntPtr.Zero);
            thumb.Save(stream, ImageFormat.Jpeg);
            stream.Position = 0;

            return await this.iBlobContainerRepository.UploadFileToStorage(stream, imageName, this.iBirdsConfiguration.BirdPhotoContainer, folder);
        }
    }
}
