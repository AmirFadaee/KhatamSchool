using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhatamSchool_Application.ViewModels.Home
{
    public class HomeNewsViewModel
    {
        public long Id { get; set; }

        public string Title { get; set; } = null!;

        public string Summary { get; set; } = null!;

        public string? CoverImage { get; set; }

        public DateTime? PublishedAt { get; set; }

        public string Slug { get; set; } = null!;
    }
}
