    using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public partial class Category
    {
        public Category()
        {
            Lots = new HashSet<Lot>();
        }

        public long Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public Category SubCategory { get; set; }


        public virtual ICollection<Lot> Lots { get; set; }
    }
}
