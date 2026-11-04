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
    public class GetTeacherListQuery : IRequest<List<TeacherListViewModel>>
    {

    }
    public class GetTeacherListQueryHandler : IRequestHandler<GetTeacherListQuery, List<TeacherListViewModel>>
    {
        private readonly ITeacherRepository _repository;

        public GetTeacherListQueryHandler(ITeacherRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<TeacherListViewModel>> Handle(GetTeacherListQuery request, CancellationToken cancellationToken)
        {
            var list = await _repository.GetTeacherListAsync();
            return list;
        }
    }
}
