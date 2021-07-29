using FL.Web.API.Core.Post.Interactions.Application.Mapper.Contracts;
using FL.Web.API.Core.Post.Interactions.Domain.Dto;
using FL.Web.API.Core.Post.Interactions.Models.v1.Response;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FL.Web.API.Core.Post.Interactions.Mapper.v1.Implementation
{
    public class PostDataMapper : IPostDataMapper
    {
        public IEnumerable<CommentResponse> ConvertAll(IEnumerable<PostDetails> source, IEnumerable<PostDetails> postVotes = null)
        {
            var response = new List<CommentResponse>();

            var comentList = source.Select(x => this.Convert(x, postVotes));
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

        private CommentResponse Convert(PostDetails source, IEnumerable<PostDetails> postVotes = null)
        {
            var result = default(CommentResponse);
            try
            {
                if (source != null)
                {
                    var vote = postVotes?.FirstOrDefault(x => x.CommentId == Guid.Parse(source.Id));
                    result = new CommentResponse()
                    {
                        Id = Guid.Parse(source.Id),
                        Text = source.Text,
                        UserId = source.UserId,
                        ParentId = source.ParentId,
                        VoteCount = source.VoteCount,
                        HasVote = false,
                        CreationDate = source.CreationDate.ToString("dd/MM/yyyy hh:mm"),
                        Replies = new List<CommentResponse>()
                    };

                    if (vote != null)
                    {
                        result.HasVote = true;
                        result.VoteId = vote.Id;
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return result;
        }
    }
}
