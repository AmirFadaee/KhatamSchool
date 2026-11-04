using BuildingManagment_Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhatamSchool_Domain.Models
{
    public class GalleryAlbum : AuditableBaseEntity
    {
        [Required]
        [MaxLength(200)]
        public string Title { get; set; } = null!;

        [MaxLength(500)]
        public string? Description { get; set; }

        [MaxLength(500)]
        public string? CoverImagePath { get; set; }        

        public bool IsActive { get; set; } = true;
        public int DisplayOrder { get; set; } = 0;

        public ICollection<GalleryItem> Items { get; set; } = new List<GalleryItem>();
    }
}
