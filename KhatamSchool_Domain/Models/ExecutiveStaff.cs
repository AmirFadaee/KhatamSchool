using BuildingManagment_Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhatamSchool_Domain.Models
{
    // Domain/Entities/ExecutiveStaff.cs
    public class ExecutiveStaff : AuditableBaseEntity
    {
        [Required]
        [MaxLength(100)]
        [Display(Name = "نام")]
        public string FirstName { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        [Display(Name = "نام خانوادگی")]
        public string LastName { get; set; } = null!;

        [MaxLength(200)]
        [Display(Name = "نام کامل")]
        public string FullName => $"{FirstName} {LastName}";

        [Required]
        [MaxLength(20)]
        [Display(Name = "کد پرسنلی")]
        public string PersonnelCode { get; set; } = null!;

        [Required]
        [Display(Name = "سمت")]
        public ExecutiveRole Role { get; set; }

        [MaxLength(200)]
        [Display(Name = "عنوان سمت (سفارشی)")]
        public string? CustomRoleTitle { get; set; }

        [MaxLength(2000)]
        [Display(Name = "بیوگرافی")]
        public string? Bio { get; set; }

        [MaxLength(500)]
        [Display(Name = "تصویر پروفایل")]
        public string? ProfileImagePath { get; set; }

        [Display(Name = "سال‌های تجربه")]
        public int? YearsOfExperience { get; set; }

        [MaxLength(200)]
        [Display(Name = "حوزه تخصصی")]
        public string? ExpertiseField { get; set; }

        [Display(Name = "تاریخ استخدام")]
        public DateOnly HireDate { get; set; }

        [Display(Name = "فعال")]
        public bool IsActive { get; set; } = true;

        [Display(Name = "ترتیب نمایش")]
        public int DisplayOrder { get; set; } = 0;

        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; }
    }
    public enum ExecutiveRole
    {
        [Display(Name = "مدیر")]
        Principal = 1,

        [Display(Name = "معاون آموزشی")]
        AcademicVice = 2,

        [Display(Name = "معاون اجرایی")]
        ExecutiveVice = 3,

        [Display(Name = "معاون پرورشی")]
        CulturalVice = 4,

        [Display(Name = "مسئول دفتر")]
        OfficeManager = 5,

        [Display(Name = "سایر")]
        Other = 6
    }
}
