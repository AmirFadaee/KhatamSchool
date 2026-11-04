using KhatamSchool_Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhatamSchool_Application.ViewModels
{
    public class TeacherListViewModel
    {
        public long Id { get; set; }
        public string FullName { get; set; } = null!;
        public string PersonnelCode { get; set; } = null!;
        public string? Specialty { get; set; }
        public string? ProfileImagePath { get; set; }
        public int? YearsOfExperience { get; set; }
        public bool IsActive { get; set; }
        public DateOnly HireDate { get; set; }
        public int DisplayOrder { get; set; }
    }
}
