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
    public class GetExecutiveStaffListQuery : IRequest<List<ExecutiveStaffListViewModel>>
    {

    }
    public class GetExecutiveStaffListQueryHandler : IRequestHandler<GetExecutiveStaffListQuery, List<ExecutiveStaffListViewModel>>
    {
        private readonly IExecutiveStaffRepository _repository;
        public GetExecutiveStaffListQueryHandler(IExecutiveStaffRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<ExecutiveStaffListViewModel>> Handle(GetExecutiveStaffListQuery request, CancellationToken cancellationToken)
        {
            var list = await _repository.GetExecutiveStaffListAsync();
            return list;
        }
    }
}
