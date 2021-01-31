﻿using FL.WebAPI.Core.Items.Api.Mapper.v1.Contracts;
using FL.WebAPI.Core.Items.Api.Models.v1.Request;
using FL.WebAPI.Core.Items.Api.Models.v1.Response;
using FL.WebAPI.Core.Items.Domain.Entities;
using Microsoft.Azure.Cosmos.Spatial;

namespace FL.WebAPI.Core.Items.Api.Mapper.v1.Implementation
{
    public class BirdPostMapper : IBirdPostMapper
    {
        public BirdItem Convert(BirdItemRequest source)
        {
            var result = default(BirdItem);
            if (source != null)
            {
                result = new BirdItem()
                {
                    Title = source.Title,
                    Text = source.Text,
                    UserId = source.UserId,
                    UserName = source.UserName,
                    SpecieName = source.SpecieName,
                    SpecieId = source.SpecieId,
                    Labels = source.Labels,
                    Location = new Point(double.Parse(source.Longitude), double.Parse(source.Latitude)),
                    ObservationDate = source.ObservationDate
                };
            }
            return result;
        }

        public BirdPhotoResponse Convert(BirdItem source)
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
                    Latitude = source.Location.Position.Latitude.ToString(),
                    Longitude = source.Location.Position.Longitude.ToString(),
                    ObservationDate = source.ObservationDate,
                    SpecieConfirmed = source.ConfirmSpecie
                };
            }
            return result;
        }
    }
}
