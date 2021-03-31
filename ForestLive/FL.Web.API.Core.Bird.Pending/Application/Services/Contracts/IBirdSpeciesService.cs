using FL.Web.API.Core.Bird.Pending.Domain.Dto;
using FL.Web.API.Core.Bird.Pending.Domain.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.Web.API.Core.Bird.Pending.Application.Services.Contracts
{
    public interface IBirdSpeciesService
    {
        Task<List<PostDto>> GetBirdBySpecie(Guid birdSpecieId, int orderBy);

        Task<IEnumerable<VotePostResponse>> GetVoteByUserId(IEnumerable<Guid> listPost, string webUserId);

        Task<List<PostDto>> GetBirds(int orderBy);

        Task<BirdPost> GetPost(Guid postId, Guid specieId);
    }
}
