using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Abstract;
using Domain.Entities;

namespace Domain.Concrete.Repositories
{
    public class EFCategoryRepository : ICategoryRepository
    {
        private readonly EFDbContext _context;

        public EFCategoryRepository(EFDbContext context)
        {
            _context = context;
        }

        public IQueryable<Category> Categories => _context.Categories;

        public long GetCategoryIdByName(string name) => (from u in _context.Categories
                                                           where u.Name == name
                                                           select u.Id).FirstOrDefault();
    }
}
