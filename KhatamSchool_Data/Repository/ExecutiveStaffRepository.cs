using KhatamSchool_Application.Interfaces;
using KhatamSchool_Application.ViewModels;
using KhatamSchool_Data.Context;
using KhatamSchool_Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhatamSchool_Data.Repository
{
    public class ExecutiveStaffRepository : GenericRepositoryAsync<ExecutiveStaff>, IExecutiveStaffRepository
    {
        private readonly DbSet<ExecutiveStaff> _dbSet;
        public ExecutiveStaffRepository(KhatamSchool_DbContext dbContext) : base(dbContext) => _dbSet = dbContext.Set<ExecutiveStaff>();

        public async Task<List<ExecutiveStaffListViewModel>> GetExecutiveStaffListAsync()
        {
            var list = await _dbSet.ToListAsync();
            var result = list.Select(x => new ExecutiveStaffListViewModel()
            {
                Id = x.Id,
                FullName = x.FullName,
                PersonnelCode = x.PersonnelCode,
                Role = x.Role.ToString(),
                RoleEnum = x.Role,
                CustomRoleTitle = x.CustomRoleTitle,
                ProfileImagePath = x.ProfileImagePath,
                YearsOfExperience = x.YearsOfExperience,
                ExpertiseField = x.ExpertiseField,
                IsActive = x.IsActive,
                HireDate = x.HireDate,
                DisplayOrder = x.DisplayOrder,

            }).ToList();
            return result;
        }
    }
}
