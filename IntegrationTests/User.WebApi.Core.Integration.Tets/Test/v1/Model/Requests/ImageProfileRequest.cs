using System;
using System.Collections.Generic;
using System.Text;

namespace User.WebApi.Core.Integration.Tets.Test.v1.Model.Requests
{
    public class ImageProfileRequest
    {
        public string ImageBase64 { get; set; }

        public string ImageName { get; set; }

        public Guid UserId { get; set; }
    }
}
