using KhatamSchool_Application.Interfaces;
using KhatamSchool_Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhatamSchool_Application.Features.Commands
{
    public class UpdateTeacherCommand : IRequest<Response<long>>
    {
        public long Id { get; set; }

        public string FirstName { get; set; } = null!;

        public string LastName { get; set; } = null!;

        public string PersonnelCode { get; set; } = null!;

        public string? Specialty { get; set; }

        public string? Bio { get; set; }

        public int? YearsOfExperience { get; set; }

        public DateOnly HireDate { get; set; }

        public bool IsActive { get; set; }

        public int DisplayOrder { get; set; }

        public string? UserId { get; set; }

        public string? CurrentProfileImagePath { get; set; }

        public IFormFile? ProfileImage { get; set; }
    }
    public class UpdateTeacherCommandHandler : IRequestHandler<UpdateTeacherCommand, Response<long>>
    {
        private readonly ITeacherRepository _repository;
        private readonly IWebHostEnvironment _environment;

        public UpdateTeacherCommandHandler(
            ITeacherRepository repository,
            IWebHostEnvironment environment)
        {
            _repository = repository;
            _environment = environment;
        }

        public async Task<Response<long>> Handle(UpdateTeacherCommand request, CancellationToken cancellationToken)
        {
            var teacher = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (teacher == null)
            {
                return new Response<long>
                {
                    Succeded = false,
                    Message = "معلم مورد نظر یافت نشد."
                };
            }

            teacher.FirstName = request.FirstName;
            teacher.LastName = request.LastName;
            teacher.PersonnelCode = request.PersonnelCode;
            teacher.Specialty = request.Specialty;
            teacher.Bio = request.Bio;
            teacher.YearsOfExperience = request.YearsOfExperience;
            teacher.HireDate = request.HireDate;
            teacher.IsActive = request.IsActive;
            teacher.DisplayOrder = request.DisplayOrder;
            teacher.UserId = request.UserId;

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

                const long maxFileSize = 5 * 1024 * 1024;

                if (request.ProfileImage.Length > maxFileSize)
                {
                    return new Response<long>
                    {
                        Succeded = false,
                        Message = "حجم تصویر نباید بیشتر از ۵ مگابایت باشد."
                    };
                }

                if (!string.IsNullOrWhiteSpace(teacher.ProfileImagePath))
                {
                    var oldImagePath = Path.Combine(
                        _environment.WebRootPath,
                        teacher.ProfileImagePath.TrimStart('/').Replace("/", Path.DirectorySeparatorChar.ToString()));

                    if (File.Exists(oldImagePath))
                    {
                        File.Delete(oldImagePath);
                    }
                }

                string uploadsFolder = Path.Combine(
                    _environment.WebRootPath,
                    "uploads",
                    "teachers");

                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                string fileName = Guid.NewGuid().ToString("N") + extension;
                string filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await request.ProfileImage.CopyToAsync(stream, cancellationToken);
                }

                teacher.ProfileImagePath = "/uploads/teachers/" + fileName;
            }

            await _repository.UpdateAsync(teacher, cancellationToken);

            return new Response<long>
            {
                Succeded = true,
                Message = "معلم با موفقیت ویرایش شد.",
                Data = teacher.Id
            };
        }
    }
}
