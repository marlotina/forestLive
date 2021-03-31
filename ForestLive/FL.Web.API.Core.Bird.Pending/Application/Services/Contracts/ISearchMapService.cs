using FL.Web.API.Core.Bird.Pending.Domain.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.Web.API.Core.Bird.Pending.Application.Services.Contracts
{
    public interface ISearchMapService
    {
        Task<List<BirdPost>> GetPostsByRadio(double latitude, double longitude, int zoom, Guid? specieId);

        Task<BirdPost> GetPostModalInfo(Guid postId, Guid specieId);
    }
}
