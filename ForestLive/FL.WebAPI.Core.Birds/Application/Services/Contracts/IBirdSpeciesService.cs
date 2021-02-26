using FL.WebAPI.Core.Birds.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Birds.Application.Services.Contracts
{
    public interface IBirdSpeciesService
    {
        Task<List<BirdPost>> GetBirdBySpecie(Guid birdSpecieId);
    }
}
