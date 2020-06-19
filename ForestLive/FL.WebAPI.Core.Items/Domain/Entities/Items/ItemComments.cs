using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Items.Domain.Entities.Items
{
    public class ItemComments
    {
        public Guid Id { get; set; }

        public Guid ItemId { get; set; }

        public Guid UserId { get; set; }

        public string Text { get; set; }

        public Guid ParentId { get; set; }

        public string UserName { get; set; }

        public string UserPhoto { get; set; }

        public DateTime CreateDate { get; set; }

        public string Type { get; set; }
    }
}
