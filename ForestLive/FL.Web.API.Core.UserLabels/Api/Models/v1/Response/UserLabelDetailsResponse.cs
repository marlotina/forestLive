namespace FL.WebAPI.Core.UserLabels.Api.Models.v1.Response
{
    public class UserLabelDetailsResponse
    {
        public string UserId { get; set; }

        public string Label { get; set; }

        public int PostCount { get; set; }

        public string CreationDate { get; set; }
    }
}
