using FL.Web.API.Core.Bird.Pending.Domain.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.Web.API.Core.Bird.Pending.Domain.Repository
{
    public interface ISearchMapRepository
    {
        Task<List<BirdPost>> GetPostByRadio(double latitude, double longitude, int meters);

        Task<List<BirdPost>> GetSpeciePostByRadio(double latitude, double longitude, int meters, Guid specieId);

        Task<BirdPost> GetPostsByPostId(Guid postId, Guid specieId);
    }
}
