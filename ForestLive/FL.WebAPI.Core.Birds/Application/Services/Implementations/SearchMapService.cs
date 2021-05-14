using FL.Web.API.Core.User.Posts.Domain.Dto;
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

        public async Task<List<PointPostDto>> GetPostsByRadio(double latitude, double longitude, int zoom, Guid? specieId)
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
            return zoom switch
            {
                6 => 960000,
                7 => 480000,
                8 => 240000,
                9 => 120000,
                10 => 60000,
                11 => 30000,
                12 => 15000,
                13 => 7500,
                14 => 3500,
                15 => 1700,
                16 => 850,
                17 => 500,
                18 => 250,
                _ => 100,
            };
        }
    }
}
