﻿using System;

namespace FL.WebAPI.Core.Birds.Domain.Dto
{
    public class SpecieInfoResponse
    {
        public Guid SpecieId { get; set; }

        public string ScienceName { get; set; }

        public string UrlSpecie { get; set; }
    }
}
