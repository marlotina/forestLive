using FL.WebAPI.Core.Birds.Domain.Model;
using System;
using System.Collections.Generic;

namespace FL.WebAPI.Core.Birds.Domain.Repository
{
    public interface ISpeciesRepository
    {
        List<SpecieItem> GetSpeciesByLanguage(Guid languageId);
    }
}
