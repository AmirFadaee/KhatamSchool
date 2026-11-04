using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhatamSchool_Application.ViewModels
{
    public class AchievementItemListViewModel
    {
        public long Id { get; set; }

        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        public string? ImagePath { get; set; }

        public long CategoryId { get; set; }

        public string CategoryTitle { get; set; } = null!;

        public string? CategoryIconClass { get; set; }

        public bool IsActive { get; set; }

        public int DisplayOrder { get; set; }
    }
}
