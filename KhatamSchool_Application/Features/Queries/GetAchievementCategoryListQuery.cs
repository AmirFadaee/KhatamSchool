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
    public class GetAchievementCategoryListQuery : IRequest<List<AchievementCategoryListViewModel>>
    {

    }
    public class GetAchievementCategoryListQueryHandler : IRequestHandler<GetAchievementCategoryListQuery, List<AchievementCategoryListViewModel>>
    {
        private readonly IAchievementCategoryRepository _repository;
        public GetAchievementCategoryListQueryHandler(IAchievementCategoryRepository repository)
        {
            _repository = repository;   
        }
        public async Task<List<AchievementCategoryListViewModel>> Handle(GetAchievementCategoryListQuery request, CancellationToken cancellationToken)
        {
            var list = await _repository.GetAllAsync(cancellationToken);
            return list.Select(x=> new AchievementCategoryListViewModel()
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                ItemsCount=x.Items.Count(),
                DisplayOrder = x.DisplayOrder,
                IconClass = x.IconClass,    
                IsActive=x.IsActive
            }).ToList();
        }
    }
}
