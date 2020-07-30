using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Items.Domain.Entities.User
{
    public class UserItem
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public DateTime CreateDate { get; set; }

        public Guid UserId { get; set; }

        public int LikesCount { get; set; }

        public int PointsCount { get; set; }

        public string UserName { get; set; }

        public string[] Labels { get; set; }

        public string TreeName { get; set; }

        public Guid SharedId { get; set; }

        public string Type { get; set; }
    }
}
