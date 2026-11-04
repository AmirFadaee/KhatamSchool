using AutoMapper;
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
    public class GetActiveTeachersQuery : IRequest<List<TeacherListItemViewModel>>
    {

    }
    public class GetActiveTeachersQueryHandler : IRequestHandler<GetActiveTeachersQuery, List<TeacherListItemViewModel>>
    {
        private readonly ITeacherRepository _repository;
        private readonly IMapper _mapper;

        public GetActiveTeachersQueryHandler(ITeacherRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<TeacherListItemViewModel>> Handle(GetActiveTeachersQuery request, CancellationToken cancellationToken)
        {
            var teachers = await _repository.GetActiveTeachersAsync();
            return _mapper.Map<List<TeacherListItemViewModel>>(teachers);
        }
    }
}
