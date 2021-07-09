﻿using FL.Web.API.Core.Species.Domain.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FL.Web.API.Core.Species.Application.Services.Contracts
{
    public interface ISpeciesService
    {
        Task<List<SpecieItem>> GetAllSpecies();

        Task<List<SpecieItem>> GetAllSpeciesByLanguageId(Guid languadeId);
    }
}