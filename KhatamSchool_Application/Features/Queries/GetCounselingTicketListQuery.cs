using KhatamSchool_Application.Interfaces;
using KhatamSchool_Application.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhatamSchool_Application.Features.Queries
{
    public class GetCounselingTicketListQuery : IRequest<List<CounselingTicketListViewModel>>
    {

    }
    public class GetCounselingTicketListQueryHandler : IRequestHandler<GetCounselingTicketListQuery, List<CounselingTicketListViewModel>>
    {
        private readonly ICounselingTicketRepository _repository;
        public GetCounselingTicketListQueryHandler(ICounselingTicketRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<CounselingTicketListViewModel>> Handle(GetCounselingTicketListQuery request, CancellationToken cancellationToken)
        {
            var list =await _repository.GetAllAsync(cancellationToken);
            return list.Select(x => new CounselingTicketListViewModel
            {
                Id = x.Id,
                ParentName = x.ParentName,
                StudentName = x.StudentName,
                TrackingCode = x.TrackingCode,
                Subject = x.Subject,
                Message = x.Message,
                StatusEnum = x.Status,
                Status = x.Status.ToString(),
                PriorityEnum = x.Priority,
                Priority = x.Priority.ToString(),
                CreatedAt = x.Created.Value,
            }).ToList();
        }
    }
}
