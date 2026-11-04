using AutoMapper;
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
    public class AddNewsCommand : IRequest<Response<long>>
    {
        public string Title { get; set; } = null!;

        public string Summary { get; set; } = null!;

        public string Content { get; set; } = null!;

        public IFormFile? CoverImage { get; set; }

        public bool IsPublished { get; set; }

        public DateTime? PublishedAt { get; set; }

        public bool IsFeatured { get; set; }
    }
    public class AddNewsCommandHandler : IRequestHandler<AddNewsCommand, Response<long>>
    {
        private readonly INewsRepository _newsRepository;
        private readonly IWebHostEnvironment _environment;
        private readonly IMapper _mapper;
        public AddNewsCommandHandler(INewsRepository newsRepository,IMapper mapper,IWebHostEnvironment environment)
        {
            _newsRepository = newsRepository;
            _mapper = mapper;   
            _environment = environment;
        }
        public async Task<Response<long>> Handle(AddNewsCommand request, CancellationToken cancellationToken)
        {
            var news = _mapper.Map<News>(request);

            news.Slug = GenerateSlug(request.Title);

            news.PublishedAt ??= DateTime.Now;

            news.ViewCount = 0;

            if (request.CoverImage != null)
            {
                string uploadsFolder = Path.Combine(
                    _environment.WebRootPath,
                    "uploads",
                    "news");

                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                string extension = Path.GetExtension(request.CoverImage.FileName);

                string fileName =
                    Guid.NewGuid().ToString("N") + extension;

                string filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await request.CoverImage.CopyToAsync(stream, cancellationToken);
                }

                news.CoverImage = "/uploads/news/" + fileName;
            }

            await _newsRepository.AddAsync(news, cancellationToken);

            return new Response<long> { Succeded = true, Message = "آیتم با موفقیت ثبت شد.", Data = news.Id };
        }
        private static string GenerateSlug(string title)
        {
            return title.Trim()
                .Replace(" ", "-")
                .Replace("ي", "ی")
                .Replace("ك", "ک")
                .ToLower();
        }
    }
}
