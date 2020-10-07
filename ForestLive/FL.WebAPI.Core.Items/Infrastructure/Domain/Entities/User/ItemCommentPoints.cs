using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Items.Domain.Entities.User
{
    public class ItemCommentPoints
    {
        public Guid Id { get; set; }

        public Guid CommentId { get; set; }

        public Guid UserId { get; set; }

        public string Type { get; set; }
    }
}
