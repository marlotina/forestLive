﻿using FL.Web.API.Core.Post.Interactions.Domain.Entities;
using FL.Web.API.Core.Post.Interactions.Models.v1.Request;
using FL.Web.API.Core.Post.Interactions.Models.v1.Response;

namespace FL.Web.API.Core.Post.Interactions.Mapper.v1.Contracts
{
    public interface IBirdCommentMapper
    {
        BirdComment Convert(BirdCommentRequest source);

        BirdCommentResponse Convert(BirdComment source);
    }
}
