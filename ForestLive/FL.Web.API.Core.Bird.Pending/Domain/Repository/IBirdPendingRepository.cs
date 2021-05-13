using FL.Web.API.Core.Bird.Pending.Domain.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.Web.API.Core.Bird.Pending.Domain.Repository
{
    public interface IBirdPendingRepository
    {
        Task<List<PostDto>> GetAllSpecieAsync(string orderCondition);
    }
}
