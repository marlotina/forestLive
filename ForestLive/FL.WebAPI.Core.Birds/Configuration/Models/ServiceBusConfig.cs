namespace FL.WebAPI.Core.Birds.Configuration.Models
{
    public class ServiceBusConfig
    {
        public string ConnectionString { get; set; }

        public string TopicPost { get; set; }

        public string TopicLabel { get; set; }

        public string TopicAssignSpecie { get; set; }
    }
}
