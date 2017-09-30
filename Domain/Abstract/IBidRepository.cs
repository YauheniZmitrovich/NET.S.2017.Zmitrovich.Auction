using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Abstract
{
    public interface IBidRepository
    {
        IQueryable<Bid> Bids { get; }

        void SaveBid(Bid bid);
    }
}
