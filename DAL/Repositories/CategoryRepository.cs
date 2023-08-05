using DAL.Data;
using DAL.Models;
using DAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        private readonly AppDbContext _dbContext;
        public CategoryRepository(AppDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
            return await _dbSet.Include(x => x.Game).FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<Category?> GetCategoryByNameAsync(string name)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.Name == name);
        }

        public async Task<IEnumerable<Category?>> GetCategoriesByGameIdAsync(int gameId)
        {
            return await _dbSet.Include(x => x.Game).Where(c => c.GameId == gameId).ToListAsync();
        }
    }
}
