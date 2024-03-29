﻿using System;

namespace FL.WebAPI.Core.Searchs.Api.Models.v1.Response
{
    public class ModalPostResponse
    {
        public Guid PostId { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public string Type { get; set; }

        public string ImageUrl { get; set; }

        public string AltImage { get; set; }

        public string UserId { get; set; }

        public string BirdSpecie { get; set; }

        public Guid? SpecieId { get; set; }

        public string ObservationDate { get; set; }

        public object SpecieUrl { get; set; }
    }
}
