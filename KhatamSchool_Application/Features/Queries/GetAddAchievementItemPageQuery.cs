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
    public class GetAddAchievementItemPageQuery : IRequest<AddAchievementItemPageViewModel>
    {

    }
    public class GetAddAchievementItemPageQueryHandler : IRequestHandler<GetAddAchievementItemPageQuery, AddAchievementItemPageViewModel>
    {
        private readonly IAchievementCategoryRepository _repository;
        public GetAddAchievementItemPageQueryHandler(IAchievementCategoryRepository repository)
        {
            _repository = repository;
        }
        public async Task<AddAchievementItemPageViewModel> Handle(GetAddAchievementItemPageQuery request, CancellationToken cancellationToken)
        {
            var categories = await _repository.GetActiveAchievementCategoryAsync();

            var model = new AddAchievementItemPageViewModel
            {
                Categories = categories
                    .Select(x => new AchievementCategoryLookupViewModel
                    {
                        Id = x.Id,
                        Title = x.Title,
                        IconClass = x.IconClass
                    })
                    .ToList()
            };

            return model;
        }
    }
}
