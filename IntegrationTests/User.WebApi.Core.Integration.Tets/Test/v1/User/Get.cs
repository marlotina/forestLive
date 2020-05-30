using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using User.WebApi.Core.Integration.Tets.Helper;
using User.WebApi.Core.Integration.Tets.v1.Model.Response;

namespace User.WebApi.Core.Integration.Tets.v1.User
{
    [TestFixture]
    public class Get
    {
        [Test]
        public void GetUserById()
        {
            var client = new RestClient(UserHelper.API_URL_USER);
            var request = new RestRequest("api/v1/User/UserGetById?id=CB72A74C-87DE-4FF2-AA0B-08D76D426E04", Method.GET);

            var response = client.Execute<UserResponse>(request);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);

            var user = JsonConvert.DeserializeObject<UserResponse>(response.Content);
            Assert.IsTrue(!string.IsNullOrWhiteSpace(user.Name));
        }

        [Test]
        public void GetUserByIdNull()
        {
            var client = new RestClient(UserHelper.API_URL_USER);
            var request = new RestRequest("api/v1/User/UserGetById", Method.GET);

            var response = client.Execute<UserResponse>(request);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.NotFound);
        }

        [Test]
        public void GetUserByIdBadId()
        {
            var client = new RestClient(UserHelper.API_URL_USER);
            var request = new RestRequest("api/v1/User/UserGetById?id=1d134297-b070-4149-a2e0-c2643de48f95", Method.GET);

            var response = client.Execute<UserResponse>(request);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.NotFound);
        }

        [Test]
        public void GetUserByIdBadParam()
        {
            var client = new RestClient(UserHelper.API_URL_USER);
            var request = new RestRequest("api/v1/User/UserGetById?id=CB725555S-87DE-4FF2-", Method.GET);

            var response = client.Execute<UserResponse>(request);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.BadRequest);
        }

        [Test]
        public void GetUserFindByEmailOk()
        {
            var client = new RestClient(UserHelper.API_URL_USER);
            var request = new RestRequest($"api/v1/User/UserFindByEmail?email={UserHelper.USER_EMAIL}", Method.GET);

            var response = client.Execute<UserResponse>(request);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);

            var userList = JsonConvert.DeserializeObject<List<UserResponse>>(response.Content);
            Assert.IsTrue(userList.Any());
        }

        [Test]
        public void GetUserFindByEmailNotFound()
        {
            var client = new RestClient(UserHelper.API_URL_USER);
            var request = new RestRequest("api/v1/User/UserFindByEmail?email=dafsfsasdt201911190319@gmail.com", Method.GET);

            var response = client.Execute<UserResponse>(request);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.NotFound);
        }

        [Test]
        public void GetUserFindByEmailEmpty()
        {
            var client = new RestClient(UserHelper.API_URL_USER);
            var request = new RestRequest("api/v1/User/UserFindByEmail?email=", Method.GET);

            var response = client.Execute<UserResponse>(request);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.BadRequest);
        }
        
        [Test]
        public void GetUserFindByEmailNothing()
        {
            var client = new RestClient(UserHelper.API_URL_USER);
            var request = new RestRequest("api/v1/User/UserFindByEmail", Method.GET);

            var response = client.Execute<UserResponse>(request);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.BadRequest);
        }
    }
}
