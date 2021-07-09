﻿using FL.WebAPI.Core.Birds.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Birds.Application.Services.Contracts
{
    public interface ISpeciesService
    {
        Task<List<PostDto>> GetBirdBySpecie(Guid birdSpecieId, int orderBy, string languageId);


        Task<List<PostDto>> GetBirdBySpecieName(string urlSpecie, int orderBy, string languageId);

        Task<List<PostDto>> GetBirds(int orderBy, string languageId);
    }
}
