﻿using FL.Infrastructure.Standard.Contracts;
using FL.LogTrace.Contracts.Standard;
using FL.Pereza.Helpers.Standard.Enums;
using FL.WebAPI.Core.Items.Api.Models.v1.Request;
using FL.WebAPI.Core.Items.Application.Exceptions;
using FL.WebAPI.Core.Items.Application.Services.Contracts;
using FL.WebAPI.Core.Items.Configuration.Contracts;
using FL.WebAPI.Core.Items.Domain.Dto;
using FL.WebAPI.Core.Items.Domain.Entities;
using FL.WebAPI.Core.Items.Domain.Enum;
using FL.WebAPI.Core.Items.Domain.Repositories;
using FL.WebAPI.Core.Items.Domain.Repository;
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
    public class ManagePostService : IManagePostService
    {
        private readonly IPostConfiguration iPostConfiguration;
        private readonly IBlobContainerRepository iBlobContainerRepository;
        private readonly IPostRepository iPostRepository;
        private readonly ILogger<ManagePostService> iLogger;
        private readonly IServiceBusLabelTopicSender<IEnumerable<UserLabel>> iServiceBusLabelTopicSender;
        private readonly IServiceBusLabelTopicSender<IEnumerable<RemoveLabelDto>> iServiceBusDeleteLabelTopicSender;
        private readonly IServiceBusAssignSpecieTopicSender<BirdPost> iServiceBusAssignSpecieTopicSender;
        private readonly ISpeciesRepository iSpeciesRepository;
        private readonly IUserPostRepository iUserPostRepository;
        public ManagePostService(
            ISpeciesRepository iSpeciesRepository,
            IPostConfiguration iPostConfiguration,
            IBlobContainerRepository iBlobContainerRepository,
            IPostRepository iPostRepository,
            IUserPostRepository iUserPostRepository,
            IServiceBusLabelTopicSender<IEnumerable<UserLabel>> iServiceBusLabelTopicSender,
            IServiceBusLabelTopicSender<IEnumerable<RemoveLabelDto>> iServiceBusDeleteLabelTopicSender,
            IServiceBusAssignSpecieTopicSender<BirdPost> iServiceBusAssignSpecieTopicSender,
            ILogger<ManagePostService> iLogger)
        {
            this.iBlobContainerRepository = iBlobContainerRepository;
            this.iPostConfiguration = iPostConfiguration;
            this.iPostRepository = iPostRepository;
            this.iServiceBusLabelTopicSender = iServiceBusLabelTopicSender;
            this.iServiceBusAssignSpecieTopicSender = iServiceBusAssignSpecieTopicSender;
            this.iServiceBusDeleteLabelTopicSender = iServiceBusDeleteLabelTopicSender;
            this.iSpeciesRepository = iSpeciesRepository;
            this.iUserPostRepository = iUserPostRepository;
            this.iLogger = iLogger;
        }

        public async Task<BirdPost> AddBirdPost(BirdPost birdPost, string imageBytes, string imageName, bool isPost)
        {
            try
            {
                var result = true;
                var folder = birdPost.UserId + "/" + DateTime.Now.ToString("ddMMyyyhhmm");
                if (!string.IsNullOrEmpty(imageBytes))
                {
                    result = await this.SavePhoto(imageBytes, imageName, folder);
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

                    await this.iPostRepository.CreatePostAsync(birdPost);
                    await this.iUserPostRepository.CreatePostAsync(birdPost);

                    if (birdPost.SpecieId.HasValue)
                    {
                        await this.iSpeciesRepository.CreatePostAsync(birdPost);
                    }

                    if (birdPost.Labels != null && birdPost.Labels.Any())
                    {
                        var dtoLabels = birdPost.Labels.Select(x => GetListLabel(x, birdPost.UserId));
                        await this.iServiceBusLabelTopicSender.SendMessage(dtoLabels, TopicHelper.LABEL_USER_LABEL_CREATED);
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

        public async Task<bool> DeleteBirdPost(Guid birdPostId, string userId)
        {
            try
            {
                var post = await this.iPostRepository.GetPostAsync(birdPostId);
                if (userId == post.UserId)
                {
                    var result = await this.iBlobContainerRepository.DeleteFileToStorage(post.ImageUrl, this.iPostConfiguration.BirdPhotoContainer);

                    if (result)
                    {
                        var specieId = post.SpecieId;
                        var type = post.Type;
                        var postLabels = post?.Labels.ToList();
                        post.ImageUrl = string.Empty;
                        post.UserId = null;
                        post.Title = string.Empty;
                        post.SpecieId = null;
                        post.SpecieName = string.Empty;
                        post.ObservationDate = null;
                        post.Location = null;
                        post.Labels = null;
                        post.Text = string.Empty;
                        post.Type = "deleted";
                        post.VoteCount = 0;
                        post.CommentCount = 0;
                        post.AltImage = string.Empty;
                        result = await this.iPostRepository.UpdatePostAsync(post);
                        if (type == "bird") {
                            result = await this.iSpeciesRepository.DeletePostAsync(post.PostId, specieId.Value);
                        }
                        result = await this.iUserPostRepository.DeletePostAsync(post.PostId, userId);
                        result = await this.iPostRepository.DeletePostVotestAsync(post.PostId);

                        if (result)
                        {
                            var removeLabelDto = postLabels.Select(x => GetListDeleteLabel(x, userId));
                            await this.iServiceBusDeleteLabelTopicSender.SendMessage(removeLabelDto, TopicHelper.LABEL_POST_DELETED);
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


        public async Task<BirdPost> GetBirdPost(Guid birdPostId)
        {
            return await this.iPostRepository.GetPostAsync(birdPostId);
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
        private static RemoveLabelDto GetListDeleteLabel(string label, string userId)
        {
            return new RemoveLabelDto()
                {
                    Label = label,
                    UserId = userId
                };
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

        public async Task<bool> UpdateSpecieToPost(UpdateSpecieRequest request, string userId)
        {
            var post = await this.iSpeciesRepository.GetPostsAsync(request.PostId, request.OldSpecieId);
            if (userId == post.UserId)
            {
                post.SpecieId = request.SpecieId;
                post.SpecieName = request.SpecieName;
                post.Type = "bird";
                var response = await this.iSpeciesRepository.DeletePostAsync(request.PostId, request.OldSpecieId);
                if (response)
                {
                    if (await this.iSpeciesRepository.CreatePostAsync(post))
                    {
                        await this.iServiceBusAssignSpecieTopicSender.SendMessage(post, TopicHelper.LABEL_UPDATE_SPECIE);
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
