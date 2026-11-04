using AutoMapper;
using KhatamSchool_Application.Interfaces;
using KhatamSchool_Domain.Models;
using MediatR;

namespace KhatamSchool_Application.Features.Commands
{
    public class AddGradeCommand : IRequest<Response<long>>
    {
        public string Title { get; set; } = null!;

        public int SortOrder { get; set; }
    }
    public class AddGradeCommandHandler : IRequestHandler<AddGradeCommand, Response<long>>
    {
        private readonly IGradeRepository _repository;
        private readonly IMapper _mapper;
        public AddGradeCommandHandler(IGradeRepository repository, IMapper mapper)
        {
            _repository = repository;   
            _mapper = mapper;
        }
        public async Task<Response<long>> Handle(AddGradeCommand request, CancellationToken cancellationToken)
        {
            var response = _mapper.Map<Grade>(request); 
            await _repository.AddAsync(response,cancellationToken);
            return new Response<long> { Succeded = true, Message = "آیتم با موفقیت ثبت شد.", Data = response.Id };
        }
    }
}
