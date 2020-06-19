using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Items.Domain.Entities.Items
{
    public class ItemLike
    {
        public Guid Id { get; set; }

        public Guid ItemId { get; set; }

        public Guid UserId { get; set; }

        public string UserName { get; set; }
        
        public DateTime CreateDate { get; set; }

        public string Type { get; set; }
    }
}
