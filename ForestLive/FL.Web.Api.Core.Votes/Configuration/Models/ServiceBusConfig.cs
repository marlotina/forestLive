namespace FL.Web.Api.Core.Votes.Configuration.Models
{
    public class ServiceBusConfig
    {
        public string ConnectionString { get; set; }

        public string TopicVote { get; set; }
    }
}
