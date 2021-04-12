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
            var meters = this.GetRadio(zoom);
            if (specieId.HasValue)
            {
                return await this.iSearchMapRepository.GetSpeciePostByRadio(latitude, longitude, meters, specieId.Value);
            }
            else {

                return await this.iSearchMapRepository.GetPostByRadio(latitude, longitude, meters);
            }
        }

        private int GetRadio(int zoom) 
        {
            switch (zoom)
            {
                case 6: return 960000;
                case 7: return 480000;
                case 8: return 240000;
                case 9: return 120000;
                case 10: return 60000;
                case 11: return 30000;
                case 12: return 15000;
                case 13: return 7500;
                case 14: return 3500;
                case 15: return 1700;
                case 16: return 850;
                case 17: return 500;
                case 18: return 250;
                default: return 100;
            }
        }
    }
}
