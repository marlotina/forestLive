﻿using FL.WebAPI.Core.Items.Domain.Entities;
using FL.WebAPI.Core.Items.Models.v1.Request;
using FL.WebAPI.Core.Items.Models.v1.Response;

namespace FL.WebAPI.Core.Items.Mapper.v1.Contracts
{
    public interface ICommentMapper
    {
        Comment Convert(CommentRequest source);

        CommentResponse Convert(Comment source);
    }
}
