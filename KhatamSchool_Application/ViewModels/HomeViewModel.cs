using KhatamSchool_Application.ViewModels.Home;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhatamSchool_Application.ViewModels
{
    public class HomeViewModel
    {
        public List<HomeNewsViewModel> News { get; set; } = new();
    }
}
