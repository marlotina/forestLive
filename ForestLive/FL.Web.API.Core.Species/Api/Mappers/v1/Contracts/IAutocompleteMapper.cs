﻿using FL.Web.API.Core.Species.Api.Models.v1.Response;
using FL.Web.API.Core.Species.Domain.Model;

namespace FL.Web.API.Core.Species.Api.Mappers.v1.Contracts
{
    public interface IAutocompleteMapper
    {
        AutocompleteResponse Convert (SpecieItem source);
    }
}