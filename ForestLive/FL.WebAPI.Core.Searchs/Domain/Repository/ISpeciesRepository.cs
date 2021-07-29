using FL.WebAPI.Core.Searchs.Domain.Dto;
using FL.WebAPI.Core.Searchs.Domain.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Searchs.Domain.Repository
{
    public interface ISpeciesRepository
    {
        Task<List<PostDto>> GetPostsBySpecieAsync(Guid? specieId, string orderCondition);
    }
}
