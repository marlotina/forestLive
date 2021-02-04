﻿using FL.WebAPI.Core.Items.Domain.Entities;
using FL.WebAPI.Core.Items.Mapper.v1.Contracts;
using FL.WebAPI.Core.Items.Models.v1.Request;
using FL.WebAPI.Core.Items.Models.v1.Response;

namespace FL.WebAPI.Core.Items.Mapper.v1.Implementation
{
    public class CommentMapper : ICommentMapper
    {
        public ItemComment Convert(CommentRequest source)
        {
            var result = default(ItemComment);
            if (source != null)
            {
                result = new ItemComment()
                {
                    ItemId = source.ItemId,
                    Text = source.Text,
                    UserId = source.UserId
                };
            }
            return result;
        }

        public CommentResponse Convert(ItemComment source)
        {
            var result = default(CommentResponse);
            if (source != null)
            {
                result = new CommentResponse()
                {
                    Id = source.Id,
                    Text = source.Text,
                    UserId = source.UserId,
                    CreateDate = source.CreateDate,
                    ItemId = source.ItemId
                };
            }
            return result;
        }
    }
}
