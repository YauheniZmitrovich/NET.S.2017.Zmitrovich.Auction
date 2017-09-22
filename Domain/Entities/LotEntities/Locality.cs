using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public partial class Locality
    {
        public Locality()
        {
            Users = new HashSet<UserProfile>();
        }

        public long Id { get; set; }

        public string Name { get; set; }


        public string Region { get; set; }

        public string District { get; set; }

        public string Type { get; set; }


        public virtual ICollection<UserProfile> Users { get; set; }
    }
}
