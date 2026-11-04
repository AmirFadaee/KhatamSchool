using KhatamSchool_Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhatamSchool_Application.Features.Commands
{
    public class DeleteGalleryItemCommand : IRequest<Response<long>>
    {
        public long Id { get; set; }    
    }
    public class DeleteGalleryItemCommandHandler : IRequestHandler<DeleteGalleryItemCommand, Response<long>>
    {
        private readonly IGalleryItemRepository _repository;
        public DeleteGalleryItemCommandHandler(IGalleryItemRepository repository)
        {
            _repository = repository;
        }
        public async Task<Response<long>> Handle(DeleteGalleryItemCommand request, CancellationToken cancellationToken)
        {
            var item = await _repository.GetByIdAsync(request.Id);
            await _repository.DeleteAsync(item,cancellationToken);
            return new Response<long> { Succeded = true, Message = "آیتم با موفقیت حذف شد.", Data = item.Id };
        }
    }
}
