namespace FL.Web.API.Core.Bird.Pending.Configuration.Models
{
    public class ServiceBusConfig
    {
        public string ConnectionString { get; set; }

        public string TopicPost { get; set; }

        public string TopicLabel { get; set; }
    }
}
