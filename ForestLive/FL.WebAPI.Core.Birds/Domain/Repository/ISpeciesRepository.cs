using FL.WebAPI.Core.Birds.Domain.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Birds.Domain.Repository
{
    public interface ISpeciesRepository
    {
        List<SpecieItem> GetSpeciesByLanguage(string languageId);
    }
}
