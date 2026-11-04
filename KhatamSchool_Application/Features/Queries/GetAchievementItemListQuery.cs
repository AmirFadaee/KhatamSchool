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
    public class GetAchievementItemListQuery : IRequest<List<AchievementItemListViewModel>>
    {

    }
    public class GetAchievementItemListQueryHandler : IRequestHandler<GetAchievementItemListQuery, List<AchievementItemListViewModel>>
    {
        private readonly IAchievementItemRepository _repository;
        public GetAchievementItemListQueryHandler(IAchievementItemRepository repository)
        {
            _repository = repository;
        }
        public async Task<List<AchievementItemListViewModel>> Handle(GetAchievementItemListQuery request, CancellationToken cancellationToken)
        {
            var list = await _repository.GetAchievementItemListAsync(x => x.IsActive);
            return list.Select(x => new AchievementItemListViewModel()
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                ImagePath = x.ImagePath,
                CategoryId = x.CategoryId,
                CategoryTitle = x.Category.Title,
                DisplayOrder = x.DisplayOrder,
                CategoryIconClass = x.Category.IconClass,
                IsActive = x.IsActive
            }).ToList();
        }
    }
}
