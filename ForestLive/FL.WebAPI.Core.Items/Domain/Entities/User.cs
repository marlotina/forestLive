using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FL.WebAPI.Core.Items.Domain.Entities.User
{
    public class User
    {
        public Guid Id { get; set; }

        public string Label { get; set; }

        public string Type { get; set; }

        public UserProperty Properties { get; set; } 

        
    }
}
