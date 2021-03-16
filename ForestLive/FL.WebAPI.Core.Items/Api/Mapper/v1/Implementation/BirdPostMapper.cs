using FL.WebAPI.Core.Items.Api.Mapper.v1.Contracts;
using FL.WebAPI.Core.Items.Api.Models.v1.Response;
using FL.WebAPI.Core.Items.Domain.Entities;

namespace FL.WebAPI.Core.Items.Api.Mapper.v1.Implementation
{
    public class BirdPostMapper : IBirdPostMapper
    {
        public BirdPointResponse MapConvert(BirdPost source)
        {
            var result = default(BirdPointResponse);
            if (source != null)
            {
                result = new BirdPointResponse()
                {
                    PostId = source.PostId,
                    Title = source.Title,
                    ImageUrl = source.ImageUrl,
                    UserId = source.UserId,
                    BirdSpecie = source.SpecieName,
                    SpecieId = source.SpecieId,
                    Location = new PositionResponse
                        {
                            Lat = source.Location.Position.Latitude,
                            Lng = source.Location.Position.Longitude,
                        },
                    AltImage = source.AltImage
                };
            }

            return result;
        }
    }
}
