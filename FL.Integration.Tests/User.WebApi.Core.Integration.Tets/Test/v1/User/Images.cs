using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using User.WebApi.Core.Integration.Tets.v1.Model.Response;

namespace User.WebApi.Core.Integration.Tets.v1.User
{
    [TestFixture]
    public class Images
    {
        [Test]
        public void AddImageOK()
        {
            var client = new RestClient("http://localhost:50027/");
            var request = new RestRequest("api/v1/UserImage/UploadFiles", Method.POST);
            request.AddQueryParameter("userId", "CB72A74C-87DE-4FF2-AA0B-08D76D426E04");

            var path = System.IO.Directory.GetCurrentDirectory();
            request.AddFile("rick.jpg", path + "/images/Rick_Sanchez.png");

            var response = client.Execute<UserResponse>(request);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
        }

        [Test]
        public void AddImageWithoutUserId()
        {
            var client = new RestClient("http://localhost:50027/");
            var request = new RestRequest("api/v1/UserImage/UploadFiles", Method.POST);

            var path = System.IO.Directory.GetCurrentDirectory();
            request.AddFile("rick.jpg", path + "/images/Rick_Sanchez.png");

            var response = client.Execute<UserResponse>(request);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.BadRequest);
        }

        [Test]
        public void AddImageEmpty()
        {
            var client = new RestClient("http://localhost:50027/");
            var request = new RestRequest("api/v1/UserImage/UploadFiles", Method.POST);
            
            var response = client.Execute<UserResponse>(request);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.BadRequest);
        }

        [Test]
        public void AddImageBadGuid()
        {
            var client = new RestClient("http://localhost:50027/");
            var request = new RestRequest("api/v1/UserImage/UploadFiles", Method.POST);
            request.AddQueryParameter("userId", "CB72A74C-87DE-4FF2-AA0B-0826E04");

            var response = client.Execute<UserResponse>(request);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.BadRequest);
        }

        [Test]
        public void DeleteImageOk()
        {
            var client = new RestClient("http://localhost:50027/");
            var request = new RestRequest("api/v1/UserImage/DeleteImage", Method.DELETE);
            request.AddQueryParameter("userId", "CB72A74C-87DE-4FF2-AA0B-08D76D426E04");

            var response = client.Execute<UserResponse>(request);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.NoContent);
        }

        [Test]
        public void DeleteImageWithoutId()
        {
            var client = new RestClient("http://localhost:50027/");
            var request = new RestRequest("api/v1/UserImage/DeleteImage", Method.DELETE);

            var response = client.Execute<UserResponse>(request);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.BadRequest);
        }

        [Test]
        public void DeleteImageBadId()
        {
            var client = new RestClient("http://localhost:50027/");
            var request = new RestRequest("api/v1/UserImage/DeleteImage", Method.DELETE);
            request.AddQueryParameter("userId", "CB72A74C-87DE-4FF2-AA0B-08D6E04");

            var response = client.Execute<UserResponse>(request);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.BadRequest);
        }

        [Test]
        [Ignore("")]
        public void DeleteImageNotExistId()
        {
            var client = new RestClient("http://localhost:50027/");
            var request = new RestRequest("api/v1/UserImage/DeleteImage", Method.DELETE);
            request.AddQueryParameter("userId", "1d134297-b070-4149-a2e0-c2643de48f95");

            var response = client.Execute<UserResponse>(request);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.NotFound);
        }
    }
}
