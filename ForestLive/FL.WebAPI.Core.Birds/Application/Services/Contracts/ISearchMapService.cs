using FL.Web.API.Core.User.Posts.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Birds.Application.Services.Contracts
{
    public interface ISearchMapService
    {
        Task<List<PointPostDto>> GetPostsByRadio(double latitude, double longitude, int zoom, Guid? specieId);
    }
}
