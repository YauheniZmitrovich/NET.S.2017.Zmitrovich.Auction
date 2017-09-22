using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Lot
    {
        #region Constructor

        public Lot()
        {
            Categories = new HashSet<Category>();
            Photos = new HashSet<Photo>();
            Comments = new HashSet<Comment>();
        }

        #endregion


        #region Main info

        [Key]
        public long Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public long ViewCount { get; set; }

        #endregion


        #region Price info

        public decimal CurrentPrice { get; set; }

        public decimal? GoldPrice { get; set; }

        #endregion


        #region Time info

        public System.DateTime UploadDate { get; set; }

        public System.DateTime EndOfTranding { get; set; }

        public bool IsEnded { get; set; }

        #endregion


        #region Foreign keys

        public long UserId { get; set; }

        public User Owner { get; set; }


        public virtual ICollection<Bid> Bids { get; set; }

        public virtual ICollection<Photo> Photos { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Category> Categories { get; set; }

        #endregion
    }
}
