using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using User.WebApi.Core.Integration.Tets.Helper;
using User.WebApi.Core.Integration.Tets.Test.v1.Model.Requests;
using User.WebApi.Core.Integration.Tets.v1.Model.Response;
using static System.Net.Mime.MediaTypeNames;

namespace User.WebApi.Core.Integration.Tets.v1.User
{
    [TestFixture]
    public class Images
    {
        [Test]
        public void AddImageOK()
        {
            var client = new RestClient(UserHelper.API_URL_USER);
            var request = new RestRequest("api/v1/UserImage/UploadFiles", Method.POST);
            request.AddHeader("Accept", "application/json");
            request.AddQueryParameter("userId", "CB72A74C-87DE-4FF2-AA0B-08D76D426E04");

            var path = System.IO.Directory.GetCurrentDirectory();
            //request.AddFile("rick.jpg", path + "/images/Rick_Sanchez.png");

            byte[] imageArray = System.IO.File.ReadAllBytes(path + "/images/Rick_Sanchez.png");
            string base64ImageRepresentation = Convert.ToBase64String(imageArray);

            var registerRequest = new ImageProfileRequest
            {
                ImageBase64 = base64ImageRepresentation,
                ImageName = "test",
                UserId = new System.Guid("CB72A74C-87DE-4FF2-AA0B-08D76D426E04")
            };

            request.AddJsonBody(registerRequest);


            var response = client.Execute<UserResponse>(request);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
        }

        [Test]
        public void AddImageWithoutUserId()
        {
            var client = new RestClient(UserHelper.API_URL_USER);
            var request = new RestRequest("api/v1/UserImage/UploadFiles", Method.POST);

            var path = System.IO.Directory.GetCurrentDirectory();
            request.AddFile("rick.jpg", path + "/images/Rick_Sanchez.png");

            var response = client.Execute<UserResponse>(request);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.BadRequest);
        }

        [Test]
        public void AddImageEmpty()
        {
            var client = new RestClient(UserHelper.API_URL_USER);
            var request = new RestRequest("api/v1/UserImage/UploadFiles", Method.POST);
            
            var response = client.Execute<UserResponse>(request);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.BadRequest);
        }

        [Test]
        public void AddImageBadGuid()
        {
            var client = new RestClient(UserHelper.API_URL_USER);
            var request = new RestRequest("api/v1/UserImage/UploadFiles", Method.POST);
            request.AddQueryParameter("userId", "CB72A74C-87DE-4FF2-AA0B-0826E04");

            var response = client.Execute<UserResponse>(request);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.BadRequest);
        }

        [Test]
        public void DeleteImageOk()
        {
            var client = new RestClient(UserHelper.API_URL_USER);
            var request = new RestRequest("api/v1/UserImage/DeleteImage", Method.DELETE);
            request.AddQueryParameter("userId", "CB72A74C-87DE-4FF2-AA0B-08D76D426E04");

            var response = client.Execute<UserResponse>(request);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.NoContent);
        }

        [Test]
        public void DeleteImageWithoutId()
        {
            var client = new RestClient(UserHelper.API_URL_USER);
            var request = new RestRequest("api/v1/UserImage/DeleteImage", Method.DELETE);

            var response = client.Execute<UserResponse>(request);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.BadRequest);
        }

        [Test]
        public void DeleteImageBadId()
        {
            var client = new RestClient(UserHelper.API_URL_USER);
            var request = new RestRequest("api/v1/UserImage/DeleteImage", Method.DELETE);
            request.AddQueryParameter("userId", "CB72A74C-87DE-4FF2-AA0B-08D6E04");

            var response = client.Execute<UserResponse>(request);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.BadRequest);
        }

        [Test]
        [Ignore("")]
        public void DeleteImageNotExistId()
        {
            var client = new RestClient(UserHelper.API_URL_USER);
            var request = new RestRequest("api/v1/UserImage/DeleteImage", Method.DELETE);
            request.AddQueryParameter("userId", "1d134297-b070-4149-a2e0-c2643de48f95");

            var response = client.Execute<UserResponse>(request);
            Assert.IsTrue(response.StatusCode == HttpStatusCode.NotFound);
        }
    }
}
