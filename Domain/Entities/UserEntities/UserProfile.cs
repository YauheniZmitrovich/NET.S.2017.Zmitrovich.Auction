using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class UserProfile
    {
        [Key]
        [ForeignKey("User")]
        public long Id { get; set; }

        public User User { get; set; }


        public System.DateTime RegistrationDate { get; set; }

        public string PhoneNumber { get; set; }


        public bool IsEmailConfirmed { get; set; }


        public long? LocalityId { get; set; }

        public Locality Locality { get; set; }
    }
}
