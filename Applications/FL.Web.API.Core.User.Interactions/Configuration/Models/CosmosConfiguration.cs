namespace FL.Web.API.Core.User.Interactions.Configuration.Models
{
    public class CosmosConfiguration
    {
        public string CosmosDatabaseId { get; set; }

        public string CosmosVoteContainer { get; set; }

        public string CosmosCommentContainer { get; set; }

        public string CosmosCommentVoteContainer { get; set; }

        public string CosmosUserContainer { get; set; }
    }
}
