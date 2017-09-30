using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Abstract;
using Domain.Entities;

namespace Domain.Concrete.Repositories
{
    public class EFBidRepository:IBidRepository
    {
        private readonly EFDbContext _context = new EFDbContext();

        public IQueryable<Bid> Bids => _context.Bids;

        public void SaveBid(Bid bid)
        {
            if (bid.Id == 0)
            {
                _context.Bids.Add(bid);
            }
            else
            {
                Bid dbEntry = _context.Bids.Find(bid.Id);

                if (dbEntry != null)
                {
                    dbEntry.Id = bid.Id;
                    dbEntry.Cost = bid.Cost;
                    dbEntry.DateTime = bid.DateTime;
                    dbEntry.LotId = bid.LotId;
                    dbEntry.UserId = bid.UserId;
                }
            }

            _context.SaveChanges();
        }

    }
}
