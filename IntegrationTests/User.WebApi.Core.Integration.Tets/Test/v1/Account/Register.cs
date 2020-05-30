using NUnit.Framework;
using RestSharp;
using System;
using System.Net;
using User.WebApi.Core.Integration.Tets.Helper;
using User.WebApi.Core.Integration.Tets.v1.Model.Requests;

namespace User.WebApi.Core.Integration.Tets.v1.Account
{
    [TestFixture]
    public class Register
    {
        [Test]
        //[Category("QA")]
        public void RegistreUserzdcdOK()
        {
            Assert.IsTrue(1==1);
        }

        [Test]
        //[Category("QA")]
        public void RegistreUserOK()
        {
            var client = new RestClient(UserHelper.API_URL_USER);
            var request = new RestRequest("api/v1/Account/Register", Method.POST);

            var createUser = new RegisterRequest
            {
                Email = string.Format("garagardotest{0}@gmail.com", DateTime.UtcNow.ToString("yyyyMMddmmss")),
                Password = "test",
                LanguageId = Guid.NewGuid()
            };
            createUser.UserName = createUser.Email;

            request.AddJsonBody(createUser);
            var response = client.Execute(request);

            Assert.IsTrue(response.StatusCode == HttpStatusCode.Created);
        }

        [Test]
        public void RegistreUserWithoutEmail()
        {
            var client = new RestClient(UserHelper.API_URL_USER);
            var request = new RestRequest("api/v1/Account/Register", Method.POST);

            var createUser = new RegisterRequest
            {
                Email = string.Empty,
                Password = "test",
                LanguageId = Guid.NewGuid(),
                UserName = DateTime.UtcNow.ToString("yyyyMMddmmss")
            };

            request.AddJsonBody(createUser);
            var response = client.Execute(request);

            Assert.IsTrue(response.StatusCode == HttpStatusCode.BadRequest);
        }

        [Test]
        public void RegistreUserWithoutUserName()
        {
            var client = new RestClient(UserHelper.API_URL_USER);
            var request = new RestRequest("api/v1/Account/Register", Method.POST);

            var createUser = new RegisterRequest
            {
                Email = string.Format("garagardotest{0}@gmail.com", DateTime.UtcNow.ToString("yyyyMMddmmss")),
                Password = "test",
                LanguageId = Guid.NewGuid(),
                UserName = string.Empty
            };

            request.AddJsonBody(createUser);
            var response = client.Execute(request);

            Assert.IsTrue(response.StatusCode == HttpStatusCode.BadRequest);
        }

        [Test]
        public void RegistreUserWithoutPassword()
        {
            var client = new RestClient(UserHelper.API_URL_USER);
            var request = new RestRequest("api/v1/Account/Register", Method.POST);

            var createUser = new RegisterRequest
            {
                Email = string.Format("garagardotest{0}@gmail.com", DateTime.UtcNow.ToString("yyyyMMddmmss")),
                Password = string.Empty,
                LanguageId = Guid.NewGuid()
            };
            createUser.UserName = createUser.Email;

            request.AddJsonBody(createUser);
            var response = client.Execute(request);

            Assert.IsTrue(response.StatusCode == HttpStatusCode.BadRequest);
        }
    }
}