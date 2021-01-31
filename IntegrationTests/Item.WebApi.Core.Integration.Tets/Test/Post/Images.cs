using Item.WebApi.Core.Integration.Tets.Helper;
using Item.WebApi.Core.Integration.Tets.Test.v1.Model;
using NUnit.Framework;
using RestSharp;
using System;
using System.Net;

namespace Item.WebApi.Core.Integration.Tets.v1.Post
{
    [TestFixture]
    public class Images
    {
        [Test]
        public void AddImageOK()
        {
            var client = new RestClient(ItemHelper.API_URL_POST);
            var request = new RestRequest("api/v1/BirdPost/AddPost", Method.POST);

            var path = System.IO.Directory.GetCurrentDirectory();
            byte[] imageArray = System.IO.File.ReadAllBytes(path + "/images/Pajaro-elefante.jpg");
            string base64ImageRepresentation = Convert.ToBase64String(imageArray);

            var registerRequest = new BirdPostRequest
            {
                ImageData = base64ImageRepresentation,
                Title = "Petirrojo en una rama",
                Text = "Un petirrojo cazado y posando al lado de un merendero en el bosque",
                UserId = new Guid("CB72A74C-87DE-4FF2-AA0B-08D76D426E04"),
                UserName = "iñaki",
                Latitude = "49.40768",
                Longitude = "8.69079",
                SpecieName = "Petirrojo",
                SpecieId = new Guid("CB72A74C-87DE-4FF2-AA0B-08D76D426E04"),
                Labels = new string[] { "Heidelberg", "Forest" }
            };

            request.AddJsonBody(registerRequest);
            var response = client.Execute(request);

            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
        }
    }
}
