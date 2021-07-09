﻿using FL.WebAPI.Core.Birds.Configuration.Contracts;
using FL.WebAPI.Core.Birds.Domain.Dto;
using FL.WebAPI.Core.Birds.Domain.Repository;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Birds.Infrastructure.Repositories
{
    public class SpecieRestRepository : ISpecieRestRepository
    {
        private readonly IBirdsConfiguration iBirdsConfiguration;

        public SpecieRestRepository(
            IBirdsConfiguration iBirdsConfiguration)
        {
            this.iBirdsConfiguration = iBirdsConfiguration;
        }

        public async Task<IEnumerable<SpecieResponse>> GetAllSpecies()
        {
            var client = new RestClient(this.iBirdsConfiguration.SpecieApiDomain);
            var restRequest = new RestRequest(this.iBirdsConfiguration.SpecieUrlService, Method.GET);

            var response = await client.ExecuteAsync<IEnumerable<SpecieResponse>>(restRequest);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<IEnumerable<SpecieResponse>>(response.Content);
            }

            return null;
        }

        public async Task<IEnumerable<SpecieResponse>> GetAllSpeciesByLanguageId(Guid languageId)
        {
            var client = new RestClient(this.iBirdsConfiguration.SpecieApiDomain);
            var restRequest = new RestRequest(this.iBirdsConfiguration.SpecieLanguageUrlService, Method.GET);

            restRequest.AddQueryParameter("languageId", languageId.ToString());

            var response = await client.ExecuteAsync<IEnumerable<SpecieResponse>>(restRequest);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                return JsonConvert.DeserializeObject<IEnumerable<SpecieResponse>>(response.Content);
            }

            return null;
        }
    }
}
