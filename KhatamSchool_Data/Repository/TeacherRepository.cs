using KhatamSchool_Application.Interfaces;
using KhatamSchool_Application.ViewModels;
using KhatamSchool_Data.Context;
using KhatamSchool_Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KhatamSchool_Data.Repository
{
    public class TeacherRepository : GenericRepositoryAsync<Teacher> , ITeacherRepository
    {
        private readonly DbSet<Teacher> _dbSet;
        public TeacherRepository(KhatamSchool_DbContext dbContext) : base (dbContext) => _dbSet = dbContext.Set<Teacher>();

        public async Task<List<TeacherListViewModel>> GetTeacherListAsync()
        {
            var list = await _dbSet.ToListAsync();
            var result = list.Select(x => new TeacherListViewModel()
            {
                Id = x.Id,
                FullName = x.FullName,
                PersonnelCode = x.PersonnelCode,
                Specialty = x.Specialty,
                ProfileImagePath = x.ProfileImagePath,
                YearsOfExperience = x.YearsOfExperience,
                IsActive = x.IsActive,
                HireDate = x.HireDate,
                DisplayOrder = x.DisplayOrder
            }).ToList();
            return result;
        }
        public async Task<List<Teacher>> GetActiveTeachersAsync()
        {
            return await _dbSet
                .Where(x => x.IsActive)
                .OrderBy(x => x.DisplayOrder)
                .ThenBy(x => x.FirstName)
                .ToListAsync();
        }
    }
}
