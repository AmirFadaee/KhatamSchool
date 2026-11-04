using KhatamSchool_Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhatamSchool_Application.Features.Commands
{
    public class DeleteAchievementItemCommand : IRequest<Response<long>>
    {
        public long Id { get; set; }
    }
    public class DeleteAchievementItemCommandHandler : IRequestHandler<DeleteAchievementItemCommand, Response<long>>
    {
        private readonly IAchievementItemRepository _repository;
        public DeleteAchievementItemCommandHandler(IAchievementItemRepository repository) => _repository = repository;

        public async Task<Response<long>> Handle(DeleteAchievementItemCommand request, CancellationToken cancellationToken)
        {
            var achievementItem = await _repository.GetByIdAsync(request.Id);
            await _repository.DeleteAsync(achievementItem, cancellationToken);
            return new Response<long> { Succeded = true, Message = "آیتم با موفقیت حذف شد.", Data = achievementItem.Id };
        }
    }
}
