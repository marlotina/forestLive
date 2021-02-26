using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Birds.Application.Services.Contracts
{
    public interface IBirdSpeciesService
    {
        List<Bird> GetBirdBySpecie(Guid birdSpecieId);
    }
}
