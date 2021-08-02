using FL.WebAPI.Core.Searchs.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Searchs.Application.Services.Contracts
{
    public interface ISpeciesService
    {
        Task<List<PostDto>> GetBirdBySpecie(Guid birdSpecieId, int orderBy, Guid? languageId);


        Task<List<PostDto>> GetBirdBySpecieName(string urlSpecie, int orderBy, Guid languageId);

        Task<List<PostDto>> GetBirds(int orderBy, Guid languageId);
    }
}
