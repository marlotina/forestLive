using FL.Web.API.Core.Species.Domain.Model;
using System;
using System.Collections.Generic;

namespace FL.Web.API.Core.Species.Domain.Repository
{
    public interface ISpeciesRepository
    {
        List<SpecieItem> GetSpeciesByLanguage(Guid languageId);
    }
}
