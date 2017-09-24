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
    }
}
