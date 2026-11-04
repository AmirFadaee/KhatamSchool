using BuildingManagment_Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhatamSchool_Domain.Models
{
    // Domain/Entities/CounselingTicket.cs
    public class CounselingTicket : AuditableBaseEntity
    {
        [Required]
        [MaxLength(100)]
        [Display(Name = "نام والدین")]
        public string ParentName { get; set; } = null!;

        [Required]
        [MaxLength(100)]
        [Display(Name = "نام دانش‌آموز")]
        public string StudentName { get; set; } = null!;

        [Required]
        [MaxLength(20)]
        [Display(Name = "شماره تماس")]
        public string PhoneNumber { get; set; } = null!;

        [Required]
        [MaxLength(300)]
        [Display(Name = "موضوع")]
        public string Subject { get; set; } = null!;

        [Required]
        [MaxLength(3000)]
        [Display(Name = "متن پیام")]
        public string Message { get; set; } = null!;

        [Display(Name = "وضعیت")]
        public TicketStatus Status { get; set; } = TicketStatus.New;

        [Display(Name = "اولویت")]
        public TicketPriority Priority { get; set; } = TicketPriority.Normal;

        // پاسخ مشاور
        [MaxLength(3000)]
        [Display(Name = "پاسخ مشاور")]
        public string? Reply { get; set; }

        [Display(Name = "تاریخ پاسخ")]
        public DateTime? RepliedAt { get; set; }



        [Display(Name = "تاریخ مشاهده")]
        public DateTime? SeenAt { get; set; }

        [Display(Name = "کد پیگیری")]
        public string TrackingCode { get; set; } = GenerateTrackingCode();

        private static string GenerateTrackingCode()
        {
            return $"TK-{DateTime.Now:yyMMdd}-{Random.Shared.Next(1000, 9999)}";
        }
    }
    public enum TicketStatus
    {
        [Display(Name = "جدید")]
        New = 1,

        [Display(Name = "در حال بررسی")]
        InProgress = 2,

        [Display(Name = "پاسخ داده شده")]
        Answered = 3,

        [Display(Name = "بسته شده")]
        Closed = 4
    }

    // Domain/Enums/TicketPriority.cs
    public enum TicketPriority
    {
        [Display(Name = "عادی")]
        Normal = 1,

        [Display(Name = "مهم")]
        Important = 2,

        [Display(Name = "فوری")]
        Urgent = 3
    }
}
