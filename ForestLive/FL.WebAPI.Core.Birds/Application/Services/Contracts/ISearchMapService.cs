using FL.WebAPI.Core.Birds.Domain.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Birds.Application.Services.Contracts
{
    public interface ISearchMapService
    {
        Task<List<BirdPost>> GetPostsByRadio(double latitude, double longitude, int zoom, Guid? specieId);

        Task<BirdPost> GetPostByPostId(string postId, string specieId);
    }
}
