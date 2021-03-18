using FL.WebAPI.Core.Birds.Api.Mappers.v1.Implementations;
using FL.WebAPI.Core.Birds.Api.Models.v1.Response;
using FL.WebAPI.Core.Birds.Domain.Model;

namespace FL.WebAPI.Core.Birds.Api.Mappers.v1.Contracts
{
    public interface IBirdSpeciePostMapper
    {
        BirdSpeciePostResponse Convert(BirdPost source);

        BirdMapResponse MapConvert(BirdPost source);

        ModalBirdPostResponse ModalConvert(BirdPost source);
    }
}
