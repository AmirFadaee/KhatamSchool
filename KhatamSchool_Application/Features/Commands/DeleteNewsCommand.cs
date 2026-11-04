using KhatamSchool_Application.Interfaces;
using KhatamSchool_Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhatamSchool_Application.Features.Commands
{
    public class DeleteNewsCommand : IRequest<Response<long>>
    {
        public long Id { get; set; }    
    }
    public class DeleteNewsCommandHandler : IRequestHandler<DeleteNewsCommand, Response<long>>
    {
        private readonly INewsRepository _repository;
        public DeleteNewsCommandHandler(INewsRepository repository)
        {
            _repository = repository;
        }
        public async Task<Response<long>> Handle(DeleteNewsCommand request, CancellationToken cancellationToken)
        {
            var news = await _repository.GetByIdAsync(request.Id);
            await _repository.DeleteAsync(news,cancellationToken);
            return new Response<long> { Succeded = true, Message = "آیتم با موفقیت حذف شد.", Data = news.Id };
        }
    }
}
