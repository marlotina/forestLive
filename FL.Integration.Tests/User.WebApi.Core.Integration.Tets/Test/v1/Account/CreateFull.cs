using Newtonsoft.Json;
using NUnit.Framework;
using RestSharp;
using System;
using System.Net;
using User.WebApi.Core.Integration.Tets.Test.v1.Model.Requests;
using User.WebApi.Core.Integration.Tets.v1.Model.Requests;

namespace User.WebApi.Core.Integration.Tets.v1.Account
{
    [TestFixture]
    public class CreateFull
    {
        [Test]
        [Category("QA")]
        public void MegaCreateConfirmLoginForgotResetLoginPassowrdUser()
        {
            var createUser = this.CreateUser();
            var userRegisterResponse = this.CreateUser(createUser);
            
            this.LoginWithoutConfirm(createUser.Email, createUser.Password);
            this.ConfirmUser(userRegisterResponse.token, userRegisterResponse.id);
            this.Login(createUser.Email, createUser.Password);

            var forgotPasswordToken = this.ForgotPasswordUser(createUser.Email);
            var newPassword = "sdfsdfsdfsd";
            this.ResetPasswordUser(forgotPasswordToken, userRegisterResponse.id, newPassword);
            this.Login(createUser.Email, newPassword);
        }

        private RegisterRequest CreateUser()
        {
            var createUser = new RegisterRequest
            {
                Email = string.Format("garagardotest{0}@gmail.com", DateTime.UtcNow.ToString("yyyyMMddmmss")),
                Password = "test",
                LanguageId = Guid.NewGuid(),
                UserName = string.Format("garagardotest{0}@gmail.com", DateTime.UtcNow.ToString("yyyyMMddmmss"))
            };

            return createUser;
        }

        private RegisterTestResponse CreateUser(RegisterRequest createUser)
        {
            var client = new RestClient("http://localhost:50027/");
            var request = new RestRequest("api/v1/AccoutTestToLoko/RegisterToLoko", Method.POST);
            request.AddJsonBody(createUser);

            var response = client.Execute(request);
            var userRegisterResponse = JsonConvert.DeserializeObject<RegisterTestResponse>(response.Content);

            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
            return userRegisterResponse;
        }
        
        private void ConfirmUser(string confirmToken, string userId)
        {
            var client = new RestClient("http://localhost:50027/");
            var request = new RestRequest("api/v1/Account/ConfirmEmail", Method.POST);
            
            var confirmRequest = new ConfirmEmailRequest()
            {
                Code = confirmToken,
                UserId = userId
            };

            request.AddJsonBody(confirmRequest);
            var response = client.Execute(request);

            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
        }
        
        private void Login(string email, string password)
        {
            var client = new RestClient("http://localhost:50027/");
            var request = new RestRequest("api/v1/UserAuth/authenticate", Method.POST);

            var registerRequest = new RegisterRequest();
            registerRequest.Email = email;
            registerRequest.Password = password;

            request.AddJsonBody(registerRequest);
            var response = client.Execute(request);

            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
        }

        private void LoginWithoutConfirm(string email, string password)
        {
            var client = new RestClient("http://localhost:50027/");
            var request = new RestRequest("api/v1/UserAuth/authenticate", Method.POST);

            var registerRequest = new RegisterRequest();
            registerRequest.Email = email;
            registerRequest.Password = password;

            request.AddJsonBody(registerRequest);
            var response = client.Execute(request);

            Assert.IsTrue(response.StatusCode == HttpStatusCode.BadRequest);
        }

        private string ForgotPasswordUser(string email)
        {
            var client = new RestClient("http://localhost:50027/");
            var request = new RestRequest("api/v1/AccoutTestToLoko/ForgotPasswordToLoko", Method.POST);

            var forgotPasswordRequest = new ForgotPasswordRequest();
            forgotPasswordRequest.Email = email;
            request.AddJsonBody(forgotPasswordRequest);

            var response = client.Execute(request);
            var userForgotResponse = JsonConvert.DeserializeObject<string>(response.Content);

            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);

            return userForgotResponse;
        }

        private void ResetPasswordUser(string code, string userId, string password)
        {
            var client = new RestClient("http://localhost:50027/");
            var request = new RestRequest("api/v1/Account/ResetPassword", Method.POST);
            
            var resetPasswordRequest = new ResetPasswordRequest();
            resetPasswordRequest.UserId = Guid.Parse(userId);
            resetPasswordRequest.NewPassword = password;
            resetPasswordRequest.Code = code;

            request.AddJsonBody(resetPasswordRequest);
            var response = client.Execute(request);

            Assert.IsTrue(response.StatusCode == HttpStatusCode.OK);
        }
    }
}