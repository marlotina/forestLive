namespace FL.Web.Api.Core.Votes.Configuration.Models
{
    public class ServiceBusConfig
    {
        public string ConnectionString { get; set; }

        public string TopicPost { get; set; }

        public string TopicVote { get; set; }

        public string TopicComment { get; set; }
    }
}
