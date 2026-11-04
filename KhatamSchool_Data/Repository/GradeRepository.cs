using KhatamSchool_Application.Interfaces;
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
    public class GradeRepository : GenericRepositoryAsync<Grade>, IGradeRepository
    {
        private readonly DbSet<Grade> _gradeSet;
        public GradeRepository(KhatamSchool_DbContext dbContext) : base(dbContext)=>_gradeSet=dbContext.Set<Grade>();   
    }
}
