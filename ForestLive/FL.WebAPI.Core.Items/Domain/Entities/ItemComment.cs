using System;

namespace FL.WebAPI.Core.Items.Domain.Entities
{
    public class ItemComment
    {
        public Guid Id { get; set; }

        public Guid ItemId { get; set; }

        public string Type { get; set; }

        public string Text { get; set; }

        public DateTime CreateDate { get; set; }

        public Guid UserId { get; set; }

        public string UserName { get; set; }
    }
}
