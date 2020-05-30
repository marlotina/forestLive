using NUnit.Framework;
using RestSharp;
using System;
using System.Net;
using User.WebApi.Core.Integration.Tets.Helper;
using User.WebApi.Core.Integration.Tets.v1.Model.Requests;

namespace User.WebApi.Core.Integration.Tets.v1.Account
{
    [TestFixture]
    public class ForgotPassword
    {      
        [Test]
        public void ForgotPasswordUser()
        {
            var client = new RestClient(UserHelper.API_URL_USER);
            var request = new RestRequest("api/v1/Account/ForgotPassword", Method.POST);

            var forgotPasswordRequest = new ForgotPasswordRequest
            {
                Email = "garagardotest201911190319@gmail.com"
            };

            request.AddJsonBody(forgotPasswordRequest);
            var response = client.Execute(request);

            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
        }

        [Test]
        public void ForgotPasswordUserBadEmail()
        {
            var client = new RestClient(UserHelper.API_URL_USER);
            var request = new RestRequest("api/v1/Account/ForgotPassword", Method.POST);

            var forgotPasswordRequest = new ForgotPasswordRequest
            {
                Email = "garagardosasasaatest201911190319@gmail.com"
            };

            request.AddJsonBody(forgotPasswordRequest);
            var response = client.Execute(request);

            Assert.IsTrue(response.StatusCode == HttpStatusCode.BadRequest);
        }

        [Test]
        public void ForgotPasswordUserNullEmail()
        {
            var client = new RestClient(UserHelper.API_URL_USER);
            var request = new RestRequest("api/v1/Account/ForgotPassword", Method.POST);

            var forgotPasswordRequest = new ForgotPasswordRequest
            {
                Email = null
            };

            request.AddJsonBody(forgotPasswordRequest);
            var response = client.Execute(request);

            Assert.IsTrue(response.StatusCode == HttpStatusCode.BadRequest);
        }

        [Test]
        public void ForgotPasswordUserEmptyEmail()
        {
            var client = new RestClient(UserHelper.API_URL_USER);
            var request = new RestRequest("api/v1/Account/ForgotPassword", Method.POST);

            var forgotPasswordRequest = new ForgotPasswordRequest
            {
                Email = string.Empty
            };

            request.AddJsonBody(forgotPasswordRequest);
            var response = client.Execute(request);

            Assert.IsTrue(response.StatusCode == HttpStatusCode.BadRequest);
        }
    }
}