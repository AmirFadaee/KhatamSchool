using AutoMapper;
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
    public class AddGalleryAlbumCommand : IRequest<Response<long>>
    {
        public string Title { get; set; } = null!;

        public string? Description { get; set; }
    }
    public class AddGalleryAlbumCommandHandler : IRequestHandler<AddGalleryAlbumCommand, Response<long>>
    {
        private readonly IMapper _mapper;
        private readonly IGalleryAlbumRepository _repository;
        public AddGalleryAlbumCommandHandler(IMapper mapper, IGalleryAlbumRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<Response<long>> Handle(AddGalleryAlbumCommand request, CancellationToken cancellationToken)
        {
            var response = _mapper.Map<GalleryAlbum>(request);
            await _repository.AddAsync(response,cancellationToken);
            return new Response<long> { Succeded = true, Message = "آیتم با موفقیت ثبت شد.", Data = response.Id };
        }
    }
}
