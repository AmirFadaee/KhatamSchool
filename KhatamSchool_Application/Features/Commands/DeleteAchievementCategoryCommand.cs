using KhatamSchool_Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhatamSchool_Application.Features.Commands
{
    public class DeleteAchievementCategoryCommand : IRequest<Response<long>>
    {
        public long Id { get; set; }
    }
    public class DeleteAchievementCategoryCommandHandler : IRequestHandler<DeleteAchievementCategoryCommand, Response<long>>
    {
        private readonly IAchievementCategoryRepository _repository;
        public DeleteAchievementCategoryCommandHandler(IAchievementCategoryRepository repository)
        {
            _repository = repository;   
        }
        public async Task<Response<long>> Handle(DeleteAchievementCategoryCommand request, CancellationToken cancellationToken)
        {
            var achievementCategory = await _repository.GetByIdAsync(request.Id);
            await _repository.DeleteAsync(achievementCategory,cancellationToken);
            return new Response<long> { Succeded = true,Message="آیتم با موفقیت حذف شد.",Data= achievementCategory.Id };
        }
    }
}
