using FL.Web.API.Core.User.Posts.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Birds.Domain.Repository
{
    public interface ISearchMapRepository
    {
        Task<List<PointPostDto>> GetPostByRadio(double latitude, double longitude, int meters);

        Task<List<PointPostDto>> GetSpeciePostByRadio(double latitude, double longitude, int meters, Guid specieId);
    }
}
