using BuildingManagment_Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhatamSchool_Domain.Models
{
    public class Grade : AuditableBaseEntity
    {
        public string Title { get; set; } = null!;

        public int SortOrder { get; set; }
    }
}
