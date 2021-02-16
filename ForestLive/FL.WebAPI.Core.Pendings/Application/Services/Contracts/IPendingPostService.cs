using FL.Pereza.Helpers.Standard.Enums;
using FL.WebAPI.Core.Pendings.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Pendings.Application.Services.Contracts
{
    public interface IPendingPostService
    {
        Task<IEnumerable<BirdPost>> GetPostByStatus(StatusSpecieEnum status);
    }
}
