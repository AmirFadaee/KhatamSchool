using KhatamSchool_Application.Interfaces;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KhatamSchool_Application.Features.Commands
{
    public class DeleteExecutiveStaffCommand : IRequest<Response<long>>
    {
        public long Id { get; set; }    
    }
    public class DeleteExecutiveStaffCommandHandler : IRequestHandler<DeleteExecutiveStaffCommand, Response<long>>
    {
        private readonly IExecutiveStaffRepository _repository;
        public DeleteExecutiveStaffCommandHandler(IExecutiveStaffRepository repository)=> _repository = repository;

        public async Task<Response<long>> Handle(DeleteExecutiveStaffCommand request, CancellationToken cancellationToken)
        {
            var executiveStaff = await _repository.GetByIdAsync(request.Id);
            await _repository.DeleteAsync(executiveStaff,cancellationToken);
            return new Response<long> { Succeded = true ,Message="آیتم با موفقیت حذف شد.",Data=executiveStaff.Id};
        }
    }
}
