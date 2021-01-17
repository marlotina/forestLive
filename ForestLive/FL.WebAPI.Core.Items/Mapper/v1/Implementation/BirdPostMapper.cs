using FL.WebAPI.Core.Items.Api.Mapper.v1.Contracts;
using FL.WebAPI.Core.Items.Api.Models.v1.Request;
using FL.WebAPI.Core.Items.Api.Models.v1.Response;
using FL.WebAPI.Core.Items.Domain.Entities;
using Microsoft.Azure.Cosmos.Spatial;

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
                    //CreateDate = request.ce
                    UserId = source.UserId,
                    UserName = source.UserName, //???
                    Labels = source.Labels,
                    Type = source.Type,
                    Location = new Point(double.Parse(source.Longitude), double.Parse(source.Latitude))
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
                    //Id = request
                    Title = source.Title,
                    Text = source.Text,
                    //CreateDate = request.cre
                    UserId = source.UserId,
                    UserName = source.UserName, //???
                    Labels = source.Labels,
                    Type = source.Type,
                    Latitude = source.Location.Position.Latitude.ToString(),
                    Longitude = source.Location.Position.Longitude.ToString()
                };
            }
            return result;
        }
    }
}
