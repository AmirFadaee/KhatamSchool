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
    public class DeleteGalleryAlbumCommand : IRequest<Response<long>>
    {
        public long Id { get; set; }
    }
    public class DeleteGalleryAlbumCommandHandler : IRequestHandler<DeleteGalleryAlbumCommand, Response<long>>
    {
        private readonly IGalleryAlbumRepository _repository;
        public DeleteGalleryAlbumCommandHandler(IGalleryAlbumRepository repository)
        {
            _repository = repository;
        }
        public async Task<Response<long>> Handle(DeleteGalleryAlbumCommand request, CancellationToken cancellationToken)
        {
            var gallery = await _repository.GetByIdAsync(request.Id);
            await _repository.DeleteAsync(gallery,cancellationToken);
            return new Response<long> { Succeded = true, Message = "آیتم با موفقیت حذف شد.", Data = gallery.Id };
        }
    }
}
