using KhatamSchool_Application.ViewModels;
using KhatamSchool_Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhatamSchool_Application.Interfaces
{
    public interface IExecutiveStaffRepository : IGenericRepositoryAsync<ExecutiveStaff>
    {
        public Task<List<ExecutiveStaffListViewModel>> GetExecutiveStaffListAsync();
    }
}
