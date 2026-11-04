using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhatamSchool_Application.Interfaces
{
    public class ColumnInformation
    {
        [Display(Name = "نام ستون")]
        public string Name { get; set; }

        [Display(Name = "نوع ورودی")]
        public string DataType { get; set; }
    }
}
