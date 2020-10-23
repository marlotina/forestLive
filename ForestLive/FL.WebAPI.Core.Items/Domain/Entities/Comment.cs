using System;

namespace FL.WebAPI.Core.Items.Domain.Entities
{
    public class Comment
    {
        public Guid Id { get; set; }

        public string Label { get; set; }

        public string Type { get; set; }

        public CommentProperty MyProperty { get; set; }
    }
}
