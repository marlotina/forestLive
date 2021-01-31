using FL.WebAPI.Core.Items.Api.Models.v1.Request;
using FL.WebAPI.Core.Items.Api.Models.v1.Response;
using FL.WebAPI.Core.Items.Domain.Entities;

namespace FL.WebAPI.Core.Items.Api.Mapper.v1.Contracts
{
    public interface IBirdPostMapper
    {
        BirdItem Convert(BirdItemRequest source);

        BirdPhotoResponse Convert(BirdItem source);
    }
}
