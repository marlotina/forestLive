using FL.WebAPI.Core.Items.Api.Mapper.v1.Contracts;
using FL.WebAPI.Core.Items.Api.Models.v1.Request;
using FL.WebAPI.Core.Items.Api.Models.v1.Response;
using FL.WebAPI.Core.Items.Domain.Entities.Items;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Items.Api.Mapper.v1.Implementation
{
    public class BirdPhotoMapper : IBirdPhotoMapper
    {
        public Item Convert(BirdPhotoRequest source)
        {
            var result = default(Item);
            if (source != null)
            {
                result = new Item()
                {
                    //Id = request
                    Title = source.Title,
                    Text = source.Text,
                    //CreateDate = request.ce
                    UserId = source.UserId,
                    UserName = source.UserName, //???
                    Labels = source.Labels,
                    TreeName = source.TreeName,
                    Type = source.Type,
                    Latitude = source.Latitude,
                    Longitude = source.Longitude
                };
            }
            return result;
        }

        public BirdPhotoResponse Convert(Item source)
        {
            var result = default(BirdPhotoResponse);
            if (source != null)
            {
                result = new BirdPhotoResponse()
                {
                    //Id = request
                    Title = source.Title,
                    Text = source.Text,
                    //CreateDate = request.ce
                    UserId = source.UserId,
                    UserName = source.UserName, //???
                    Labels = source.Labels,
                    TreeName = source.TreeName,
                    Type = source.Type,
                    Latitude = source.Latitude,
                    Longitude = source.Longitude
                };
            }
            return result;
        }
    }
}
