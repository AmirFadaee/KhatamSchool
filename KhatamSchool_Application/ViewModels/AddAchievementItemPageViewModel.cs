using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhatamSchool_Application.ViewModels
{
    public class AddAchievementItemPageViewModel
    {
        public IEnumerable<AchievementCategoryLookupViewModel> Categories { get; set; }
            = new List<AchievementCategoryLookupViewModel>();
    }
}
