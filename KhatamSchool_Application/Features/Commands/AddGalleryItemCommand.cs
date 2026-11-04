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
    public class AddGalleryItemCommand : IRequest<Response<long>>
    {
        public long GalleryAlbumId { get; set; }

        public string FileName { get; set; } = null!;

        public string FilePath { get; set; } = null!;

        public bool IsVideo { get; set; }
    }
    public class AddGalleryItemCommandHandler : IRequestHandler<AddGalleryItemCommand, Response<long>>
    {
        private readonly IMapper _mapper;
        private readonly IGalleryItemRepository _repository;

        public AddGalleryItemCommandHandler(IMapper mapper, IGalleryItemRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<Response<long>> Handle(AddGalleryItemCommand request, CancellationToken cancellationToken)
        {
            var response = _mapper.Map<GalleryItem>(request);
            await _repository.AddAsync(response,cancellationToken);
            return new Response<long> { Succeded = true, Message = "آیتم با موفقیت ثبت شد.", Data = response.Id };
        }
    }
}
