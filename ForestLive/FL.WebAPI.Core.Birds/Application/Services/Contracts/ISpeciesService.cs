using FL.WebAPI.Core.Birds.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Birds.Application.Services.Contracts
{
    public interface ISpeciesService
    {
        Task<List<PostDto>> GetBirdBySpecie(Guid birdSpecieId, int orderBy);

        Task<List<PostDto>> GetBirds(int orderBy);

        Task<List<PostHomeDto>> GetLastBirds();
    }
}
