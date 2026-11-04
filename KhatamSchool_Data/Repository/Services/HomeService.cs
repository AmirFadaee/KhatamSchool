using KhatamSchool_Application.Features.Queries;
using KhatamSchool_Application.Features.Queries.Home;
using KhatamSchool_Application.Interfaces.Services;
using KhatamSchool_Application.ViewModels;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhatamSchool_Data.Repository.Services
{
    public class HomeService : IHomeService
    {
        private readonly IMediator _mediator;
        public HomeService(IMediator mediator)
        {
            _mediator = mediator;   
        }

        public async Task<AchievementViewModel> GetAchievementsViewAsync(long CategoryId)
        {
            return await _mediator.Send(new GetAchievementCategoryWithItemsQuery { CategoryId=CategoryId});
        }
        public async Task<HomeViewModel> GetHomeDataAsync()
        {
            return new HomeViewModel
            {
                News = await _mediator.Send(new GetHomeNewsQuery())
            };
        }
    }
}
