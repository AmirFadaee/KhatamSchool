using KhatamSchool_Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhatamSchool_Application.ViewModels
{
    public class CounselingTicketListViewModel
    {
        public long Id { get; set; }

        public string ParentName { get; set; } = null!;

        public string StudentName { get; set; } = null!;

        public string TrackingCode { get; set; } = null!;

        public string Subject { get; set; } = null!;
        public string Message { get; set; } = null!;

        public TicketStatus StatusEnum { get; set; }

        public string Status { get; set; } = null!;

        public TicketPriority PriorityEnum { get; set; }

        public string Priority { get; set; } = null!;

        public DateTime CreatedAt { get; set; }
    }
}
