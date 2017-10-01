using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using Domain.Abstract;
using Domain.Entities;

namespace Domain.Concrete.Repositories
{
    public class EFBidRepository:IBidRepository
    {
        private readonly EFDbContext _context;

        public IQueryable<Bid> Bids => _context.Bids;

        public EFBidRepository(EFDbContext context)
        {
            _context = context;
        }

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
                    dbEntry.User = bid.User;
                    dbEntry.Lot = bid.Lot;
                }
            }
            try
            {
                _context.SaveChanges();
            }
            catch (Exception exception)
            {

            }
        }

    }
}
