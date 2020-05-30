using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using System;
using System.Net;
using User.WebApi.Core.Integration.Tets.Helper;
using User.WebApi.Core.Integration.Tets.v1.Model.Requests;

namespace User.WebApi.Core.Integration.Tets.v1.Account
{
    public class Login
    {
        [SetUp]
        public void Setup()
        {

        }
        [Test]
        public void LoginOk()
        {
            var client = new RestClient(UserHelper.API_URL_USER);
            var request = new RestRequest("api/v1/Account/login", Method.POST);

            var registerRequest = new RegisterRequest
            {
                Email = UserHelper.USER_EMAIL,
                Password = "123qwe"
            };

            request.AddJsonBody(registerRequest);
            var response = client.Execute(request);

            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);

            //var authResponse = JsonConvert.DeserializeObject<AuthResponse>(response.Content);
            //Assert.IsTrue(!string.IsNullOrWhiteSpace(authResponse.Token));
        }

        [Test]
        public void LoginBadEmail()
        {
            var client = new RestClient(UserHelper.API_URL_USER);
            var request = new RestRequest("api/v1/UserAuth/authenticate", Method.POST);

            var registerRequest = new RegisterRequest
            {
                Email = "garagardotest201911190319@gmail.com",
                Password = "test"
            };

            request.AddJsonBody(registerRequest);
            var response = client.Execute(request);

            Assert.IsTrue(response.StatusCode == HttpStatusCode.BadRequest);
        }

        [Test]
        public void LoginBadPassword()
        {
            var client = new RestClient(UserHelper.API_URL_USER);
            var request = new RestRequest("api/v1/UserAuth/authenticate", Method.POST);

            var registerRequest = new RegisterRequest
            {
                Email = "garagardotest201911190319@gmail.com",
                Password = "tesdasdsat"
            };

            request.AddJsonBody(registerRequest);
            var response = client.Execute(request);

            Assert.IsTrue(response.StatusCode == HttpStatusCode.BadRequest);
        }

        [Test]
        public void LogiEmpty()
        {
            var client = new RestClient(UserHelper.API_URL_USER);
            var request = new RestRequest("api/v1/UserAuth/authenticate", Method.POST);

            var registerRequest = new RegisterRequest
            {
                Email = string.Empty,
                Password = string.Empty
            };

            request.AddJsonBody(registerRequest);
            var response = client.Execute(request);

            Assert.IsTrue(response.StatusCode == HttpStatusCode.BadRequest);
        }
    }
}