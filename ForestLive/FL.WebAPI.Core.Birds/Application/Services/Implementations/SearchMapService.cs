using FL.WebAPI.Core.Birds.Application.Services.Contracts;
using FL.WebAPI.Core.Birds.Domain.Model;
using FL.WebAPI.Core.Birds.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Birds.Application.Services.Implementations
{
    public class SearchMapService : ISearchMapService
    {
        private readonly ISearchMapRepository iSearchMapRepository; 

        public SearchMapService(ISearchMapRepository iSearchMapRepository)
        {
            this.iSearchMapRepository = iSearchMapRepository;
        }

        public async Task<BirdPost> GetPostModalInfo(Guid postId, Guid specieId)
        {
            try
            {
                return await this.iSearchMapRepository.GetPostsByPostId(postId, specieId);
            }
            catch (Exception ex)
            {
            }

            return null;
        }

        public async Task<List<BirdPost>> GetPostsByRadio(double latitude, double longitude, int zoom, Guid? specieId)
        {
            var meters = 200000;
            if (specieId.HasValue)
            {
                return await this.iSearchMapRepository.GetSpeciePostByRadio(latitude, longitude, meters, specieId.Value);
            }
            else {

                return await this.iSearchMapRepository.GetPostByRadio(latitude, longitude, meters);
            }
        }
    }
}
