using FL.WebAPI.Core.Items.Api.Mapper.v1.Contracts;
using FL.WebAPI.Core.Items.Api.Models.v1.Request;
using FL.WebAPI.Core.Items.Api.Models.v1.Response;
using FL.WebAPI.Core.Items.Domain.Entities;

namespace FL.WebAPI.Core.Items.Api.Mapper.v1.Implementation
{
    public class BirdPostMapper : IBirdPostMapper
    {
        public BirdPost Convert(BirdPostRequest source)
        {
            var result = default(BirdPost);
            if (source != null)
            {
                result = new BirdPost()
                {
                    //Id = request
                    Title = source.Title,
                    Text = source.Text,
                    UserId = source.UserId,
                    UserName = source.UserName,
                    SpecieName = source.SpecieName,
                    SpecieId = source.SpecieId,
                    Labels = source.Labels,
                    Latitude = double.Parse(source.Latitude),
                    Longitude = double.Parse(source.Longitude),
                    ObservationDate = source.ObservationDate
                };
            }
            return result;
        }

        public BirdPhotoResponse Convert(BirdPost source)
        {
            var result = default(BirdPhotoResponse);
            if (source != null)
            {
                result = new BirdPhotoResponse()
                {
                    Id = source.Id,
                    Title = source.Title,
                    Text = source.Text,
                    CreateDate = source.CreateDate,
                    UserId = source.UserId,
                    UserName = source.UserName,
                    BirdSpecie = source.SpecieName,
                    SpecieId = source.SpecieId,
                    Labels = source.Labels,
                    LikesCount = source.LikesCount,
                    CommentsCount = source.CommentsCount,
                    Latitude = source.Latitude.ToString(),
                    Longitude = source.Longitude.ToString(),
                    ObservationDate = source.ObservationDate
                };
            }
            return result;
        }
    }
}
