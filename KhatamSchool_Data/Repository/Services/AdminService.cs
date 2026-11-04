using KhatamSchool_Application;
using KhatamSchool_Application.Features.Commands;
using KhatamSchool_Application.Features.Queries;
using KhatamSchool_Application.Interfaces.Services;
using KhatamSchool_Application.ViewModels;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace KhatamSchool_Data.Repository.Services
{
    public class AdminService : IAdminService
    {
        private readonly IMediator _mediator;
        public AdminService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<Response<long>> AddExecutiveStaffAsync(AddExecutiveStaffCommand command, IFormFile ProfileImage)
        {
            var response = await _mediator.Send(command);
            return response;
        }

        public async Task<Response<long>> AddTeacherAsync(AddTeacherCommand command, IFormFile ProfileImage)
        {
            var response = await _mediator.Send(command);
            return response;
        }

        public async Task<List<ExecutiveStaffListViewModel>> GetExecutiveStaffListAsync()
        {
            var list = await _mediator.Send(new GetExecutiveStaffListQuery());
            return list;
        }

        public async Task<List<TeacherListViewModel>> GetTeacherListAsync()
        {
            var list = await _mediator.Send(new GetTeacherListQuery());
            return list;
        }
        public async Task<AddAchievementItemPageViewModel> GetAddAchievementItemPageAsync()
        {
            return await _mediator.Send(new GetAddAchievementItemPageQuery());
        }

        public async Task<Response<long>> AddAchievementCategoryAsync(AddAchievementCategoryCommand command)
        {
            var response = await _mediator.Send(command);
            return response;
        }

        public async Task<Response<long>> AddAchievementItemAsync(AddAchievementItemCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<List<AchievementCategoryListViewModel>> GetAchievementCategoryListAsync()
        {
            return await _mediator.Send(new GetAchievementCategoryListQuery());
        }

        public async Task<List<AchievementItemListViewModel>> GetAchievementItemListAsync()
        {
            return await _mediator.Send(new GetAchievementItemListQuery());
        }

        public async Task<Response<long>> DeleteTeacherAsync(long Id)
        {
            return await _mediator.Send(new DeleteTeacherCommand { Id = Id });
        }

        public async Task<Response<long>> DeleteExecutiveStaffAsync(long Id)
        {
            return await _mediator.Send(new DeleteExecutiveStaffCommand { Id = Id });
        }

        public async Task<Response<long>> DeleteAchievementCategoryAsync(long Id)
        {
            return await _mediator.Send(new DeleteAchievementCategoryCommand { Id = Id });
        }

        public async Task<List<CounselingTicketListViewModel>> GetCounselingTicketListAsync()
        {
            return await _mediator.Send(new GetCounselingTicketListQuery());
        }

        public async Task<Response<long>> DeleteAchievementItemAsync(long Id)
        {
            return await _mediator.Send(new DeleteAchievementItemCommand { Id = Id });
        }

        public async Task<Response<long>> AddNewsAsync(AddNewsCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<List<NewsListViewModel>> GetNewsListAsync()
        {
            return await _mediator.Send(new GetNewsListQuery());
        }

        public async Task<UpdateTeacherCommand> GetTeacherDataByIdAsync(long Id)
        {
            var teacher = await _mediator.Send(new GetTeacherDataByIdQuery() { teacherId = Id });
            return teacher;
        }

        public async Task<Response<long>> UpdateTeacherAsync(UpdateTeacherCommand command)
        {
            return await _mediator.Send(command);
        }

        public async Task<Response<long>> DeleteNewsAsync(long Id)
        {
            return await _mediator.Send(new DeleteNewsCommand() { Id = Id });
        }
    }
}
