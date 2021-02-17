﻿using FL.Functions.BirdPost.Model;
using System;
using System.Threading.Tasks;

namespace FL.Functions.BirdPost.Services
{
    public interface IPostCosmosDbService
    {
        Task CreatePostInUserAsync(BirdPostDto post);

        Task DeleteItemAsync(BirdPostDto deletePostRequest);
    }
}
