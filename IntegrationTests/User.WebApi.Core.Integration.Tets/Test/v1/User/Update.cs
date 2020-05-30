using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using User.WebApi.Core.Integration.Tets.Helper;
using User.WebApi.Core.Integration.Tets.v1.Model.Requests;
using User.WebApi.Core.Integration.Tets.v1.Model.Response;

namespace User.WebApi.Core.Integration.Tets.v1.User
{
    [TestFixture]
    public class Update
    {
        [Test]
        public void UpdateUserOk()
        {
            var user = GetUser();
            var requestUpdate = new UserRequest() {
                Id = user.Id,
                Name = DateTime.Now.ToLongTimeString(),
                Surname = user.Surname,
                UrlWebSite = user.UrlWebSite,
                IsCompany = user.IsCompany,
                AcceptedConditions = user.AcceptedConditions,
                LanguageId = user.LanguageId,
                BirthDate = user.BirthDate,
                Description = user.Description,
                HasPhoto = user.HasPhoto,
                CityId = user.CityId,
                CountryId = user.CountryId,
                Email = user.Email,
                UserName = user.UserName
            };

            var client = new RestClient("http://localhost:50027/");
            var request = new RestRequest("api/v1/User/", Method.PUT);
            request.AddJsonBody(requestUpdate);

            var response = client.Execute(request);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);

            var newUser = GetUser();
            Assert.IsTrue(newUser.Name == requestUpdate.Name);
        }

        [Test]
        public void UpdateUserWithoutUserName()
        {
            var user = GetUser();
            var requestUpdate = new UserRequest()
            {
                Id = user.Id,
                Name = DateTime.Now.ToLongTimeString(),
                Surname = user.Surname,
                UrlWebSite = user.UrlWebSite,
                IsCompany = user.IsCompany,
                AcceptedConditions = user.AcceptedConditions,
                LanguageId = user.LanguageId,
                BirthDate = user.BirthDate,
                Description = user.Description,
                HasPhoto = user.HasPhoto,
                CityId = user.CityId,
                CountryId = user.CountryId,
                Email = user.Email
            };

            var client = new RestClient(UserHelper.API_URL_USER);
            var request = new RestRequest("api/v1/User/", Method.PUT);
            request.AddJsonBody(requestUpdate);

            var response = client.Execute(request);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.BadRequest);
        }

        [Test]
        public void UpdateUserWithoutId()
        {
            var user = GetUser();
            var requestUpdate = new UserRequest()
            {
                Name = DateTime.Now.ToLongTimeString(),
                Surname = user.Surname,
                UrlWebSite = user.UrlWebSite,
                IsCompany = user.IsCompany,
                AcceptedConditions = user.AcceptedConditions,
                LanguageId = user.LanguageId,
                BirthDate = user.BirthDate,
                Description = user.Description,
                HasPhoto = user.HasPhoto,
                CityId = user.CityId,
                CountryId = user.CountryId,
                Email = user.Email,
                UserName = user.UserName
            };

            var client = new RestClient(UserHelper.API_URL_USER);
            var request = new RestRequest("api/v1/User/", Method.PUT);
            request.AddJsonBody(requestUpdate);

            var response = client.Execute(request);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.BadRequest);
        }

        [Test]
        public void UpdateUserWithoutEmail()
        {
            var user = GetUser();
            var requestUpdate = new UserRequest()
            {
                Id = user.Id,
                Name = DateTime.Now.ToLongTimeString(),
                Surname = user.Surname,
                UrlWebSite = user.UrlWebSite,
                IsCompany = user.IsCompany,
                AcceptedConditions = user.AcceptedConditions,
                LanguageId = user.LanguageId,
                BirthDate = user.BirthDate,
                Description = user.Description,
                HasPhoto = user.HasPhoto,
                CityId = user.CityId,
                CountryId = user.CountryId,
                UserName = user.UserName
            };

            var client = new RestClient(UserHelper.API_URL_USER);
            var request = new RestRequest("api/v1/User/", Method.PUT);
            request.AddJsonBody(requestUpdate);

            var response = client.Execute(request);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.BadRequest);
        }

        [Test]
        public void UpdateUserWithoutData()
        {
            var client = new RestClient(UserHelper.API_URL_USER);
            var request = new RestRequest("api/v1/User/", Method.PUT);
            request.AddJsonBody(null);

            var response = client.Execute(request);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.BadRequest);
        }

        private UserResponse GetUser()
        {
            var client = new RestClient(UserHelper.API_URL_USER);
            var request = new RestRequest("api/v1/User/UserGetById?id=CB72A74C-87DE-4FF2-AA0B-08D76D426E04", Method.GET);

            var response = client.Execute<UserResponse>(request);
            var user = JsonConvert.DeserializeObject<UserResponse>(response.Content);

            return user;
        }
    }
}
