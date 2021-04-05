using FL.Pereza.Helpers.Standard.Images;
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
        public BirdComment Convert(CommentRequest source)
        {
            var result = default(BirdComment);
            if (source != null)
            {
                result = new BirdComment()
                {
                    PostId = source.PostId,
                    Text = source.Text,
                    UserId = source.UserId,
                    AuthorPostUserId = source.AuthorPostUserId,
                    TitlePost = source.TitlePost,
                    SpecieId = source.SpecieId,
                    CommentParentId = source.CommentParentId
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
                    ParentId = source.CommentParentId,
                    CreationDate = source.CreationDate.ToString("dd/MM/yyyy hh:mm"),
                    UserImage = source.UserId + ImageHelper.USER_PROFILE_IMAGE_EXTENSION,
                    Replies = new List<CommentResponse>()
                };
            }
            return result;
        }

        public IEnumerable<CommentResponse> Convert(IEnumerable<BirdComment> source)
        {
            var response = new List<CommentResponse>();
            
            var comentList = source.Select(x => this.Convert(x));
            var childComments = comentList.Where(x => x.ParentId != null);

            foreach (var comment in comentList.Where(x => x.ParentId == null))
            {
                response.Add(comment);
                AddRepliesLoop(comment, childComments);
            }

            return response;
        }

        private void AddRepliesLoop(CommentResponse comment, IEnumerable<CommentResponse> replyComments)
        {
            foreach (var reply in replyComments.Where(c => c.ParentId.HasValue && c.ParentId == comment.Id))
            {
                comment.Replies.Add(reply);
                AddRepliesLoop(reply, replyComments);
            }
        }
    }
}
