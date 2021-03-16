using FL.WebAPI.Core.Items.Application.Services.Contracts;
using FL.WebAPI.Core.Items.Domain.Entities;
using FL.WebAPI.Core.Items.Domain.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Items.Application.Services.Implementations
{
    public class SearchMapService : ISearchMapService
    {
        private readonly ISearchMapRepository searchMapRepository; 

        public SearchMapService(ISearchMapRepository searchMapRepository)
        {
            this.searchMapRepository = searchMapRepository;
        }

        public async Task<List<BirdPost>> GetPostByRadio(double latitude, double longitude, int zoom)
        {
            var meters = 200000;
            return await this.searchMapRepository.GetPostByRadio(latitude, longitude, meters);
        }
    }
}
