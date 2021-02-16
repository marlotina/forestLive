using FL.WebAPI.Core.Pendings.Api.Models.v1.Response;
using FL.WebAPI.Core.Pendings.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Pendings.Api.Mapper.v1.Contracts
{
    public interface IPendingPostMapper
    {
        BirdPendingResponse Convert(BirdPost source);
    }
}
