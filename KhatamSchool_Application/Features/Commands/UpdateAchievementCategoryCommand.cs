using KhatamSchool_Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhatamSchool_Application.Features.Commands
{
    public class UpdateAchievementCategoryCommand : IRequest<Response<long>>
    {
        public long Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public string? IconClass { get; set; }
        public bool IsActive { get; set; }
        public int DisplayOrder { get; set; }
    }
    public class UpdateAchievementCategoryCommandHandler : IRequestHandler<UpdateAchievementCategoryCommand, Response<long>>
    {
        private readonly IAchievementCategoryRepository _repository;
        public UpdateAchievementCategoryCommandHandler(IAchievementCategoryRepository repository)
        {
            _repository = repository;
        }
        public async Task<Response<long>> Handle(UpdateAchievementCategoryCommand request, CancellationToken cancellationToken)
        {
            var achievementCategory = await _repository.GetByIdAsync(request.Id);
            achievementCategory.Title = request.Title;
            achievementCategory.Description = request.Description;
            achievementCategory.IconClass = request.IconClass;
            achievementCategory.IsActive = request.IsActive;
            achievementCategory.DisplayOrder = request.DisplayOrder;
            await _repository.UpdateAsync(achievementCategory, cancellationToken);
            return new Response<long> { Succeded = true, Message = "آیتم با موفقیت ویرایش شد.", Data = achievementCategory.Id };
        }
    }
}
