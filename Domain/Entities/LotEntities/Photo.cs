using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public partial class Photo
    {
        public long Id { get; set; }

        public byte[] Content { get; set; }


        public long LotId { get; set; }

        public virtual Lot Lot { get; set; }
    }
}
