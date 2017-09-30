using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Abstract;
using Domain.Entities;

namespace Domain.Concrete.Repositories
{
    public class EFLotRepository : ILotRepository
    {
        private readonly EFDbContext _context = new EFDbContext();

        public IQueryable<Lot> Lots => _context.Lots;

        public void SaveLot(Lot lot)
        {
            if (lot.Id == 0)
            {
                _context.Lots.Add(lot);
            }
            else
            {
                Lot dbEntry = _context.Lots.Find(lot.Id);

                if (dbEntry != null)
                {
                    dbEntry.Id = lot.Id;
                    dbEntry.Title = lot.Title;
                    dbEntry.Description = lot.Description;
                    dbEntry.ViewCount = lot.ViewCount;
                    dbEntry.CurrentPrice = lot.CurrentPrice;
                    dbEntry.GoldPrice = lot.GoldPrice;
                    dbEntry.UploadDate = lot.UploadDate;
                    dbEntry.EndOfTranding = lot.EndOfTranding;
                    dbEntry.CategoryId = lot.CategoryId;
                    dbEntry.IsEnded = lot.IsEnded;
                    dbEntry.UserId = lot.UserId;
                }
            }

            _context.SaveChanges();
        }

        public void IncViewCount(long id)
        {
            Lot dbEntry = _context.Lots.FirstOrDefault(l=>l.Id==id);

            if(dbEntry!=null)
                    dbEntry.ViewCount++;

            _context.SaveChanges(); 
        }
    }
}
