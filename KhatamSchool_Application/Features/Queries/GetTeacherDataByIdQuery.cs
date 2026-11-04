using KhatamSchool_Application.Features.Commands;
using KhatamSchool_Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhatamSchool_Application.Features.Queries
{
    public class GetTeacherDataByIdQuery : IRequest<UpdateTeacherCommand>
    {
        public long teacherId { get; set; } 
    }
    public class GetTeacherDataByIdQueryHandler : IRequestHandler<GetTeacherDataByIdQuery, UpdateTeacherCommand>
    {
        private readonly ITeacherRepository _repository;
        public GetTeacherDataByIdQueryHandler(ITeacherRepository repository) => _repository = repository;

        public async Task<UpdateTeacherCommand> Handle(GetTeacherDataByIdQuery request, CancellationToken cancellationToken)
        {
            var teacher = await _repository.GetByIdAsync(request.teacherId,cancellationToken);
            if(teacher == null)
            {
                throw new Exception("معلم مورد نظر یافت نشد.");
            }
            var data = new UpdateTeacherCommand()
            {
                Id = teacher.Id,

                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
                PersonnelCode = teacher.PersonnelCode,

                Specialty = teacher.Specialty,
                Bio = teacher.Bio,

                YearsOfExperience = teacher.YearsOfExperience,

                HireDate = teacher.HireDate,

                IsActive = teacher.IsActive,

                DisplayOrder = teacher.DisplayOrder,

                UserId = teacher.UserId,

                CurrentProfileImagePath = teacher.ProfileImagePath
            };
            return data;    
        }
    }
}
