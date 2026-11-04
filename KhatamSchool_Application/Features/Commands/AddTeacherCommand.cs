using AutoMapper;
using KhatamSchool_Application.Interfaces;
using KhatamSchool_Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace KhatamSchool_Application.Features.Commands
{
    public class AddTeacherCommand : IRequest<Response<long>>
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string PersonnelCode { get; set; } = null!;
        public string? Specialty { get; set; }
        public string? Bio { get; set; }
        public int? YearsOfExperience { get; set; }
        public DateOnly HireDate { get; set; }
        public bool IsActive { get; set; } = true;
        public int DisplayOrder { get; set; } = 0;
        public string? UserId { get; set; }
        public IFormFile? ProfileImage { get; set; }
    }
    public class AddTeacherCommandHandler : IRequestHandler<AddTeacherCommand, Response<long>>
    {
        private readonly IMapper _mapper;
        private readonly ITeacherRepository _repository;
        private readonly IWebHostEnvironment _environment;

        public AddTeacherCommandHandler(
            IMapper mapper,
            ITeacherRepository repository,
            IWebHostEnvironment environment)
        {
            _mapper = mapper;
            _repository = repository;
            _environment = environment;
        }

        public async Task<Response<long>> Handle(AddTeacherCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Teacher>(request);

            if (request.ProfileImage != null && request.ProfileImage.Length > 0)
            {
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };
                var extension = Path.GetExtension(request.ProfileImage.FileName).ToLowerInvariant();

                if (!allowedExtensions.Contains(extension))
                {
                    return new Response<long>
                    {
                        Succeded = false,
                        Message = "فرمت تصویر مجاز نیست. فقط JPG، PNG یا WEBP مجاز است."
                    };
                }

                const long maxFileSize = 5 * 1024 * 1024; // 5MB
                if (request.ProfileImage.Length > maxFileSize)
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
                    "teachers");

                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                string fileName = Guid.NewGuid().ToString("N") + extension;
                string filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await request.ProfileImage.CopyToAsync(stream, cancellationToken);
                }

                entity.ProfileImagePath = "/uploads/teachers/" + fileName;
            }

            await _repository.AddAsync(entity, cancellationToken);

            return new Response<long>
            {
                Succeded = true,
                Message = "معلم با موفقیت ثبت شد.",
                Data = entity.Id
            };
        }
    }
}
