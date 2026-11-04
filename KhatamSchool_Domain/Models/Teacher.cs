using BuildingManagment_Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhatamSchool_Domain.Models
{
    public class Teacher : AuditableBaseEntity
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

        [MaxLength(500)]
        [Display(Name = "تخصص")]
        public string? Specialty { get; set; }

        [MaxLength(2000)]
        [Display(Name = "بیوگرافی")]
        public string? Bio { get; set; }

        [MaxLength(500)]
        [Display(Name = "تصویر پروفایل")]
        public string? ProfileImagePath { get; set; }

        [Display(Name = "سال‌های تجربه")]
        public int? YearsOfExperience { get; set; }

        [Display(Name = "تاریخ استخدام")]
        public DateOnly HireDate { get; set; }

        [Display(Name = "فعال")]
        public bool IsActive { get; set; } = true;

        [Display(Name = "ترتیب نمایش")]
        public int DisplayOrder { get; set; } = 0;

        // ارتباط با کاربر (اختیاری - اگر حساب کاربری داشته باشن)
        public string? UserId { get; set; }
        public ApplicationUser? User { get; set; }
    }
}
