using BuildingManagment_Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhatamSchool_Domain.Models
{
    public class AchievementCategory : AuditableBaseEntity
    {
        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = null!;      

        [MaxLength(500)]
        public string? Description { get; set; }           

        [MaxLength(100)]
        public string? IconClass { get; set; }             

        public bool IsActive { get; set; } = true;
        public int DisplayOrder { get; set; } = 0;

        public ICollection<AchievementItem> Items { get; set; } = new List<AchievementItem>();
    }
}
