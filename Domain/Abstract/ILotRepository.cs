using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Abstract
{
    public interface ILotRepository
    {
        IQueryable<Lot> Lots { get; }

        void IncViewCount(long id);

        void SaveLot(Lot lot);

        Lot GetLotById(long id);
    }
}
