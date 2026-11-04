using KhatamSchool_Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhatamSchool_Application.Features.Commands
{
    public class DeleteTeacherCommand : IRequest<Response<long>>
    {
        public long Id { get; set; }
    }
    public class DeleteTeacherCommandHandler : IRequestHandler<DeleteTeacherCommand, Response<long>>
    {
        private readonly ITeacherRepository _repository;
        public DeleteTeacherCommandHandler(ITeacherRepository repository)
        {
            _repository = repository;
        }
        public async Task<Response<long>> Handle(DeleteTeacherCommand request, CancellationToken cancellationToken)
        {
            var teacher = await _repository.GetByIdAsync(request.Id);
            await _repository.DeleteAsync(teacher);
            return new Response<long> { Succeded = true, Message = "آیتم با موفقیت حذف شد.", Data = teacher.Id };
        }
    }
}
