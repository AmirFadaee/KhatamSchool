using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhatamSchool_Application.ViewModels
{
    public class AchievementCategoryListViewModel
    {
        public long Id { get; set; }

        public string Title { get; set; } = null!;

        public string? Description { get; set; }

        public string? IconClass { get; set; }

        public int ItemsCount { get; set; }

        public int DisplayOrder { get; set; }

        public bool IsActive { get; set; }
    }
}
