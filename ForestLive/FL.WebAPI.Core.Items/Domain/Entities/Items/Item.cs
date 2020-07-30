using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Items.Domain.Entities.Items
{
    public class Item
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Text { get; set; }

        public DateTime CreateDate { get; set; }

        public Guid UserId { get; set; }

        public int LikesCount { get; set; }

        public string UserName { get; set; }

        public string[] Labels { get; set; }

        public string TreeName { get; set; }

        public string Type { get; set; }
    }
}
