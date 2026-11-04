using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhatamSchool_Application.ViewModels
{
    public class AchievementCategoryLookupViewModel
    {
        public long Id { get; set; }

        public string Title { get; set; } = null!;

        public string? IconClass { get; set; }
    }
}
