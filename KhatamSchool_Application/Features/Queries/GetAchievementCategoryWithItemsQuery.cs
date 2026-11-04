using KhatamSchool_Application.Interfaces;
using KhatamSchool_Application.ViewModels;
using MediatR;

namespace KhatamSchool_Application.Features.Queries
{
    public class GetAchievementCategoryWithItemsQuery : IRequest<AchievementViewModel>
    {
        public long CategoryId { get; set; }
    }
    public class GetAchievementCategoryWithItemsQueryHandler : IRequestHandler<GetAchievementCategoryWithItemsQuery, AchievementViewModel>
    {
        private readonly IAchievementCategoryRepository _repository;
        public GetAchievementCategoryWithItemsQueryHandler(IAchievementCategoryRepository repository)
        {
            _repository = repository;
        }
        public async Task<AchievementViewModel> Handle(GetAchievementCategoryWithItemsQuery request, CancellationToken cancellationToken)
        {
            var category = await _repository.GetAchievementCategoryWithItemsAsync(request.CategoryId);
            if (category == null)
            {
                return new AchievementViewModel();
            }
            return new AchievementViewModel
            {
                CategoryId = category.Id,
                CategoryTitle = category.Title,
                CategoryDescription = category.Description,
                IconClass = category.IconClass,

                Items = category.Items
                    .OrderBy(x => x.DisplayOrder)
                    .ThenBy(x => x.Title)
                    .Select(x => new AchievementItemViewModel
                    {
                        Id = x.Id,
                        Title = x.Title,
                        Description = x.Description,
                        ImagePath = x.ImagePath
                    }).ToList()
            };
        }
    }
}
