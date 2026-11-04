using KhatamSchool_Application.Features.Commands;
using KhatamSchool_Application.ViewModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhatamSchool_Application.Interfaces.Services
{
    public interface IAdminService
    {
        public Task<Response<long>> AddTeacherAsync(AddTeacherCommand command, IFormFile ProfileImage);
        public Task<Response<long>> AddExecutiveStaffAsync(AddExecutiveStaffCommand command, IFormFile ProfileImage);
        public Task<List<TeacherListViewModel>> GetTeacherListAsync();
        public Task<List<CounselingTicketListViewModel>> GetCounselingTicketListAsync();
        public Task<List<NewsListViewModel>> GetNewsListAsync();
        public Task<List<ExecutiveStaffListViewModel>> GetExecutiveStaffListAsync();
        public Task<AddAchievementItemPageViewModel> GetAddAchievementItemPageAsync();
        public Task<Response<long>> AddAchievementCategoryAsync(AddAchievementCategoryCommand command);
        public Task<Response<long>> AddAchievementItemAsync(AddAchievementItemCommand command);
        public Task<Response<long>> AddNewsAsync(AddNewsCommand command);
        public Task<List<AchievementCategoryListViewModel>> GetAchievementCategoryListAsync();
        public Task<List<AchievementItemListViewModel>> GetAchievementItemListAsync();
        public Task<Response<long>> DeleteTeacherAsync(long Id);
        public Task<Response<long>> DeleteExecutiveStaffAsync(long Id);
        public Task<Response<long>> DeleteAchievementCategoryAsync(long Id);
        public Task<Response<long>> DeleteNewsAsync(long Id);
        public Task<Response<long>> DeleteAchievementItemAsync(long Id);
        public Task<UpdateTeacherCommand> GetTeacherDataByIdAsync(long Id);
        public Task<Response<long>> UpdateTeacherAsync(UpdateTeacherCommand command);

    }
}
