using KhatamSchool_Application.Interfaces;
using KhatamSchool_Application.ViewModels;
using KhatamSchool_Data.Context;
using KhatamSchool_Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KhatamSchool_Data.Repository
{
    public class AchievementItemRepository : GenericRepositoryAsync<AchievementItem>, IAchievementItemRepository
    {
        private readonly DbSet<AchievementItem> _dbSet;
        public AchievementItemRepository(KhatamSchool_DbContext dbContext) : base(dbContext) => _dbSet = dbContext.Set<AchievementItem>();

        public async Task<List<AchievementItem>> GetAchievementItemListAsync(Expression<Func<AchievementItem, bool>> predicate)
        {
           return await _dbSet.Include(x=>x.Category).OrderBy(x=>x.DisplayOrder).ToListAsync();  
        }
    }
}
