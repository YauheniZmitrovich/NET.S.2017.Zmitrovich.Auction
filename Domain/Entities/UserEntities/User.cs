using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class User
    {
        public User()
        {
            Lots = new HashSet<Lot>();
            Bids = new HashSet<Bid>();
            Comments = new HashSet<Comment>();
        }

        public long Id { get; set; }

        public string Name { get; set; }


        public UserRole Role { get; set; }

        public long RoleId { get; set; }


        public string Email { get; set; }

        public string Password { get; set; }


        public byte[] Avatar { get; set; }


        public UserProfile Profile { get; set; }


        public virtual ICollection<Lot> Lots { get; set; }

        public virtual ICollection<Bid> Bids { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }
    }
}