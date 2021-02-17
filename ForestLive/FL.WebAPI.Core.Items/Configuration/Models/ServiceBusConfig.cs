namespace FL.WebAPI.Core.Items.Configuration.Models
{
    public class ServiceBusConfig
    {
        public string ConnectionString { get; set; }

        public string TopicPostCreated { get; set; }

        public string TopicPostDeleted { get; set; }

        public string TopicVoteCreated { get; set; }

        public string TopicVotedeleted { get; set; }
    }
}
