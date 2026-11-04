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
    public interface ITeacherRepository : IGenericRepositoryAsync<Teacher>
    {
        public Task<List<TeacherListViewModel>> GetTeacherListAsync();  
        public Task<List<Teacher>> GetActiveTeachersAsync();
    }
}
