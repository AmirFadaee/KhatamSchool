using KhatamSchool_Application.Features.Commands;
using KhatamSchool_Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhatamSchool_Application.Interfaces.Services
{
    public interface IDepartmentsService
    {
        public Task<Response<long>> AddCounselingTicketAsync(AddCounselingTicketCommand command);
        public Task<List<TeacherListItemViewModel>> GetTeacherListItemAsync();
    }
}
