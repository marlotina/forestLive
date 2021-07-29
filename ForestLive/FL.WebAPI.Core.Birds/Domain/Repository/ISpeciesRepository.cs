using FL.WebAPI.Core.Birds.Domain.Dto;
using FL.WebAPI.Core.Birds.Domain.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Birds.Domain.Repository
{
    public interface ISpeciesRepository
    {
        Task<List<PostDto>> GetPostsBySpecieAsync(Guid? specieId, string orderCondition);
    }
}
