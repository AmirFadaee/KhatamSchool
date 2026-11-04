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
    public class AddCounselingTicketCommand : IRequest<Response<long>>
    {
        public string ParentName { get; set; } = null!;

        public string StudentName { get; set; } = null!;

        public string PhoneNumber { get; set; } = null!;

        public string Subject { get; set; } = null!;

        public string Message { get; set; } = null!;

        public TicketStatus Status { get; set; } = TicketStatus.New;

        public TicketPriority Priority { get; set; } = TicketPriority.Normal;
    }
    public class AddCounselingTicketCommandHandler : IRequestHandler<AddCounselingTicketCommand, Response<long>>
    {
        private readonly ICounselingTicketRepository _repository;
        private readonly IMapper _mapper;
        public AddCounselingTicketCommandHandler(ICounselingTicketRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }
        public async Task<Response<long>> Handle(AddCounselingTicketCommand request, CancellationToken cancellationToken)
        {
            var response = _mapper.Map<CounselingTicket>(request);

            response.TrackingCode = $"TK-{DateTime.Now:yyMMdd}-{Random.Shared.Next(1000, 9999)}";

            await _repository.AddAsync(response,cancellationToken);
            return new Response<long> { Succeded = true,Message="تیکت با موفقیت ارسال شد.",Data=response.Id };
        }
    }
}
