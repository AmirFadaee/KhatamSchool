using KhatamSchool_Application.Interfaces;
using KhatamSchool_Domain.Models;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhatamSchool_Application.Features.Commands
{
    public class UpdateAchievementItemCommand : IRequest<Response<long>>
    {
        public long Id { get; set; }
        public long CategoryId { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public string? ImagePath { get; set; }
        public bool IsActive { get; set; } = true;
        public int DisplayOrder { get; set; } = 0;
    }
    public class UpdateAchievementItemCommandHandler : IRequestHandler<UpdateAchievementItemCommand, Response<long>>
    {
        private readonly IAchievementItemRepository _repository;
        public UpdateAchievementItemCommandHandler(IAchievementItemRepository repository) =>_repository =repository;

        public async Task<Response<long>> Handle(UpdateAchievementItemCommand request, CancellationToken cancellationToken)
        {
            var achievementItem =await _repository.GetByIdAsync(request.Id);
            achievementItem.CategoryId= request.CategoryId; 
            achievementItem.Title = request.Title;
            achievementItem.Description = request.Description;  
            achievementItem.ImagePath = request.ImagePath;
            achievementItem.IsActive = request.IsActive;
            achievementItem.DisplayOrder = request.DisplayOrder;    
            await _repository.UpdateAsync(achievementItem,cancellationToken);
            return new Response<long> { Succeded = true, Message = "آیتم با موفقیت ویرایش شد.", Data = achievementItem.Id };
        }
    }
}
