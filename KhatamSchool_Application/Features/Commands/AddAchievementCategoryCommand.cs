using AutoMapper;
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
    public class AddAchievementCategoryCommand : IRequest<Response<long>>
    {
        public string Title { get; set; } = null!;
        public string? Description { get; set; }
        public string? IconClass { get; set; }
        public bool IsActive { get; set; } = true;
        public int DisplayOrder { get; set; } = 0;
    }
    public class AddAchievementCategoryCommandHandler : IRequestHandler<AddAchievementCategoryCommand, Response<long>>
    {
        private readonly IMapper _mapper;
        private readonly IAchievementCategoryRepository _repository;
        public AddAchievementCategoryCommandHandler(IMapper mapper, IAchievementCategoryRepository repository)
        {
            _mapper = mapper;
            _repository = repository;   
        }
        public async Task<Response<long>> Handle(AddAchievementCategoryCommand request, CancellationToken cancellationToken)
        {
            var response = _mapper.Map<AchievementCategory>(request);
            await _repository.AddAsync(response);
            return new Response<long> { Succeded = true,Message="آیتم با موفقیت ثبت شد.",Data=response.Id };
        }
    }
}
