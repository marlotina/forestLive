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
        private readonly ISearchMapRepository searchMapRepository; 

        public SearchMapService(ISearchMapRepository searchMapRepository)
        {
            this.searchMapRepository = searchMapRepository;
        }

        public async Task<BirdPost> GetPostByPostId(string postId, string specieId)
        {
            try
            {
                var post = await this.searchMapRepository.GetPostsByPostId(postId, specieId);

                return post;
            }
            catch (Exception ex)
            {
            }

            return null;
        }

        public async Task<List<BirdPost>> GetPostsByRadio(double latitude, double longitude, int zoom, Guid? specieId)
        {
            var meters = 200000;
            if (specieId.HasValue && specieId.Value != Guid.Empty)
            {
                return await this.searchMapRepository.GetSpeciePostByRadio(latitude, longitude, meters, specieId.Value);
            }
            else {

                return await this.searchMapRepository.GetPostByRadio(latitude, longitude, meters);
            }
        }
    }
}
