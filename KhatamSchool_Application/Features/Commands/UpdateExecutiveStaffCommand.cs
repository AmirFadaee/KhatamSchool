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
    public class UpdateExecutiveStaffCommand : IRequest<Response<long>>
    {
        public long Id { get; set; }    
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string PersonnelCode { get; set; } = null!;
        public ExecutiveRole Role { get; set; }
        public string? CustomRoleTitle { get; set; }
        public string? Bio { get; set; }
        public string? ProfileImagePath { get; set; }
        public int? YearsOfExperience { get; set; }
        public string? ExpertiseField { get; set; }
        public DateOnly HireDate { get; set; }
        public bool IsActive { get; set; } = true;
        public int DisplayOrder { get; set; } = 0;
        public string? UserId { get; set; }
    }
    public class UpdateExecutiveStaffCommandHandler : IRequestHandler<UpdateExecutiveStaffCommand, Response<long>>
    {
        private readonly IExecutiveStaffRepository _repository;
        public UpdateExecutiveStaffCommandHandler(IExecutiveStaffRepository repository)
        {
            _repository = repository;
        }
        public async Task<Response<long>> Handle(UpdateExecutiveStaffCommand request, CancellationToken cancellationToken)
        {
            var executiveStaff = await _repository.GetByIdAsync(request.Id);
            executiveStaff.FirstName = request.FirstName;
            executiveStaff.LastName = request.LastName;
            executiveStaff.PersonnelCode = request.PersonnelCode;
            executiveStaff.Role = request.Role;
            executiveStaff.CustomRoleTitle = request.CustomRoleTitle;
            executiveStaff.Bio=request.Bio;
            executiveStaff.ProfileImagePath = request.ProfileImagePath;
            executiveStaff.YearsOfExperience = request.YearsOfExperience;
            executiveStaff.ExpertiseField = request.ExpertiseField;
            executiveStaff.HireDate = request.HireDate;
            executiveStaff.IsActive = request.IsActive;
            executiveStaff.DisplayOrder = request.DisplayOrder;
            executiveStaff.UserId = request.UserId;
            await _repository.UpdateAsync(executiveStaff,cancellationToken);
            return new Response<long> { Succeded = true ,Message="آیتم با موفقیت ویرایش شد.",Data=executiveStaff.Id};
        }
    }
}
