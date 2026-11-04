using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhatamSchool_Application.ViewModels
{
    public class NewsListViewModel
    {
        public long Id { get; set; }

        public string Title { get; set; } = null!;

        public string Summary { get; set; } = null!;

        public string? CoverImage { get; set; }

        public bool IsPublished { get; set; }

        public bool IsFeatured { get; set; }

        public int ViewCount { get; set; }

        public DateTime? PublishedAt { get; set; }
    }
}
