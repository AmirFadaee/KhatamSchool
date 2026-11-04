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
    public class AddExecutiveStaffCommand : IRequest<Response<long>>
    {
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string FullName => $"{FirstName} {LastName}";

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
    public class AddExecutiveStaffCommandHandler : IRequestHandler<AddExecutiveStaffCommand, Response<long>>
    {
        private readonly IMapper _mapper;
        private readonly IExecutiveStaffRepository _repository;
        public AddExecutiveStaffCommandHandler(IMapper mapper, IExecutiveStaffRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<Response<long>> Handle(AddExecutiveStaffCommand request, CancellationToken cancellationToken)
        {
            var response = _mapper.Map<ExecutiveStaff>(request);    
            await _repository.AddAsync(response,cancellationToken);
            return new Response<long> { Succeded = true,Message="آیتم با موفقیت ثبت شد.",Data=response.Id };
        }
    }
}
