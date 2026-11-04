using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhatamSchool_Application.ViewModels
{
    public class AchievementViewModel
    {
        public long CategoryId { get; set; }
        public string CategoryTitle { get; set; } = null!;
        public string? CategoryDescription { get; set; }
        public string? IconClass { get; set; }
        public IEnumerable<AchievementItemViewModel> Items { get; set; } = new List<AchievementItemViewModel>();
    }
}
