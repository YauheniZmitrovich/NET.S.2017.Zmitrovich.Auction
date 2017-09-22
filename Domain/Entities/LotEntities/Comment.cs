using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public partial class Comment
    {
        public long Id { get; set; }


        public string Content { get; set; }

        public System.DateTime Time { get; set; }


        public long LotId { get; set; }

        public virtual Lot Lot { get; set; }


        [ForeignKey(nameof(User))]
        public long UserId { get; set; }

        public virtual User User { get; set; }
    }
}
