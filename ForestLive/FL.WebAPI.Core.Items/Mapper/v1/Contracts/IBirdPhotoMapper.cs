using FL.WebAPI.Core.Items.Api.Models.v1.Request;
using FL.WebAPI.Core.Items.Api.Models.v1.Response;
using FL.WebAPI.Core.Items.Domain.Entities.Items;

namespace FL.WebAPI.Core.Items.Api.Mapper.v1.Contracts
{
    public interface IBirdPhotoMapper
    {
        BirdData Convert(BirdPhotoRequest source);

        BirdPhotoResponse Convert(BirdData source);
    }
}
