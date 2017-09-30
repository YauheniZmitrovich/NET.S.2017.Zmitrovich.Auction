using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public partial class Bid
    {
        [Key]
        public long Id { get; set; }


        public decimal Cost { get; set; }

        public System.DateTime DateTime { get; set; }


        [ForeignKey(nameof(Lot))]
        public long LotId { get; set; }

        [Required]
        public Lot Lot { get; set; }


        [ForeignKey(nameof(User))]
        public long UserId { get; set; }

        [Required]
        public User User { get; set; }
    }
}
