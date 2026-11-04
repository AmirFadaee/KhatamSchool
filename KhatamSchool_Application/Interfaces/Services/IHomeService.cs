using KhatamSchool_Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhatamSchool_Application.Interfaces.Services
{
    public interface IHomeService
    {
        public Task<AchievementViewModel> GetAchievementsViewAsync(long CategoryId);
        public Task<HomeViewModel> GetHomeDataAsync();
    }
}
