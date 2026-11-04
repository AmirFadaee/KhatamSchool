using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildingManagment_Domain.Models
{
    public abstract class AuditableBaseEntity
    {
        [Display(Name = "شناسه")]
        public long Id { get; set; }

        [MaxLength(200)]
        public string? CreatedBy { get; set; }

        public DateTime? Created { get; set; }

        [MaxLength(200)]
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModified { get; set; }

        [MaxLength(200)]
        public string? DeletedBy { get; set; }
        public DateTime? Deleted { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}