using FL.Pereza.Helpers.Standard.Images;
using FL.Web.API.Core.Post.Interactions.Domain.Dto;
using FL.Web.API.Core.Post.Interactions.Domain.Entities;
using FL.Web.API.Core.Post.Interactions.Mapper.v1.Contracts;
using FL.Web.API.Core.Post.Interactions.Models.v1.Request;
using FL.Web.API.Core.Post.Interactions.Models.v1.Response;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FL.Web.API.Core.Post.Interactions.Mapper.v1.Implementation
{
    public class CommentMapper : ICommentMapper
    {
        public CommentDto Convert(CommentRequest source)
        {
            var result = default(CommentDto);
            if (source != null)
            {
                result = new CommentDto()
                {
                    PostId = source.PostId,
                    SpecieId = source.SpecieId,
                    UserId = source.UserId,
                    AuthorPostId = source.AuthorPostId,
                    TitlePost = source.TitlePost,
                    ParentId = source.ParentId,
                    Text = source.Text
                };
            }
            return result;
        }        

        public CommentResponse Convert(BirdComment source)
        {
            var result = default(CommentResponse);
            if (source != null)
            {
                result = new CommentResponse()
                {
                    Id = source.Id,
                    Text = source.Text,
                    UserId = source.UserId,
                    ParentId = source.ParentId,
                    VoteCount = source.VoteCount,
                    HasVote = false,
                    CreationDate = source.CreationDate.ToString("dd/MM/yyyy hh:mm"),
                    Replies = new List<CommentResponse>()
                };
            }
            return result;
        }
    }
}
