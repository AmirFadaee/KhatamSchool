using KhatamSchool_Application.Interfaces;
using KhatamSchool_Application.ViewModels;
using KhatamSchool_Data.Context;
using KhatamSchool_Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhatamSchool_Data.Repository
{
    public class AchievementCategoryRepository : GenericRepositoryAsync<AchievementCategory>, IAchievementCategoryRepository
    {
        private readonly DbSet<AchievementCategory> _dbSet;
        public AchievementCategoryRepository(KhatamSchool_DbContext dbContext) : base(dbContext) => _dbSet = dbContext.Set<AchievementCategory>();

        public async Task<AchievementCategory> GetAchievementCategoryWithItemsAsync(long categoryId)
        {
            return await _dbSet
                .Include(x => x.Items.Where(x => x.IsActive))
                .Where(x => x.IsActive)
                .FirstOrDefaultAsync(x => x.Id == categoryId);
        }

        public async Task<List<AchievementCategory>> GetActiveAchievementCategoryAsync()
        {
            return await _dbSet.Where(x => x.IsActive).OrderBy(x => x.DisplayOrder).ThenBy(x => x.Title).ToListAsync();
        }
    }
}
