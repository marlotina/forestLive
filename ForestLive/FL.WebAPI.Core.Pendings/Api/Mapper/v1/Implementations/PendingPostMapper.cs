using FL.WebAPI.Core.Pendings.Api.Mapper.v1.Contracts;
using FL.WebAPI.Core.Pendings.Api.Models.v1.Response;
using FL.WebAPI.Core.Pendings.Domain.Entities;

namespace FL.WebAPI.Core.Pendings.Api.Mapper.v1.Implementations
{
    public class PendingPostMapper : IPendingPostMapper
    {
        public BirdPendingResponse Convert(BirdPost source)
        {
            var result = default(BirdPendingResponse);
            if (source != null)
            {
                result = new BirdPendingResponse()
                {
                    Id = source.Id,
                    PostId = source.PostId,
                    Title = source.Title,
                    ImageUrl = source.ImageUrl,
                    AltImage = source.AltImage,
                    CreateDate = source.CreateDate,
                    UserId = source.UserId,
                    BirdSpecie = source.SpecieName,
                    SpecieId = source.SpecieId,
                    CommentsCount = source.CommentsCount,
                    ObservationDate = source.ObservationDate.ToString("dd/MM/yyyy")
                };
            }
            return result;
        }
    }
}
