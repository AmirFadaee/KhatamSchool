using KhatamSchool_Application.Interfaces;
using KhatamSchool_Application.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhatamSchool_Application.Features.Queries
{
    public class GetNewsListQuery : IRequest<List<NewsListViewModel>>
    {

    }
    public class GetNewsListQueryHandler : IRequestHandler<GetNewsListQuery, List<NewsListViewModel>>
    {
        private readonly INewsRepository _repository;
        public GetNewsListQueryHandler(INewsRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<NewsListViewModel>> Handle(GetNewsListQuery request, CancellationToken cancellationToken)
        {
            var list = await _repository.GetAllAsync(cancellationToken);
            return list.Select(x => new NewsListViewModel()
            {
                Id = x.Id,
                Title = x.Title,
                Summary = x.Summary,
                CoverImage = x.CoverImage,
                IsPublished = x.IsPublished,
                IsFeatured = x.IsFeatured,
                ViewCount = x.ViewCount,
                PublishedAt = x.PublishedAt
            }).ToList();
        }
    }
}
