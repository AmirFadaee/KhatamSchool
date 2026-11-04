using AutoMapper;
using KhatamSchool_Application.Interfaces;
using KhatamSchool_Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhatamSchool_Application.Features.Commands
{
    public class AddAchievementItemCommand : IRequest<Response<long>>
    {
        public long CategoryId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public IFormFile? Image { get; set; }
        public bool IsActive { get; set; } = true;
        public int DisplayOrder { get; set; } = 0;
    }
    public class AddAchievementItemCommandHandler : IRequestHandler<AddAchievementItemCommand, Response<long>>
    {
        private readonly IMapper _mapper;
        private readonly IAchievementItemRepository _repository;
        private readonly IWebHostEnvironment _environment;

        public AddAchievementItemCommandHandler(
            IMapper mapper,
            IAchievementItemRepository repository,
            IWebHostEnvironment environment)
        {
            _mapper = mapper;
            _repository = repository;
            _environment = environment;
        }

        public async Task<Response<long>> Handle(AddAchievementItemCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<AchievementItem>(request);

            if (request.Image != null && request.Image.Length > 0)
            {
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };
                var extension = Path.GetExtension(request.Image.FileName).ToLowerInvariant();

                if (!allowedExtensions.Contains(extension))
                {
                    return new Response<long>
                    {
                        Succeded = false,
                        Message = "فرمت تصویر مجاز نیست. فقط JPG، PNG یا WEBP مجاز است."
                    };
                }

                const long maxFileSize = 5 * 1024 * 1024; // 5MB
                if (request.Image.Length > maxFileSize)
                {
                    return new Response<long>
                    {
                        Succeded = false,
                        Message = "حجم تصویر نباید بیشتر از ۵ مگابایت باشد."
                    };
                }

                string uploadsFolder = Path.Combine(
                    _environment.WebRootPath,
                    "uploads",
                    "achievements");

                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                string fileName = Guid.NewGuid().ToString("N") + extension;
                string filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await request.Image.CopyToAsync(stream, cancellationToken);
                }

                entity.ImagePath = "/uploads/achievements/" + fileName;
            }

            await _repository.AddAsync(entity, cancellationToken);

            return new Response<long>
            {
                Succeded = true,
                Message = "آیتم با موفقیت ثبت شد.",
                Data = entity.Id
            };
        }
    }
}
