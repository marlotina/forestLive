namespace FL.Web.API.Core.Post.Interactions.Configuration.Dto
{
    public class ServiceBusConfig
    {
        public string ConnectionString { get; set; }

        public string TopicComment { get; set; }

        public string TopicVote { get; set; }
    }
}
