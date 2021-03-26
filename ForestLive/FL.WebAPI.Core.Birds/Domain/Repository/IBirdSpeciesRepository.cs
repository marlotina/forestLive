using FL.WebAPI.Core.Birds.Domain.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Birds.Domain.Repository
{
    public interface IBirdSpeciesRepository
    {
        Task<List<PostDto>> GetBirdsPostsBySpecieId(string specieId, string orderCondition);
    }
}
