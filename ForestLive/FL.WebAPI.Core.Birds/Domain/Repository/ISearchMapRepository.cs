using FL.WebAPI.Core.Birds.Domain.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Birds.Domain.Repository
{
    public interface ISearchMapRepository
    {
        Task<List<BirdPost>> GetPostByRadio(double latitude, double longitude, int meters);

        Task<List<BirdPost>> GetSpeciePostByRadio(double latitude, double longitude, int meters, Guid specieId);

        Task<BirdPost> GetPostsByPostId(string postId, string specieId);
    }
}
