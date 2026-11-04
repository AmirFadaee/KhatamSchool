using KhatamSchool_Application.ViewModels;
using KhatamSchool_Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KhatamSchool_Application.Interfaces
{
    public interface IAchievementItemRepository : IGenericRepositoryAsync<AchievementItem>
    {
        public Task<List<AchievementItem>> GetAchievementItemListAsync(Expression<Func<AchievementItem,bool>> predicate);
    }
}
