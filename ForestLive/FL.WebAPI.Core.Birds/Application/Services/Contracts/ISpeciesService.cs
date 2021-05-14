using FL.WebAPI.Core.Birds.Domain.Dto;
using FL.WebAPI.Core.Birds.Domain.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Birds.Application.Services.Contracts
{
    public interface ISpeciesService
    {
        Task<List<PostDto>> GetBirdBySpecie(Guid birdSpecieId, int orderBy);

        Task<IEnumerable<VotePostResponse>> GetVoteByUserId(IEnumerable<Guid> listPost, string webUserId);

        Task<List<PostDto>> GetBirds(int orderBy);


        Task<List<PostHomeDto>> GetLastBirds();

        Task<BirdPost> GetPost(Guid postId, Guid specieId);
    }
}
