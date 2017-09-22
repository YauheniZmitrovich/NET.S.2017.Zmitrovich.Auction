using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public partial class UserRole
    {
        public UserRole()
        {
            Users = new HashSet<User>();
        }

        public int Id { get; set; }

        public string Name { get; set; }


        public virtual ICollection<User> Users { get; set; }
    }
}
