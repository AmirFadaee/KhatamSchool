using KhatamSchool_Application;
using KhatamSchool_Application.Features.Commands;
using KhatamSchool_Application.Features.Queries;
using KhatamSchool_Application.Interfaces.Services;
using KhatamSchool_Application.ViewModels;
using KhatamSchool_Domain.Models;
using MediatR;
using System.Linq.Expressions;

namespace KhatamSchool_Data.Repository.Services
{
    public class DepartmentsService : IDepartmentsService
    {
        private readonly IMediator _mediator;
        public DepartmentsService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<Response<long>> AddCounselingTicketAsync(AddCounselingTicketCommand command)
        {
            var response = await _mediator.Send(command);
            return response;
        }

        public async Task<List<TeacherListItemViewModel>> GetTeacherListItemAsync()
        {
            return await _mediator.Send(new GetActiveTeachersQuery());
        }
    }
}
