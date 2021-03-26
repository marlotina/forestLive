using FL.Infrastructure.Standard.Contracts;
using FL.LogTrace.Contracts.Standard;
using FL.Pereza.Helpers.Standard.Enums;
using FL.WebAPI.Core.Items.Application.Exceptions;
using FL.WebAPI.Core.Items.Application.Services.Contracts;
using FL.WebAPI.Core.Items.Configuration.Contracts;
using FL.WebAPI.Core.Items.Domain.Dto;
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
    public class PostService : IPostService
    {
        private readonly IPostConfiguration postConfiguration;
        private readonly IBlobContainerRepository blobContainerRepository;
        private readonly IPostRepository postRepository;
        private readonly ILogger<PostService> logger;
        private readonly IUserVotesRepository userVotesRepository;
        private readonly IServiceBusPostTopicSender<BirdPost> serviceBusCreatedPostTopic;
        private readonly IServiceBusLabelTopicSender<List<UserLabel>> serviceBusLabelTopicSender;

        public PostService(IPostConfiguration postConfiguration,
            IBlobContainerRepository blobContainerRepository,
            IPostRepository postRepository,
            IUserVotesRepository userVotesRepository,
            IServiceBusPostTopicSender<BirdPost> serviceBusCreatedPostTopic,
            IServiceBusLabelTopicSender<List<UserLabel>> serviceBusLabelTopicSender,
            ILogger<PostService> logger)
        {
            this.userVotesRepository = userVotesRepository;
            this.blobContainerRepository = blobContainerRepository;
            this.postConfiguration = postConfiguration;
            this.postRepository = postRepository;
            this.serviceBusCreatedPostTopic = serviceBusCreatedPostTopic;
            this.serviceBusLabelTopicSender = serviceBusLabelTopicSender;
            this.logger = logger;
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
                        await this.serviceBusLabelTopicSender.SendMessage(dtoLabels, TopicHelper.LABEL_USER_LABEL_CREATED);
                    }

                    var post = await this.postRepository.CreatePostAsync(birdPost);
                    await this.serviceBusCreatedPostTopic.SendMessage(birdPost, TopicHelper.LABEL_POST_CREATED);
                    
                    return post;
                }
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "AddBirdItem");
            }

            return null;
        }

        public async Task<bool> DeleteBirdPost(Guid birdPostId, string userId)
        {
            try
            {
                var post = await this.postRepository.GetPostAsync(birdPostId);
                if (userId == post.UserId)
                {
                    var image = post.ImageUrl;
                    var partitionKey = post.PostId.ToString();
                    var id = post.Id;
                    var userPartitionKey = post.UserId;
                    var result = await this.blobContainerRepository.DeleteFileToStorage(image, this.postConfiguration.BirdPhotoContainer);

                    if (result)
                    {
                        await this.postRepository.DeletePostAsync(id, partitionKey);
                        await this.serviceBusCreatedPostTopic.SendMessage(post, TopicHelper.LABEL_POST_DELETED);
                    }

                    return true;
                }
                else 
                {
                    throw new UnauthorizedRemove();
                }

                return false;
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "DeleteBirdItem");
            }

            return false;
        }

        public async Task<BirdPost> GetBirdPost(Guid birdPostId)
        {
            try
            {
                return await this.postRepository.GetPostAsync(birdPostId);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "GetBirdItem");
            }

            return null;
        }

        public async Task<List<BirdComment>> GetCommentByPost(Guid postId)
        {
            try
            {
                return await this.postRepository.GetCommentsAsync(postId);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "GetCommentByItem");
            }

            return new List<BirdComment>();
        }

        public async Task<IEnumerable<VotePostResponse>> GetVoteByUserId(IEnumerable<Guid> listPost, string webUserId)
        {
            try
            {
                if (webUserId != null)
                {
                    return await this.userVotesRepository.GetUserVoteByPosts(listPost, webUserId);
                }

                return new List<VotePostResponse>();
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "GetBlogPostsForUserId");
                return null;
            }
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

        public async Task<List<PostDto>> GetPosts(int orderBy)
        {
            try
            {
                var order = this.GerOrderCondition(orderBy);
                return await this.postRepository.GetPostsAsync(order);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex, "GetBirdItem");
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

            return await this.blobContainerRepository.UploadFileToStorage(stream, imageName, this.postConfiguration.BirdPhotoContainer, folder);
        }

        private string GerOrderCondition(int orderBy)
        {
            switch (orderBy)
            {
                case 1: return "creationDate DESC";
                case 2: return "voteCount DESC";
                case 3: return "commentCount DESC";
                default: return "creationDate DESC";
            }
        }
    }
}
