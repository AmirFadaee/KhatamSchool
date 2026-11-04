using BuildingManagment_Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhatamSchool_Domain.Models
{
    public class AchievementItem : AuditableBaseEntity
    {
        public long CategoryId { get; set; }

        [Required]
        [MaxLength(300)]
        public string Title { get; set; } = null!;         

        [MaxLength(1000)]
        public string? Description { get; set; }           

        [MaxLength(500)]
        public string? ImagePath { get; set; }             

        public bool IsActive { get; set; } = true;
        public int DisplayOrder { get; set; } = 0;

        public AchievementCategory Category { get; set; } = null!;
    }
}
