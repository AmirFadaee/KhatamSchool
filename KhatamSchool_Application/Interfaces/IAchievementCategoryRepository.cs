using KhatamSchool_Application.ViewModels;
using KhatamSchool_Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhatamSchool_Application.Interfaces
{
    public interface IAchievementCategoryRepository : IGenericRepositoryAsync<AchievementCategory>
    {
        public Task<List<AchievementCategory>> GetActiveAchievementCategoryAsync();
        public Task<AchievementCategory> GetAchievementCategoryWithItemsAsync(long categoryId);
    }
}
