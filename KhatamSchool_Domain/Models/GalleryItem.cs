using BuildingManagment_Domain.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhatamSchool_Domain.Models
{
    public class GalleryItem : AuditableBaseEntity
    {
        public long GalleryAlbumId { get; set; }

        [Required]
        [MaxLength(300)]
        public string FileName { get; set; } = null!;

        [Required]
        [MaxLength(500)]
        public string FilePath { get; set; } = null!;

        [MaxLength(200)]
        public string? Title { get; set; }               

        public bool IsVideo { get; set; } = false;
        public bool IsActive { get; set; } = true;
        public int DisplayOrder { get; set; } = 0;

        public GalleryAlbum GalleryAlbum { get; set; } = null!;
    }
}
